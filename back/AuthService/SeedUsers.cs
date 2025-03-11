using AuthService.Context;
using AuthService.Models;
using AuthService.Services.Interfaces;

namespace AuthService;

public static class SeedUsers
{
    public static void Seed(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!context.Users.Any())
        {
            var jwtService = scope.ServiceProvider.GetRequiredService<IJwtService>();

            var adminUser = new User
            {
                Username = "admin",
                Email = "admin@example.com",
                UserStatus = 1,
                RefreshToken = jwtService.GenerateRefreshToken(),
                RefreshTokenExpiry = DateTime.UtcNow.AddDays(7)
            };
            var user = new User
            {
                Username = "user",
                Email = "user@example.com",
                UserStatus = 0,
                RefreshToken = jwtService.GenerateRefreshToken(),
                RefreshTokenExpiry = DateTime.UtcNow.AddDays(7)
            };

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                adminUser.PasswordSalt = hmac.Key;
                adminUser.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Anime123!$"));
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("User@!!$"));
            }

            context.Users.Add(adminUser);
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
