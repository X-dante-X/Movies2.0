namespace UserService.Models
{
    public class UserMovieReview
    {
        public int Id { get; set; } 
        public string UserName { get; set; } = String.Empty;
        public string Comment { get; set; } = String.Empty;
    }
}
