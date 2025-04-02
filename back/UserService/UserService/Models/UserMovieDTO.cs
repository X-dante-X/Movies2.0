using UserService.Models.Enums;

namespace UserService.Models
{
    public class UserMovieDto
    {
        public int MovieId { get; set; }
        public bool IsFavorite { get; set; }
        public WatchStatus? Status { get; set; }
    }
}
