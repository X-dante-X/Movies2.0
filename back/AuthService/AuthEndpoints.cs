using AuthService.Models.DTO;
using AuthService.Models;
using AuthService.Services.Interfaces;

namespace AuthService;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/api/auth/register", async (IUserService userService, UserDTO userDto) =>
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
        });

        app.MapPost("/api/auth/verify", async (IUserService userService, isAdminRequestModel user) =>
        {
            try
            {
                var response = await userService.Verify(user); 
                return Results.Ok(response);
            }
            catch (ApplicationException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });


        app.MapPost("/api/auth/login", async (IUserService userService, LoginRequestModel loginDto) =>
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
        });
    }
}

