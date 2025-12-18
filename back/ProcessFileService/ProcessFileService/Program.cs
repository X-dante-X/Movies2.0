using Minio;
using ProcessFileService.Service;

var builder = WebApplication.CreateBuilder(args);

// Add gRPC services with increased message size limits
builder.Services.AddGrpc(options =>
{
    options.MaxReceiveMessageSize = 1000 * 1024 * 1024; // 1000 MB receive limit
    options.MaxSendMessageSize = 1000 * 1024 * 1024;    // 1000 MB send limit
});

// Configure Kestrel server to listen on port 8081 with HTTP/2 protocol
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8081, listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
    });
});

// Register MinioClient as a singleton service using environment variables for configuration
builder.Services.AddSingleton(sp =>
{
    return new MinioClient()
        .WithEndpoint(new Uri(Environment.GetEnvironmentVariable("MINIO_URL")!))
        .WithCredentials(
            Environment.GetEnvironmentVariable("MINIO_ACCESS_KEY")!,
            Environment.GetEnvironmentVariable("MINIO_SECRET_KEY")!
        )
        .WithSSL(false)
        .Build();
});

var app = builder.Build();

// Map the gRPC FileUploadService
app.MapGrpcService<FileUploadService>();

app.Run();
