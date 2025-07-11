namespace AuthService.Models
{
    public class GoogleAuthRequest
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string GoogleId { get; set; } = string.Empty;
    }
}
