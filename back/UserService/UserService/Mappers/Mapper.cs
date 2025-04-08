using UserService.Models;
using UserService.Models.Enums;

namespace UserService.Mappers;

public static class Mapper
{
    public static UserFavoriteMovie MovieResponseToMovieFavorite(MovieResponse movie, WatchStatus status, bool isFavorite)
    {
<<<<<<< HEAD
        public static UserFavoriteMovie MovieResponseToMovieFavorite(MovieResponse movie, WatchStatus? status, bool isFavorite)
=======
        return new UserFavoriteMovie()
>>>>>>> 13caaae709cd760fddea1627fdfa01ffabd4b709
        {
            Title = movie.Title,
            Description = movie.Description,
            PosterPath = movie.PosterPath,
            IsFavorite = isFavorite,
            Status = status
        };
    }
}
