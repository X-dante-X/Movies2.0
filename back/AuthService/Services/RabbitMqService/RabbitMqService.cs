using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using AuthService.RabbitMq.Messages;
using Microsoft.EntityFrameworkCore;
using AuthService.Context;

namespace AuthService.Services;

public class RabbitMqService : IAsyncDisposable
{
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly string _requestQueue = "user_request";
    private AsyncEventingBasicConsumer? _consumer;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly string _hostName;
    private readonly int _port;

    public RabbitMqService(
        IServiceScopeFactory scopeFactory,
        IConfiguration configuration)
    {
        _scopeFactory = scopeFactory;
        _hostName = configuration["RabbitMQ:HostName"] ?? "rabbitmq";
        _port = int.Parse(configuration["RabbitMQ:Port"] ?? "5672");

        Console.WriteLine($"RabbitMqService created. Will connect to {_hostName}:{_port}");
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Initializing RabbitMQ connection with retry logic...");

        var factory = new ConnectionFactory()
        {
            HostName = _hostName,
            Port = _port,
            AutomaticRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
        };

        // Retry logic for initial connection
        int maxRetries = 10;
        int retryCount = 0;

        while (retryCount < maxRetries && !cancellationToken.IsCancellationRequested)
        {
            try
            {
                Console.WriteLine($"Attempting to connect to RabbitMQ (attempt {retryCount + 1}/{maxRetries})...");

                _connection = await factory.CreateConnectionAsync(cancellationToken);
                _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

                Console.WriteLine("✓ RabbitMQ connection established successfully.");
                return;
            }
            catch (Exception ex)
            {
                retryCount++;
                Console.WriteLine($"✗ Failed to connect to RabbitMQ: {ex.Message}");

                if (retryCount >= maxRetries)
                {
                    Console.WriteLine("Max retries reached. Could not connect to RabbitMQ.");
                    throw;
                }

                var delay = TimeSpan.FromSeconds(Math.Min(retryCount * 2, 30));
                Console.WriteLine($"Retrying in {delay.TotalSeconds} seconds...");
                await Task.Delay(delay, cancellationToken);
            }
        }
    }

    public async Task StartConsumingMessages(CancellationToken cancellationToken)
    {
        if (_channel == null)
        {
            throw new InvalidOperationException("RabbitMQ channel not initialized. Call InitializeAsync first.");
        }

        Console.WriteLine("Starting to consume messages...");

        await _channel.QueueDeclareAsync(
            queue: _requestQueue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            cancellationToken: cancellationToken);

        _consumer = new AsyncEventingBasicConsumer(_channel);
        _consumer.ReceivedAsync += OnMessageReceivedAsync;

        await _channel.BasicConsumeAsync(
            queue: _requestQueue,
            autoAck: true,
            consumer: _consumer,
            cancellationToken: cancellationToken);

        Console.WriteLine($"✓ Now listening on queue: {_requestQueue}");

        // Keep the consumer alive
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(1000, cancellationToken);
        }
    }

    private async Task OnMessageReceivedAsync(object sender, BasicDeliverEventArgs ea)
    {
        try
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Received message: {message}");

            List<string> selectedUsers = JsonSerializer.Deserialize<List<string>>(message)!;

            // Use scoped DbContext
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var users = await context.Users
                .Where(user => selectedUsers.Contains(user.Id.ToString()))
                .Select(user => new RabbitMQUserResponse
                {
                    Id = user.Id.ToString(),
                    UserName = user.Username,
                })
                .ToListAsync();

            var responseJson = JsonSerializer.Serialize(users);
            var responseBytes = Encoding.UTF8.GetBytes(responseJson);

            var properties = new BasicProperties
            {
                CorrelationId = ea.BasicProperties.CorrelationId,
                ReplyTo = ea.BasicProperties.ReplyTo
            };

            if (_channel != null)
            {
                await _channel.BasicPublishAsync(
                    exchange: "",
                    routingKey: properties.ReplyTo!,
                    mandatory: true,
                    basicProperties: properties,
                    body: responseBytes
                );

                Console.WriteLine($"✓ Sent response with {users.Count} users");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error processing message: {ex.Message}");
        }
    }

    public async Task StopConsumingMessagesAsync()
    {
        if (_consumer != null && _channel != null)
        {
            try
            {
                await _channel.BasicCancelAsync(_consumer.ConsumerTags[0]);
                Console.WriteLine("Message consumption stopped.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error stopping consumer: {ex.Message}");
            }
        }
    }

    public async ValueTask DisposeAsync()
    {
        Console.WriteLine("Disposing RabbitMqService...");

        if (_channel != null)
        {
            try
            {
                await _channel.CloseAsync();
                await _channel.DisposeAsync();
                Console.WriteLine("Channel closed and disposed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error disposing channel: {ex.Message}");
            }
        }

        if (_connection != null)
        {
            try
            {
                await _connection.CloseAsync();
                await _connection.DisposeAsync();
                Console.WriteLine("Connection closed and disposed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error disposing connection: {ex.Message}");
            }
        }
    }
}