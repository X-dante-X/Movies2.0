using MoviesCore.Models;

namespace MoviesCore.Interfaces;

public interface IUserService
{
    Task<(IQueryable<Movie?> movies, int lastPage)> GetMovies(int[]? genresId, int[]? KeywordsId, int page);
    Task<Movie?> GetMovieById(int movieId);
    Task<IQueryable<Movie?>> GetMoviesByTitleFragment(string titleFragment);

    Task<IQueryable<MovieCast?>> GetMovieCastByMovieId(int movieId);
    Task<IQueryable<MovieCast?>> GetMovieCastByActorId(int actorId);
    Task<IQueryable<MovieCrew?>> GetMovieCrewByMovieId(int movieId);
    Task<ProductionCompany?> GetProductionCompanyId(int productionCompanyId);
}
