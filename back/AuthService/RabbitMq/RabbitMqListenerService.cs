using AuthService.Services;

namespace AuthService.RabbitMQService;

public class RabbitMqListenerService : IHostedService
{
    private readonly RabbitMqService _rabbitMqService;
    private CancellationTokenSource? _cancellationTokenSource;
    private Task? _consumingTask;

    public RabbitMqListenerService(RabbitMqService rabbitMqService)
    {
        _rabbitMqService = rabbitMqService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting RabbitMQ listener service...");

        try
        {
            // Initialize the connection first
            await _rabbitMqService.InitializeAsync(cancellationToken);

            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            // Start consuming in background
            _consumingTask = Task.Run(
                () => StartConsumingMessages(_cancellationTokenSource.Token),
                cancellationToken);

            Console.WriteLine("✓ RabbitMQ listener service started successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Failed to start RabbitMQ listener: {ex.Message}");
            throw;
        }
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
        catch (Exception ex)
        {
            Console.WriteLine($"Error in message consumption: {ex.Message}");
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Stopping RabbitMQ listener service...");

        _cancellationTokenSource?.Cancel();

        if (_consumingTask != null)
        {
            try
            {
                await _consumingTask;
            }
            catch (OperationCanceledException)
            {
                // Expected when cancelling
            }
        }

        await _rabbitMqService.StopConsumingMessagesAsync();
        await _rabbitMqService.DisposeAsync();

        Console.WriteLine("✓ RabbitMQ listener service stopped");
    }
}