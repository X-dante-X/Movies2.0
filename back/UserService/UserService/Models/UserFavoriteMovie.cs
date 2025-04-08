using UserService.Models.Enums;

namespace UserService.Models
{
    public class UserFavoriteMovie
    {
        public string Description { get; set; }
        public string PosterPath { get; set; }
        public string Title { get; set; }
        public bool IsFavorite { get; set; }
        public WatchStatus? Status { get; set; }
    }
}
