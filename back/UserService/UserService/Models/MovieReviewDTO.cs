namespace UserService.Models;

public class MovieReviewDto
{
    public string UserId { get; set; } = null!;
    public int MovieId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
}
