using MovieService.Model.DTO;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using DBContext;
using MovieService.RabbitMQService.Messages;
using Models;
using Microsoft.EntityFrameworkCore;

namespace MovieService.Services;

/// <summary>
/// Service responsible for interacting with RabbitMQ.
/// Handles publishing and consuming messages, specifically for movie-related requests.
/// Implements <see cref="IAsyncDisposable"/> to ensure proper cleanup of connection and channel.
/// </summary>
public class RabbitMqService : IAsyncDisposable
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;
    private readonly string _requestQueue = "movie_request";
    private AsyncEventingBasicConsumer? _consumer;
    private readonly Context _context;

    /// <summary>
    /// Initializes a new instance of <see cref="RabbitMqService"/>.
    /// Establishes RabbitMQ connection and channel.
    /// </summary>
    /// <param name="context">EF Core database context to query movies.</param>
    /// <param name="hostName">RabbitMQ host name (default: "rabbitmq").</param>
    /// <param name="port">RabbitMQ port (default: 5672).</param>
    public RabbitMqService(Context context, string hostName = "rabbitmq", int port = 5672)
    {
        _context = context;
        Console.WriteLine("Initializing RabbitMqService...");

        var factory = new ConnectionFactory() { HostName = hostName, Port = port };
        Console.WriteLine("Creating RabbitMQ connection...");

        _connection = Task.Run(() => factory.CreateConnectionAsync()).Result;
        Console.WriteLine("RabbitMQ connection established.");

        _channel = Task.Run(() => _connection.CreateChannelAsync()).Result;
        Console.WriteLine("Channel created.");
    }

    /// <summary>
    /// Starts consuming messages from the request queue.
    /// Runs until the <see cref="cancellationToken"/> is canceled.
    /// </summary>
    /// <param name="cancellationToken">Token to signal stopping the consumption loop.</param>
    public async Task StartConsumingMessages(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting to consume messages...");

        await _channel.QueueDeclareAsync(_requestQueue, durable: false, exclusive: false, autoDelete: false);

        _consumer = new AsyncEventingBasicConsumer(_channel);
        _consumer.ReceivedAsync += OnMessageReceivedAsync;

        await _channel.BasicConsumeAsync(_requestQueue, autoAck: true, consumer: _consumer);

        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(1000, cancellationToken);
        }
    }

    /// <summary>
    /// Handles incoming RabbitMQ messages.
    /// Parses message content, queries movies from database, and sends response to the reply queue.
    /// </summary>
    /// <param name="sender">Event sender (RabbitMQ consumer).</param>
    /// <param name="ea">Event args containing message data and metadata.</param>
    public async Task OnMessageReceivedAsync(object sender, BasicDeliverEventArgs ea)
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        List<int> favoriteMovies = JsonSerializer.Deserialize<List<int>>(message)!;
        Console.WriteLine($"Received message: {message}");

        // Query movies matching IDs in the message
        var movies = await _context.Movies
            .Where(movie => favoriteMovies.Contains(movie.MovieId))
            .Select(movie => new RabbitMQMovieResponse
            {
                Id = movie.MovieId,
                Title = movie.Title,
                PosterPath = movie.PosterPath ?? "No image",
                Description = movie.Description
            })
            .ToListAsync();

        // Serialize response
        var responseJson = JsonSerializer.Serialize(movies);
        var responseBytes = Encoding.UTF8.GetBytes(responseJson);

        var properties = new BasicProperties
        {
            CorrelationId = ea.BasicProperties.CorrelationId,
            ReplyTo = ea.BasicProperties.ReplyTo
        };

        // Publish response to the reply queue
        await _channel.BasicPublishAsync(
            exchange: "",
            routingKey: properties.ReplyTo!,
            mandatory: true,
            basicProperties: properties,
            body: responseBytes
        );
    }

    /// <summary>
    /// Stops consuming messages gracefully.
    /// Cancels the consumer and unsubscribes from the queue.
    /// </summary>
    public async Task StopConsumingMessagesAsync()
    {
        if (_consumer != null)
        {
            await _channel.BasicCancelAsync(_consumer.ConsumerTags[0]);
            Console.WriteLine("Message consumption stopped.");
        }
    }

    /// <summary>
    /// Disposes RabbitMQ connection and channel asynchronously.
    /// Ensures resources are released when the service is disposed.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        Console.WriteLine("Disposing RabbitMqService...");

        if (_channel != null)
        {
            await _channel.CloseAsync();
            await _channel.DisposeAsync();
            Console.WriteLine("Channel closed and disposed.");
        }

        if (_connection != null)
        {
            await _connection.CloseAsync();
            await _connection.DisposeAsync();
            Console.WriteLine("Connection closed and disposed.");
        }
    }
}
