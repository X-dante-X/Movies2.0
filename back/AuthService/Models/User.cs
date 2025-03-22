namespace AuthService.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    public int UserStatus { get; set; } = 0; 
    public bool IsAdmin => UserStatus == 1;
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExpiry { get; set; } = DateTime.UtcNow; 
}
