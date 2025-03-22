using Minio;
using ProcessFileService.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc(options =>
{
    options.MaxReceiveMessageSize = 1000 * 1024 * 1024;
    options.MaxSendMessageSize = 1000 * 1024 * 1024;
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8081, listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
    });
});

builder.Services.AddSingleton(sp =>
{
    return new MinioClient()
        .WithEndpoint(new Uri (Environment.GetEnvironmentVariable("MINIO_URL")!))
        .WithCredentials(Environment.GetEnvironmentVariable("MINIO_ACCESS_KEY")!, Environment.GetEnvironmentVariable("MINIO_SECRET_KEY")!)
        .WithSSL(false)
        .Build();
});

var app = builder.Build();

app.MapGrpcService<FileUploadService>();

app.Run();
