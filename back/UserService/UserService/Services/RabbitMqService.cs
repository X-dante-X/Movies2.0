using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using UserService.Models;

namespace UserService.Services;

public static class RabbitMqService
{
    public static async Task<List<MovieResponse>> GetMovieById(List<int> favoriteMovies)
    {
        Console.WriteLine("Starting RabbitMQ request...");

        var factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
        Console.WriteLine("Creating RabbitMQ connection...");

        await using var connection = await factory.CreateConnectionAsync();
        Console.WriteLine("Connection established.");

        await using var channel = await connection.CreateChannelAsync();
        Console.WriteLine("Channel created.");

        var replyQueue = await channel.QueueDeclareAsync(queue: "", durable: false, exclusive: true, autoDelete: true, arguments: null);
        var replyQueueName = replyQueue.QueueName;
        Console.WriteLine($"Reply queue declared with name: {replyQueueName}");

        var consumer = new AsyncEventingBasicConsumer(channel);
        var correlationId = Guid.NewGuid().ToString();
        var tcs = new TaskCompletionSource<string>();

        consumer.ReceivedAsync += async (model, ea) =>
        {
            if (ea.BasicProperties.CorrelationId == correlationId)
            {
                var response = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine($"Received response: {response}");
                tcs.SetResult(response);
                await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
            }
        };

        await channel.BasicConsumeAsync(queue: replyQueueName, autoAck: false, consumer: consumer);
        Console.WriteLine("Started consuming messages from reply queue.");

        var props = new BasicProperties
        {
            CorrelationId = correlationId,
            ReplyTo = replyQueueName
        };
        string json = JsonSerializer.Serialize(favoriteMovies);

        var messageBytes = Encoding.UTF8.GetBytes(json);
        Console.WriteLine($"Sending message with ID: json");

        await channel.BasicPublishAsync(exchange: "", routingKey: "movie_request", mandatory: true, basicProperties: props, body: messageBytes);
        Console.WriteLine("Message sent to RabbitMQ.");

        var responseJson = await tcs.Task;
        Console.WriteLine("Response received.");

        var movies = JsonSerializer.Deserialize<List<MovieResponse>>(responseJson);
        if (movies == null)
        {
          throw new Exception("Not found");
        }
        Console.WriteLine("Movie response deserialized.");
        return movies;
    }
}
