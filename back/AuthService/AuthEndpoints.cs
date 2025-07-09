using AuthService.Models;
using AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace AuthService;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/register", async (IUserService userService, RegisterRequest userDto) =>
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

        app.MapPost("/logout", async (IUserService userService) =>
        {
            try
            {
                var response = await userService.Logout();
                return Results.Ok(response);
            }
            catch (ApplicationException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
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
        }).WithOpenApi();

        app.MapGet("/google/login", (HttpContext context) =>
        {
            var props = new AuthenticationProperties
    {
        RedirectUri = "http://localhost/auth/google/callback"  
    };

            return Results.Challenge(props, new[] { GoogleDefaults.AuthenticationScheme });
        }).WithOpenApi();

        app.MapGet("/google/callback", async (HttpContext context, IUserService userService) =>
        {
            try
            {
                var result = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                if (!result.Succeeded)
                {
                    Console.WriteLine("Authentication failed");
                    return Results.BadRequest("Google authentication failed");
                }

                var claims = result.Principal?.Claims;
                var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var firstName = claims?.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
                var lastName = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
                var googleId = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(googleId))
                {
                    Console.WriteLine("Email or Google ID not found in claims");
                    return Results.BadRequest("Required information not found");
                }

                // Use your UserService to handle Google auth
                var googleAuthRequest = new GoogleAuthRequest
                {
                    Email = email,
                    FirstName = firstName ?? string.Empty,
                    LastName = lastName ?? string.Empty,
                    GoogleId = googleId
                };

                var loginResponse = await userService.GoogleAuth(googleAuthRequest);

                var redirectUrl = $"http://localhost:5173/oauth-callback?accessToken={loginResponse.AccessToken}&email={email}&refreshToken={loginResponse.RefreshToken}&username={loginResponse.Username}&isAdmin={loginResponse.IsAdmin}";
                return Results.Redirect(redirectUrl);

            }
            catch (Exception ex)
            {
                Console.WriteLine("OAuth callback error: " + ex.ToString());
                return Results.Problem("Internal server error during Google callback.");
            }
        }).WithOpenApi();


        app.MapGet("/hello", () => {
            return Results.Ok("world");
        }).WithOpenApi();
    }
}

