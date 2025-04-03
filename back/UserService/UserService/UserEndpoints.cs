using Microsoft.AspNetCore.Http.HttpResults;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client.Events;

namespace UserService;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/test", () => Results.Ok("Hello")).WithOpenApi();

        app.MapGet("/rabbitmq/{id:int}", async (int id) =>
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

            var messageBytes = Encoding.UTF8.GetBytes(id.ToString());
            Console.WriteLine($"Sending message with ID: {id}");

            await channel.BasicPublishAsync(exchange: "", routingKey: "movie_request", mandatory: true, basicProperties: props, body: messageBytes);
            Console.WriteLine("Message sent to RabbitMQ.");

            var responseJson = await tcs.Task;
            Console.WriteLine("Response received.");

            var movie = JsonSerializer.Deserialize<MovieResponse>(responseJson);
            Console.WriteLine("Movie response deserialized.");

            return Results.Ok(movie);
        }).WithOpenApi();
    }
}

record MovieResponse(string Title, string? PosterPath);
