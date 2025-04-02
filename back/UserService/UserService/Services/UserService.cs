using UserService.Models;
using UserService.Models.Enums;
using UserService.Context;
using Microsoft.EntityFrameworkCore;


namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<MovieReviewDto> AddOrUpdateReviewAsync(string userId, MovieReviewDto reviewDto)
        {
            if (reviewDto.Rating < 1 || reviewDto.Rating > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(reviewDto.Rating), "Rating must be between 1 and 10");
            }

            var existingReview = await _context.MovieReviews
                .FirstOrDefaultAsync(r => r.UserId == userId && r.MovieId == reviewDto.MovieId);

            if (existingReview == null)
            {
                var newReview = new MovieReview
                {
                    UserId = userId,
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

        public async Task<UserMovieDto> AddOrUpdateUserMovieAsync(string userId, UserMovieDto userMovieDto)
        {
            var existingUserMovie = await _context.UserMovies
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == userMovieDto.MovieId);
            if (existingUserMovie == null)
            {
                var newUserMovie = new UserMovie
                {
                    UserId = userId,
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
            throw new NotImplementedException();
        }

        public async Task<List<UserMovieDto>> GetUserFavoritesAsync(string userId)
        {
            var favorites = await _context.UserMovies
                .Where(um => um.UserId == userId && um.IsFavorite)
                .Select(um => new UserMovieDto
                {
                    MovieId = um.MovieId,
                    IsFavorite = true,
                    Status = um.Status
                })
                .ToListAsync();

            return favorites;
        }

        public Task<UserMovieDto> GetUserMovieAsync(string userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserMovieDto>> GetUserMoviesAsync(string userId)
        {
            throw new NotImplementedException();
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

        public Task RemoveUserMovieAsync(string userId, int movieId)
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
}
