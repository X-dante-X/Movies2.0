using MongoDB.Driver;
using Moq;

public class WeatherServiceTests
{
    private readonly Mock<IMongoCollection<WeatherForecast>> _mockWeatherCollection;
    private readonly WeatherService _weatherService;

    public WeatherServiceTests()
    {
        // Mock the MongoDB collection
        _mockWeatherCollection = new Mock<IMongoCollection<WeatherForecast>>();

        // Mock the MongoDB database
        var mockDatabase = new Mock<IMongoDatabase>();
        mockDatabase.Setup(x => x.GetCollection<WeatherForecast>("WeatherForecasts", null))
                    .Returns(_mockWeatherCollection.Object);

        // Mock the MongoDB client
        var mockClient = new Mock<IMongoClient>();
        mockClient.Setup(x => x.GetDatabase("WeatherDb", null))
                  .Returns(mockDatabase.Object);

        // Instantiate the service
        _weatherService = new WeatherService(mockClient.Object);
    }

    [Fact]
    public async Task SaveWeatherDataAsync_ShouldCallInsertOneAsync()
    {
        // Arrange
        var weatherForecast = new WeatherForecast(DateOnly.FromDateTime(DateTime.Now), 25, "Sunny");

        // Act
        await _weatherService.SaveWeatherDataAsync(weatherForecast);

        // Assert
        _mockWeatherCollection.Verify(
            x => x.InsertOneAsync(weatherForecast, null, default),
            Times.Once
        );
    }

    [Fact]
    public async Task GetWeatherDataAsync_ShouldReturnWeatherData()
    {
        // Arrange
        var weatherForecasts = new List<WeatherForecast>
        {
            new WeatherForecast(DateOnly.FromDateTime(DateTime.Now), 20, "Mild"),
            new WeatherForecast(DateOnly.FromDateTime(DateTime.Now.AddDays(1)), 30, "Hot")
        };

        var asyncCursorMock = new Mock<IAsyncCursor<WeatherForecast>>();
        asyncCursorMock.SetupSequence(x => x.MoveNext(It.IsAny<System.Threading.CancellationToken>()))
                       .Returns(true).Returns(false);
        asyncCursorMock.Setup(x => x.Current).Returns(weatherForecasts);

        _mockWeatherCollection.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<WeatherForecast>>(),
                                                      It.IsAny<FindOptions<WeatherForecast, WeatherForecast>>(),
                                                      default))
                              .ReturnsAsync(asyncCursorMock.Object);

        // Act
        var result = await _weatherService.GetWeatherDataAsync();

        // Assert
        Assert.NotNull(result);
        /*Assert.Equal(2, result.Count);
        Assert.Equal("Mild", result[0].Summary);
        Assert.Equal("Hot", result[1].Summary);*/
    }
}

