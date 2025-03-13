using Microsoft.EntityFrameworkCore;
using MoviesCore.Interfaces;
using MoviesCore.Models;
using System.Linq;

namespace Infrastructure.Service;

public class UserService: IUserService
{
    private readonly MoviesDbContext _context;
    private const int pageSize = 10;

    public UserService(MoviesDbContext context)
    {
        _context = context;
    }

    public async Task<IQueryable<MovieCast?>> GetMovieCastByActorId(int actorId)
    {
        return _context.MovieCasts.Where(mc => mc.PersonId == actorId)
            .Include(mc => mc.Movie)
            .Include(mc => mc.Gender)
            .Include(mc => mc.Person);
    }

    public async Task<(IQueryable<Movie?> movies, int lastPage)> GetMovies(int[]? genresId, int[]? keywordsId, int page)
    {
        int skipAmount = (page - 1) * pageSize;

        IQueryable<Movie?> query = _context.Movies
            .Include(m => m.MovieCompanies).ThenInclude(mc => mc.ProductionCompany)
            .Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre)
            .Include(m => m.ProductionCountries).ThenInclude(pc => pc.Country)
            .Include(m => m.MovieKeywords).ThenInclude(mk => mk.Keyword)
            .Include(m => m.MovieLanguages).ThenInclude(ml => ml.Language);

        if (genresId != null && genresId.Length > 0)
        {
            foreach (var genreId in genresId)
            {
                query = query.Where(m => m.MovieGenres.Any(mg => mg.GenreId == genreId));
            }
        }

        if (keywordsId != null && keywordsId.Length > 0)
        {
            foreach (var keywordId in keywordsId)
            {
                query = query.Where(m => m.MovieKeywords.Any(mk => mk.KeywordId == keywordId));
            }
        }


        int totalCount = await query.CountAsync();

        int lastPage = (int)Math.Ceiling((double)totalCount / pageSize);

        query = query.Skip(skipAmount).Take(pageSize);

        return (query, lastPage);
    }

    public async Task<Movie?> GetMovieById(int movieId)
    {
        return await _context.Movies
            .Include(m => m.MovieCompanies).ThenInclude(mc => mc.ProductionCompany)
            .Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre)
            .Include(m => m.ProductionCountries).ThenInclude(pc => pc.Country)
            .Include(m => m.MovieKeywords).ThenInclude(mk => mk.Keyword)
            .Include(m => m.MovieLanguages).ThenInclude(ml => ml.Language)
            .FirstOrDefaultAsync(m => m.MovieId == movieId);
    }


    public async Task<IQueryable<Movie>> GetMoviesByTitleFragment(string titleFragment)
    {
        return _context.Movies.Take(pageSize)
            .Where(m => m.Title.Contains(titleFragment));
    }

    public async Task<IQueryable<MovieCast?>> GetMovieCastByMovieId(int movieId)
    {

        return _context.MovieCasts.Where(mc => mc.MovieId == movieId)
            .Include(mc => mc.Movie)
            .Include(mc => mc.Gender)
            .Include(mc => mc.Person);
    }

    public async Task<IQueryable<MovieCrew?>> GetMovieCrewByMovieId(int movieId)
    {
        return _context.MovieCrews.Where(mc => mc.MovieId == movieId)
            .Include(mc => mc.Movie)
            .Include(mc => mc.Department)
            .Include(mc => mc.Person);
    }

    public async Task<ProductionCompany?> GetProductionCompanyId(int productionCompanyId)
    {
        return await _context.ProductionCompanies
            .Include(pc => pc.MovieCompanies).ThenInclude(mc => mc.Movie)
            .FirstOrDefaultAsync(pc => pc.CompanyId == productionCompanyId);
    }

}
