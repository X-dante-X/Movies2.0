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

    public async Task<bool> DeleteFavoriteMovieAsync(UserMovieDeleteDTO deleteDTO)
    {
        var review = await _context.MovieReviews
                        .FirstOrDefaultAsync(r => r.UserId == deleteDTO.UserId && r.MovieId == deleteDTO.MovieId);

        if (review != null)
        {
            _context.MovieReviews.Remove(review);
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

        public async Task<List<UserFavoriteMovie>> GetUserFavoritesAsync(string userId)
        {

            var favorites = await _context.UserMovies
            .Where(um => um.UserId == userId && um.IsFavorite)
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
