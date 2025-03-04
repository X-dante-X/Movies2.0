using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

var connectionUri = Environment.GetEnvironmentVariable("MONGODB_URI");
var settings = MongoClientSettings.FromConnectionString(connectionUri);
settings.ServerApi = new ServerApi(ServerApiVersion.V1);
builder.Services.AddSingleton<IMongoClient>(new MongoClient(settings));

builder.Services.AddSingleton<WeatherService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapPost("/api/generateweather", async (WeatherService weatherService) =>
{
    var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    foreach (var weather in forecast)
    {
        await weatherService.SaveWeatherDataAsync(weather);
    }

    return Results.Ok(forecast);
})
.WithOpenApi();


app.MapGet("/api/weather", async (ILogger<Program> logger, WeatherService weatherService) =>
{
    logger.LogInformation("get information from logs");
    var weatherData = await weatherService.GetWeatherDataAsync();
    return Results.Ok(weatherData);
})
.WithOpenApi();


app.Run();

