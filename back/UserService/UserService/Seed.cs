using UserService.Context;
using UserService.Models;
using UserService.Models.Enums;
using UserService.Services;

namespace UserService
{
    public static class SeedUserData
    {
        public static void Seed(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (!context.UserMovies.Any())
            {
                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                var favoriteMovie1 = new UserMovie()
                {
                    UserId = "1",
                    MovieId = 1,
                    IsFavorite = true,
                    Status = WatchStatus.Watching,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                var favoriteMovie2 = new UserMovie()
                {
                    UserId = "2",
                    MovieId = 2,
                    IsFavorite = true,
                    Status = WatchStatus.Watching,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                var userReview1 = new MovieReview
                {
                    UserId = "1",
                    MovieId = 1,
                    Rating = 8,
                    Comment = "Great movie",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                var userReview2 = new MovieReview
                {
                    UserId = "2",
                    MovieId = 1,
                    Rating = 9,
                    Comment = "Masterpiece",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                context.UserMovies.Add(favoriteMovie1);
                context.UserMovies.Add(favoriteMovie2);
                context.MovieReviews.Add(userReview1);
                context.MovieReviews.Add(userReview2);
                context.SaveChanges();
            }
        }
    }
}
