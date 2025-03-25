using AuthService.Models.DTO;
using AuthService.Models;
using AuthService.Services.Interfaces;

namespace AuthService;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/register", async (IUserService userService, UserDTO userDto) =>
        {
            try
            {
                var response = await userService.Register(userDto);
                Console.WriteLine(response);
                return Results.Ok(response);
            }
            catch (ApplicationException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        }).WithOpenApi();

        app.MapPost("/validate", async (IUserService userService, ValidateRequest request) =>
        {
            await Console.Out.WriteLineAsync("request:" + request.ToString());
            try
            {
                var response = await userService.Validate(request.Token);
                return Results.Ok(response);
            }
            catch (ApplicationException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        }).WithOpenApi();

        app.MapGet("/hello", () => {
            return Results.Ok("world");
        }).WithOpenApi();

        app.MapPost("/login", async (IUserService userService, LoginRequestModel loginDto) =>
        {
            try
            {
                var response = await userService.Login(loginDto);
                return Results.Ok(response);
            }
            catch (ApplicationException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        }).WithOpenApi(); ;
    }
}

