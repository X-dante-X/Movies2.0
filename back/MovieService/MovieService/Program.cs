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

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 1024 * 1024 * 1024;
});

builder.Services.AddGrpcClient<FileUpload.FileUploadClient>(o =>
{
    o.Address = new Uri(Environment.GetEnvironmentVariable("PROCESSFILESERVICE_URL")!);
});

builder.Services.AddSingleton<IUploadService, UploadService>();
builder.Services.AddSingleton<RabbitMqService>();
builder.Services.AddHostedService<RabbitMqListenerService>();

builder.Services.AddDbContext<Context>();

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
        o.MaxFieldCost = 50000;
        o.MaxTypeCost = 50000;
    });



    var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
app.MapGraphQL("/graphql");


app.MapGet("/", () =>
{
    return Results.Ok("hello world!");
});

app.Run();