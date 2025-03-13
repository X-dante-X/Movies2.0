using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MoviesCore.Models;
using MovieWebAPI.DTO;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace Tests;

public class IntegrationTest
{

    [Fact]
    public async Task GetMovie_Returns_Success()
    {
        // Arrange
        await using var application = new WebApplicationFactory<API.Program>();
        using var client = application.CreateClient();
        int movieId = 5;

        // Act
        var response = await client.GetAsync($"api/movies/{movieId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        var movie = JsonConvert.DeserializeObject<MovieDTO>(content);

        Assert.NotNull(movie);
        Assert.Equal(movieId, movie.Id);
    }

    [Fact]
    public async Task GetMovie_Returns_NotFound_For_Invalid_Id()
    {
        // Arrange
        await using var application = new WebApplicationFactory<API.Program>();
        using var client = application.CreateClient();
        int invalidMovieId = 3;

        // Act
        var response = await client.GetAsync($"api/movies/{invalidMovieId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}