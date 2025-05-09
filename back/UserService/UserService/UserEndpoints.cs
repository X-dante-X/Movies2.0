using Microsoft.AspNetCore.Http.HttpResults;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client.Events;
using UserService.Models;
using UserService.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;


namespace UserService;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/test", () => Results.Ok("Hello")).WithOpenApi();

        app.MapGet("/favorites", async (HttpContext httpContext, IUserService userService) =>
        {
            var authHeader = httpContext.Request.Headers.Authorization.FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return Results.Unauthorized();

            var token = authHeader.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
                return Results.Unauthorized();

            var jwtToken = handler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c =>
            c.Type == JwtRegisteredClaimNames.Sub || c.Type == "nameid");

            if (userIdClaim == null)
                return Results.Unauthorized();

            var userId = userIdClaim.Value;

            var favorites = await userService.GetUserFavoritesAsync($"{userId}");
            return Results.Ok(favorites);
        }).WithOpenApi();

        app.MapPost("/favorites", async (IUserService userService, UserMovieDto movieDto) =>
        {
            var favorites = await userService.AddOrUpdateUserMovieAsync(movieDto);
            return Results.Ok(favorites);
        }).WithOpenApi();

        app.MapGet("/favorites/{movieId:int}", async (HttpContext httpContext, IUserService userService, int movieId) =>
        {
            var authHeader = httpContext.Request.Headers.Authorization.FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return Results.Unauthorized();
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
            {
                return Results.Unauthorized();
            }

            var jwtToken = handler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c =>
            c.Type == JwtRegisteredClaimNames.Sub || c.Type == "nameid");

            if (userIdClaim == null)
            {
                return Results.Unauthorized();
            }

            var userId = userIdClaim.Value;
            
            var favorites = await userService.GetUsersWatchStatusAsync(movieId, userId);
            if (favorites == null)
            {
                return Results.Ok("");
            }
            return Results.Ok(favorites);
        }).WithOpenApi();

        app.MapPost("/favorites/delete", async (HttpContext httpContext, IUserService userService, UserMovieDeleteDTO delete) =>
        {
            var authHeader = httpContext.Request.Headers.Authorization.FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return Results.Unauthorized();
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
            {
                return Results.Unauthorized();
            }

            var jwtToken = handler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c =>
            c.Type == JwtRegisteredClaimNames.Sub || c.Type == "nameid");

            if (userIdClaim == null)
            {
                return Results.Unauthorized();
            }

            var userId = userIdClaim.Value;

            var deleted = await userService.DeleteFavoriteMovieAsync(delete);
            return Results.Ok(deleted);
        }).WithOpenApi();
    }
}


