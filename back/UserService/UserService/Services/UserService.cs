using UserService.Models;
using UserService.Models.Enums;
using UserService.Context;
using Microsoft.EntityFrameworkCore;
using Npgsql.PostgresTypes;


namespace UserService.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    public UserService(AppDbContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Creates a new  review entry for a user and the movie if none exists;
    /// otherwise updates the rating, comment and timestamp.
    /// </summary>
    public async Task<MovieReviewDto> AddOrUpdateReviewAsync(MovieReviewDto reviewDto)
    {
        if (reviewDto.Rating < 1 || reviewDto.Rating > 10)
        {
            throw new ArgumentOutOfRangeException(nameof(reviewDto.Rating), "Rating must be between 1 and 10");
        }

        var existingReview = await _context.MovieReviews
            .FirstOrDefaultAsync(r => r.UserId == reviewDto.UserId && r.MovieId == reviewDto.MovieId);

        if (existingReview == null)
        {
            var newReview = new MovieReview
            {
                UserId = reviewDto.UserId,
                MovieId = reviewDto.MovieId,
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.MovieReviews.Add(newReview);
        }
        else
        {
            existingReview.Rating = reviewDto.Rating;
            existingReview.Comment = reviewDto.Comment;
            existingReview.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return reviewDto;
    }
    /// <summary>
    /// Retrieves the list of reviews based on the provided movieId.
    /// </summary>
    public async Task<List<UserMovieReview>> GetAllReviewsForMovie (int movieId)
    {
        var movieReviews = await _context.MovieReviews.Where(x => x.MovieId == movieId).ToListAsync();
        foreach (var movieReview in movieReviews)
        {
            Console.WriteLine($"{movieReview.Id} {movieReview.UserId} {movieReview.Comment}");
        }
        var userIds = movieReviews.Select(x => x.UserId).ToList();
        var usernames = await RabbitMqService.GetUserNameById(userIds);

        var userMovieReviews = movieReviews.Select(review => new UserMovieReview
        {
            UserName = usernames.FirstOrDefault(u => u.Id == review.UserId)?.UserName ?? "Unknown User",
            Comment = review.Comment
        }).ToList();

        return userMovieReviews;
    }
    /// <summary>
    /// Retrieves the list of reviews based on the provided UserId along with the rating for the given movie.
    /// </summary>
    public async Task<List<MovieReviewDto>> GetUserReviews(string userId)
    {
        var reviews = await _context.MovieReviews
            .Where(x => x.UserId == userId)
            .Select(x => new MovieReviewDto
            {
                UserId = x.UserId,
                Comment = x.Comment,
                MovieId = x.MovieId,
                Rating = x.Rating,
            })
            .ToListAsync();

        return reviews;
    }
    /// <summary>
    /// Adds new entry of the movie to the user favorites list or updates the existing entry's watch status and flag.
    /// Creates a new record if none exists, else updates the existing and refreshes timestamps.
    /// </summary>

    public async Task<UserMovieDto> AddOrUpdateUserMovieAsync(UserMovieDto userMovieDto)
    {
        var existingUserMovie = await _context.UserMovies
            .FirstOrDefaultAsync(um => um.UserId == userMovieDto.UserId && um.MovieId == userMovieDto.MovieId);
        if (existingUserMovie == null)
        {
            var newUserMovie = new UserMovie
            {
                UserId = userMovieDto.UserId,
                MovieId = userMovieDto.MovieId,
                IsFavorite = userMovieDto.IsFavorite,
                Status = userMovieDto.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.UserMovies.Add(newUserMovie);
            await _context.SaveChangesAsync();
        }
        else
        {
            existingUserMovie.IsFavorite = userMovieDto.IsFavorite;
            existingUserMovie.Status = userMovieDto.Status;
            existingUserMovie.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        return userMovieDto;
    }
    /// <summary>
    /// Retrieves the watch status of a specific movie for the given user. 
    /// Returns the corresponding <see cref="WatchStatus"/> value, or null if 
    /// no record exists for the user and movie.
    /// </summary>

    public async Task<WatchStatus?> GetUsersWatchStatusAsync(int movieId, string userId)
    {
        var userMovie = await _context.UserMovies
            .FirstOrDefaultAsync(um => um.MovieId == movieId && um.UserId == userId);

        if (userMovie == null)
        {
            return null;
        }

        return userMovie.Status;
    }
    /// <summary>
    /// Deletes a user's review entry  based on the provided
    /// user and movie identifiers.
    /// </summary>

    public async Task DeleteReviewAsync(string userId, int movieId)
    {
        var review = await _context.MovieReviews
                        .FirstOrDefaultAsync(r => r.UserId == userId && r.MovieId == movieId);

        if (review != null)
        {
            _context.MovieReviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Deletes a user's movie entry (favorite or watch status) based on the provided
    /// user and movie identifiers. Returns true if the record was found and removed.
    /// </summary>

    public async Task<bool> DeleteFavoriteMovieAsync(UserMovieDeleteDTO deleteDTO)
    {
        var review = await _context.UserMovies
                        .FirstOrDefaultAsync(r => r.UserId == deleteDTO.UserId && r.MovieId == deleteDTO.MovieId);

        if (review != null)
        {
            _context.UserMovies.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public Task<double> GetMovieAverageRatingAsync(int movieId)
    {
        throw new NotImplementedException();
    }

    public Task<MovieReviewDto> GetMovieReviewAsync(string userId, int movieId)
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// Gets the reviews of the movie from the database based on the provided movieId
    /// </summary>

    public Task<List<MovieReviewDto>> GetMovieReviewsAsync(int movieId)
    {
        var reviews = _context.MovieReviews
        .Where(x => x.MovieId == movieId).Select(x => new MovieReviewDto
        {
            UserId = x.UserId,
            MovieId = x.MovieId,
            Comment = x.Comment,    
            Rating = x.Rating,
        }).ToListAsync();
        return reviews;
    } 
    /// <summary>
    /// returns the list of favorite movies added by the user based on the userId
    /// </summary>
    public async Task<List<UserFavoriteMovie>> GetUserFavoritesAsync(string userId)
    {
        var favorites = await _context.UserMovies
            .Where(um => um.UserId == userId && um.IsFavorite)
                .Select(um => new
                {
                    um.MovieId,
                    um.Status,
                    um.IsFavorite
                }).ToListAsync();

        var movieIds = favorites.Select(f => f.MovieId).ToList();
        var movies = await RabbitMqService.GetMovieById(movieIds);
        List<UserFavoriteMovie> favoriteMovies = favorites
                .Join(
                    movies,
                    fav => fav.MovieId,
                    movie => movie.Id, 
                    (fav, movie) => Mappers.Mapper.MovieResponseToMovieFavorite(movie, fav.Status, fav.IsFavorite)
                ).ToList();
           return favoriteMovies;
    }

    /// <summary>
    /// Retrieves all movies associated with a user, including their watch status 
    /// and favorite flag. Fetches movie details from an external service and maps 
    /// them into a list of <see cref="UserFavoriteMovie"/> objects.
    /// </summary>

    public async Task<List<UserFavoriteMovie>> GetAllUserMovies(string userId)
    {

        var favorites = await _context.UserMovies
        .Where(um => um.UserId == userId)
        .Select(um => new
        {
            um.MovieId,
            um.Status,
            um.IsFavorite
        })
        .ToListAsync();
        var movieIds = favorites.Select(f => f.MovieId).ToList();
        var movies = await RabbitMqService.GetMovieById(movieIds);
        List<UserFavoriteMovie> favoriteMovies = favorites
        .Join(
            movies,
            fav => fav.MovieId,
            movie => movie.Id,
            (fav, movie) => Mappers.Mapper.MovieResponseToMovieFavorite(movie, fav.Status, fav.IsFavorite)
        )
        .ToList();
        return favoriteMovies;
    }

    /// <summary>
    /// Retrieves all movies for a user filtered by the specified watch status, 
    /// returning a list of simplified movie DTOs containing movie ID, status, 
    /// and favorite information.
    /// </summary>

    public async Task<List<UserMovieDto>> GetUserMoviesByStatusAsync(string userId, WatchStatus status)
    {
        var movies = await _context.UserMovies
            .Where(um => um.UserId == userId && um.Status == status)
            .Select(um => new UserMovieDto
            {
                MovieId = um.MovieId,
                IsFavorite = um.IsFavorite,
                Status = status
            })
            .ToListAsync();

        return movies;
    }

    public Task<List<MovieReviewDto>> GetUserReviewsAsync(string userId)
    {
        throw new NotImplementedException();
    }


    public Task ToggleFavoriteAsync(string userId, int movieId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Updates the watch status of a user's movie. If the movie entry does not exist,
    /// a new record is created with the specified status. Otherwise, the existing
    /// record is updated and timestamp refreshed.
    /// </summary>

    public async Task UpdateWatchStatusAsync(string userId, int movieId, WatchStatus status)
    {
        var userMovie = await _context.UserMovies
        .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);

        if (userMovie == null)
        {
            userMovie = new UserMovie
            {
                UserId = userId,
                MovieId = movieId,
                Status = status,
                IsFavorite = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.UserMovies.Add(userMovie);
        }
        else
        {
            userMovie.Status = status;
            userMovie.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
    }
}
