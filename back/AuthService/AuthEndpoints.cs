using AuthService.Models;
using AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

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

        app.MapGet("/google/login", ([FromQuery] string returnUrl, [FromServices] LinkGenerator linkGenerator, HttpContext context) =>
        {
            var publicHostUrl = "http://localhost/auth";

            var callbackPath = linkGenerator.GetPathByName(context, "GoogleLoginCallback") + $"?returnUrl={returnUrl}";
            var redirectUri = $"{publicHostUrl}{callbackPath}";

            Console.WriteLine(redirectUri);
            var properties = new AuthenticationProperties
            {
                RedirectUri = redirectUri
            };
            return Results.Challenge(properties, new[] { "Google" });


        }).WithOpenApi();

        app.MapGet("/verify-email", async (IUserService userService, [FromQuery] string id) =>
        {
            if (string.IsNullOrWhiteSpace(id))
                return Results.BadRequest("Missing token.");

           var verified = await userService.ValidateToken(id);
           if (verified) 
           { 
                var redirectUrl = "https://localhost:30000/email-verified";
                return Results.Redirect(redirectUrl);
            }
            return Results.BadRequest("Invalid or expired verification link.");
        }).WithOpenApi();

        app.MapGet("/google/callback", async ([FromQuery] string returnUrl, HttpContext context, IUserService userService) =>
        {
            try
            {
                var result = await context.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
                if (!result.Succeeded)
                {
                    Console.WriteLine("Authentication failed");
                    return Results.Unauthorized();
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
        }).WithOpenApi().WithName("GoogleLoginCallback");


        app.MapGet("/hello", () => {
            return Results.Ok("world");
        }).WithOpenApi();
    }
}

