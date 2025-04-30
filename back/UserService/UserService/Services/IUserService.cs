using UserService.Models.Enums;
using UserService.Models;

namespace UserService.Services;

public interface IUserService
{
    Task<List<UserFavoriteMovie>> GetUserFavoritesAsync(string userId);
    Task<List<UserMovieDto>> GetUserMoviesByStatusAsync(string userId, WatchStatus status);
    Task<UserMovieDto> AddOrUpdateUserMovieAsync( UserMovieDto userMovieDto);
    Task ToggleFavoriteAsync(string userId, int movieId);
    Task UpdateWatchStatusAsync(string userId, int movieId, WatchStatus status);
    Task<List<UserFavoriteMovie>> GetAllUserMovies(string userId);
    Task<bool> DeleteFavoriteMovieAsync(UserMovieDeleteDTO movie); 
    Task<List<MovieReviewDto>> GetUserReviewsAsync(string userId);
    Task<MovieReviewDto> GetMovieReviewAsync(string userId, int movieId);
    Task<MovieReviewDto> AddOrUpdateReviewAsync(MovieReviewDto reviewDto);
    Task DeleteReviewAsync(string userId, int movieId);

    Task<double> GetMovieAverageRatingAsync(int movieId);
    Task<List<MovieReviewDto>> GetMovieReviewsAsync(int movieId);

}
