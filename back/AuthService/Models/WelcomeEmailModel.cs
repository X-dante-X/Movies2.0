namespace AuthService.Models
{
    public class WelcomeEmailModel
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Message { get; set; } = "";
        public string ActionUrl { get; set; } = ""; 
    }

}
