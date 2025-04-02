using UserService.Models.Enums;
using UserService.Models;

namespace UserService.Services
{
    public interface IUserService
    {
        Task<List<UserMovieDto>> GetUserMoviesAsync(string userId);
        Task<List<UserMovieDto>> GetUserFavoritesAsync(string userId);
        Task<List<UserMovieDto>> GetUserMoviesByStatusAsync(string userId, WatchStatus status);
        Task<UserMovieDto> GetUserMovieAsync(string userId, int movieId);
        Task<UserMovieDto> AddOrUpdateUserMovieAsync(string userId, UserMovieDto userMovieDto);
        Task ToggleFavoriteAsync(string userId, int movieId);
        Task UpdateWatchStatusAsync(string userId, int movieId, WatchStatus status);
        Task RemoveUserMovieAsync(string userId, int movieId);

        Task<List<MovieReviewDto>> GetUserReviewsAsync(string userId);
        Task<MovieReviewDto> GetMovieReviewAsync(string userId, int movieId);
        Task<MovieReviewDto> AddOrUpdateReviewAsync(string userId, MovieReviewDto reviewDto);
        Task DeleteReviewAsync(string userId, int movieId);

        Task<double> GetMovieAverageRatingAsync(int movieId);
        Task<List<MovieReviewDto>> GetMovieReviewsAsync(int movieId);

    }
}
