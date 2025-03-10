namespace AuthService.Models
{
    public class LoginResponseModel
    {
        public string? UserName { get; set; }   
        public string? AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsAdmin { get; set; }
        //  public int ExpiresIn { get; set; }      
    }
}
