namespace UserService.Models
{
    public class MovieReview
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public int Rating { get; set; } // 1-10
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
