using MovieService.Model.DTO;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using DBContext;
using MovieService.RabbitMQService.Messages;

public class RabbitMqService : IAsyncDisposable
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;
    private readonly string _requestQueue = "movie_request";
    private AsyncEventingBasicConsumer? _consumer;
    private readonly Context _context;
    private readonly Dictionary<int, MovieResponse> _movies = new()
    {
        { 1, new("The Matrix", "/images/matrix.jpg") },
        { 2, new("Inception", "/images/inception.jpg") },
        { 3, new("Interstellar", "/images/interstellar.jpg") }
    };

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

    private async Task OnMessageReceivedAsync(object sender, BasicDeliverEventArgs ea)
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        List<int> favoriteMovies = JsonSerializer.Deserialize<List<int>>(message);
        Console.WriteLine($"Received message: {message}");
        /*
        if (int.TryParse(message, out int movieId) && _movies.TryGetValue(movieId, out var movie))
        {
            var responseJson = JsonSerializer.Serialize(movie);
            var responseBytes = Encoding.UTF8.GetBytes(responseJson);

            var properties = new BasicProperties
            {
                CorrelationId = ea.BasicProperties.CorrelationId,
                ReplyTo = ea.BasicProperties.ReplyTo
            };

            await _channel.BasicPublishAsync(
                exchange: "",
                routingKey: properties.ReplyTo!,
                mandatory: true,
                basicProperties: properties,
                body: responseBytes
            );
        }
        else
        {
            Console.WriteLine($"Invalid movie ID received: {message}");
        }
        */
        List<RabbitMQMovieResponse> rabbitMQMovies = new List<RabbitMQMovieResponse>();
        foreach (var movie in favoriteMovies)
        {
            var movieModel = await MovieResponse(movie);
            rabbitMQMovies.Add(movieModel);
        }
        var responseJson = JsonSerializer.Serialize(rabbitMQMovies);
        var responseBytes = Encoding.UTF8.GetBytes(responseJson);

        var properties = new BasicProperties
        {
            CorrelationId = ea.BasicProperties.CorrelationId,
            ReplyTo = ea.BasicProperties.ReplyTo
        };

        await _channel.BasicPublishAsync(
            exchange: "",
            routingKey: properties.ReplyTo!,
            mandatory: true,
            basicProperties: properties,
            body: responseBytes
        );
    }

    public async Task<RabbitMQMovieResponse> MovieResponse(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            throw new Exception("Not found");
        }
        return new RabbitMQMovieResponse
        {
            Id = movie.MovieId,
            Title = movie.Title,
            PosterPath = movie.PosterPath == null ? "No image" : movie.PosterPath,
            Description = movie.Description
        };
    }
    public async Task StopConsumingMessagesAsync()
    {
        if (_consumer != null)
        {
            await _channel.BasicCancelAsync(_consumer.ConsumerTags[0]);
            Console.WriteLine("Message consumption stopped.");
        }
    }

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
