using DBContext;
using Fileupload;
using GraphQL;
using Microsoft.EntityFrameworkCore;
using Movies;
using MovieService.Services.Interfaces;
using MovieService.Services;
using MovieService;
using MovieService.RabbitMQService;
using HotChocolate.Execution.Options;
using HotChocolate.Execution.Configuration;
using HotChocolate.Execution;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Kestrel to allow large request bodies (up to 1GB).
// Required for large media uploads.
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 1024 * 1024 * 1024;
});

// Register gRPC client for FileUpload microservice.
// The URL is provided via PROCESSFILESERVICE_URL environment variable.
builder.Services.AddGrpcClient<FileUpload.FileUploadClient>(o =>
{
    o.Address = new Uri(Environment.GetEnvironmentVariable("PROCESSFILESERVICE_URL")!);
});


// Application services.
// UploadService is responsible for file streaming logic.
// RabbitMqService handles messaging and background tasks.
// RabbitMqListenerService listens for events from RabbitMQ.
builder.Services.AddSingleton<IUploadService, UploadService>();
builder.Services.AddSingleton<RabbitMqService>();
builder.Services.AddHostedService<RabbitMqListenerService>();

builder.Services.AddDbContext<Context>();

// Configure GraphQL server with Queries, Mutations, Upload support,
// and enable filtering, sorting, projections, and paging.
// Also applies cost limits to protect against expensive queries.
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<UploadType>()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddPagingArguments()
    .ModifyCostOptions(o =>
    {
        o.DefaultResolverCost = 10000;
        o.MaxFieldCost = 500000;
        o.MaxTypeCost = 500000;
    });



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Apply pending EF Core migrations and seed initial data.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Context>();
    db.Database.Migrate();
    var serviceProvider = scope.ServiceProvider;
    SeedData.Initialize(serviceProvider);
}

app.UseHttpsRedirection();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseRouting();
app.UseWebSockets();
app.UseAuthorization();
app.MapControllers();

// Map GraphQL endpoint.
app.MapGraphQL("/graphql");


app.MapGet("/", () =>
{
    return Results.Ok("hello world!");
});

app.Run();