using DBContext;
using Fileupload;
using GraphQL;
using Microsoft.EntityFrameworkCore;
using Movies;
using MovieService.Services.Interfaces;
using MovieService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpcClient<FileUpload.FileUploadClient>(o =>
{
    o.Address = new Uri(Environment.GetEnvironmentVariable("PROCESSFILESERVICE_URL")!);
});

builder.Services.AddSingleton<IUploadService, UploadService>();

builder.Services.AddDbContext<Context>();

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<UploadType>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

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
app.UseRouting();
app.UseWebSockets();
app.UseAuthorization();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.MapControllers();
app.MapGraphQL("/graphql");


app.MapGet("/", () =>
{
    return Results.Ok("hello world!");
});

app.Run();
