namespace AuthService.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;

    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }

    public string? GoogleId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool IsGoogleUser { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }

    public int UserStatus { get; set; } = 0;
    public bool IsAdmin => UserStatus == 1;
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExpiry { get; set; } = DateTime.UtcNow;

    public bool HasPassword => PasswordHash != null && PasswordSalt != null;
}