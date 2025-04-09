using UserService.Models.Enums;

namespace UserService.Models;

public class UserMovieDto
{
    public string UserId { get; set; } = null!;
    public int MovieId { get; set; } 
    public bool IsFavorite { get; set; }
    public WatchStatus? Status { get; set; }
}
