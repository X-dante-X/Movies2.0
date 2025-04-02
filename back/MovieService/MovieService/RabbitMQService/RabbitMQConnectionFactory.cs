using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using MovieService.RabbitMQService.Messages;

namespace MovieService
{
    public class RabbitMQConnectionFactor : BackgroundService
    {
        private readonly ILogger<RabbitMQConnectionFactor>? _logger;
        private ConnectionFactory _factory;
        private IConnection? _connection;
        private IChannel? _channel;
        private readonly IConfiguration? _configuration;
        private AsyncEventingBasicConsumer? _consumer;
        private const string QueueName = "messages_queue";
        public RabbitMQConnectionFactor(ILogger<RabbitMQConnectionFactor> logger, IConfiguration? configuration)
        {
            _logger = logger; 
          
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                _logger.LogInformation("Received message: {Message}", message);

                await _channel.BasicAckAsync(e.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message.");
                await _channel.BasicAckAsync(e.DeliveryTag, multiple: false);
            }
        }


        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "admin",
                Password = "admin",
            };
            int retries = 5;
            while (retries > 0)
            {
                try
                {
                    Console.Write($"[TESTING] {_factory.HostName} {_factory.Port}");


                    _connection = await _factory.CreateConnectionAsync();
                    Console.Write($"[TESTING] Will it get to this point? {_factory.HostName} {_factory.Port}\n");

                    _channel = await _connection.CreateChannelAsync();
                    _consumer = new AsyncEventingBasicConsumer(_channel);

                    await _channel.QueueDeclareAsync(
                        queue: QueueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);
                    Console.Write($"[TESTING] TEST TEST{_factory.HostName} {_factory.Port}\n");

                    _consumer.ReceivedAsync += Consumer_Received;

                    await _channel.BasicConsumeAsync(QueueName, autoAck: false, consumer: _consumer);

                    _logger.LogInformation("RabbitMQ consumer connection established successfully");

                    break;

                }
                catch (Exception ex)
                {
                    if (_logger == null)
                    {
                        throw new InvalidOperationException("Logger is not initialized.");
                    }
                    retries--;
                    _logger.LogError(ex, "Error connecting to RabbitMQ. Retries left: {Retries}", retries);
                    await Task.Delay(2000); 
                }
            }

            if (_logger == null)
            {
                throw new InvalidOperationException("Logger is not initialized.");
            }



            try
            {
                if (_channel == null || _logger == null)
                {
                    throw new InvalidOperationException("Logger is not initialized.");
                }
                await _channel.QueueDeclareAsync(
                    queue: QueueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

                _logger.LogInformation("RabbitMQ consumer connection established successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error establishing RabbitMQ connection");
                throw;
            }

            await base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            if (_channel == null)
            {
                throw new InvalidOperationException("Logger is not initialized.");
            }


            _consumer = new AsyncEventingBasicConsumer(_channel);

            if (_logger == null)
            {
                throw new InvalidOperationException("Logger is not initialized.");
            }

            _consumer.ReceivedAsync += async (model, ea) =>
            {
                string messageBody = Encoding.UTF8.GetString(ea.Body.ToArray());
                string messageId = ea.BasicProperties.MessageId ?? "unknown";

                try
                {

                    var message = JsonSerializer.Deserialize<Message>(messageBody) ;

                    if (message == null)
                    {
                        throw new Exception("Can't be null");
                    }
                    _logger.LogInformation("Processing message: {MessageId}", messageId);
                    await ProcessMessageAsync(message);

                    await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);

                    _logger.LogInformation("Message {MessageId} processed successfully", messageId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message {MessageId}", messageId);

                    await _channel.BasicNackAsync(
                        deliveryTag: ea.DeliveryTag,
                        multiple: false,
                        requeue: false); 
                }
            };

            _channel.BasicConsumeAsync(
                queue: QueueName,
                autoAck: false, 
                consumer: _consumer);

            _logger.LogInformation("Started consuming messages from queue: {QueueName}", QueueName);

            return Task.CompletedTask;
        }
        private async Task ProcessMessageAsync(Message message)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));
            if (_logger == null)
            {
                throw new InvalidOperationException("Logger is not initialized.");
            }

            _logger.LogInformation("Message content: {MessageContent}", message.Name);

        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _channel?.CloseAsync();
            _connection?.CloseAsync();
            await base.StopAsync(cancellationToken);
        }
        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }

    }
}
