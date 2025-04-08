using MovieService.Services;

namespace MovieService.RabbitMQService;

public class RabbitMqListenerService : IHostedService
{
    private readonly RabbitMqService _rabbitMqService;
    private CancellationTokenSource? _cancellationTokenSource;

    public RabbitMqListenerService(RabbitMqService rabbitMqService)
    {
        _rabbitMqService = rabbitMqService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting RabbitMQ listener...");

        _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        Task.Run(() => StartConsumingMessages(_cancellationTokenSource.Token), cancellationToken);

        return Task.CompletedTask;
    }

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

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Stopping RabbitMQ listener...");

        _cancellationTokenSource?.Cancel();

        return _rabbitMqService.StopConsumingMessagesAsync();
    }
}