using UserService.Models.Enums;

namespace UserService.Models;

public class UserMovie
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public int MovieId { get; set; }
    public bool IsFavorite { get; set; }
    public WatchStatus? Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
