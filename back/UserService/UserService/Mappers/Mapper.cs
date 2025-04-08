using UserService.Models;
using UserService.Models.Enums;

namespace UserService.Mappers
{
    public static class Mapper
    {
        public static UserFavoriteMovie MovieResponseToMovieFavorite(MovieResponse movie, WatchStatus? status, bool isFavorite)
        {
            return new UserFavoriteMovie()
            {
                Title = movie.Title,
                Description = movie.Description,
                PosterPath = movie.PosterPath,
                IsFavorite = isFavorite,
                Status = status
            };
        }
    }
}
