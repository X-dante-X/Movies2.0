namespace AuthService.Models;

public class LoginResponseModel
{
    public string Username { get; set; } = null!;
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime Expiration { get; set; }
    public bool IsAdmin { get; set; }
    //  public int ExpiresIn { get; set; }      
}
