using Minio;
using ProcessFileService.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc(options =>
{
    options.MaxReceiveMessageSize = 1000 * 1024 * 1024;
    options.MaxSendMessageSize = 1000 * 1024 * 1024;
});

builder.Services.AddSingleton(sp =>
{
    return new MinioClient()
        .WithEndpoint("localhost", 9000)
        .WithCredentials("minioadmin", "minioadmin")
        .WithSSL(false)
        .Build();
});

var app = builder.Build();

app.MapGrpcService<MovieUploadService>();
app.MapGrpcService<PosterUploadService>();
app.MapGrpcService<BackdropUploadService>();

app.Run();
