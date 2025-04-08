using Microsoft.AspNetCore.Http.HttpResults;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client.Events;
using UserService.Models;
using UserService.Services;


namespace UserService;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/test", () => Results.Ok("Hello")).WithOpenApi();
       
        app.MapGet("/favorites/{id:int}", async (IUserService userService, int id) =>
        {
            var favorites = await userService.GetUserFavoritesAsync($"{id}");
            return Results.Ok(favorites);
        }).WithOpenApi();

        app.MapPost("/favorites", async (IUserService userService, UserMovieDto movieDto) =>
        {
            var favorites = await userService.AddOrUpdateUserMovieAsync(movieDto);
            return Results.Ok(favorites);
        }).WithOpenApi();

        app.MapPost("/delete", async (IUserService userService, UserMovieDeleteDTO delete) =>
        {
            var deleted = await userService.DeleteFavoriteMovieAsync(delete);
            return Results.Ok(deleted);
        }).WithOpenApi();   
    }
}


