using MovieService.Services;

namespace MovieService.RabbitMQService;

/// <summary>
/// Background hosted service that listens to RabbitMQ messages.
/// It starts consuming messages when the application starts and
/// stops gracefully when the application shuts down.
/// </summary>
public class RabbitMqListenerService : IHostedService
{
    private readonly RabbitMqService _rabbitMqService;
    private CancellationTokenSource? _cancellationTokenSource;

    /// <summary>
    /// Initializes a new instance of <see cref="RabbitMqListenerService"/>.
    /// </summary>
    /// <param name="rabbitMqService">The RabbitMQ service responsible for consuming messages.</param>
    public RabbitMqListenerService(RabbitMqService rabbitMqService)
    {
        _rabbitMqService = rabbitMqService;
    }

    /// <summary>
    /// Starts the hosted service and begins consuming RabbitMQ messages in a background task.
    /// </summary>
    /// <param name="cancellationToken">Token to signal start cancellation.</param>
    /// <returns>A completed task when the listener has been started.</returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting RabbitMQ listener...");

        _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        // Run message consumption in a background task
        Task.Run(() => StartConsumingMessages(_cancellationTokenSource.Token), cancellationToken);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Executes the consumption loop for RabbitMQ messages.
    /// This method runs in the background until canceled.
    /// </summary>
    /// <param name="cancellationToken">Token to signal stopping the consumption loop.</param>
    private async Task StartConsumingMessages(CancellationToken cancellationToken)
    {
        try
        {
            Console.WriteLine("RabbitMQ consumer is now listening for messages...");
            await _rabbitMqService.StartConsumingMessages(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("RabbitMQ message consumption was canceled.");
        }
    }

    /// <summary>
    /// Stops the hosted service and cancels message consumption gracefully.
    /// </summary>
    /// <param name="cancellationToken">Token to signal stop cancellation.</param>
    /// <returns>A task that completes when the service has stopped.</returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Stopping RabbitMQ listener...");

        // Cancel the background consumption
        _cancellationTokenSource?.Cancel();

        // Stop consuming messages in the RabbitMQ service
        return _rabbitMqService.StopConsumingMessagesAsync();
    }
}
