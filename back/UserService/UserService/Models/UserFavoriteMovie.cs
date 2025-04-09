using UserService.Models.Enums;

namespace UserService.Models;

public class UserFavoriteMovie
{
    public string Description { get; set; } = null!;
    public string PosterPath { get; set; } = null!;
    public string Title { get; set; } = null!;
    public bool IsFavorite { get; set; }
    public WatchStatus? Status { get; set; }
}
