using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class WeatherService
{
    private readonly IMongoCollection<WeatherForecast> _weatherCollection;

    public WeatherService(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("WeatherDb");
        _weatherCollection = database.GetCollection<WeatherForecast>("WeatherForecasts");
    }

    public async Task SaveWeatherDataAsync(WeatherForecast weatherForecast)
    {
        await _weatherCollection.InsertOneAsync(weatherForecast);
    }

    public async Task<List<WeatherForecast>> GetWeatherDataAsync()
    {
        return await _weatherCollection.Find(_ => true).ToListAsync();
    }
}


public class WeatherForecast
{
    [BsonId]
    public ObjectId Id { get; set; }

    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public WeatherForecast(DateOnly date, int temperatureC, string? summary)
    {
        Date = date;
        TemperatureC = temperatureC;
        Summary = summary;
    }
}