using Microsoft.EntityFrameworkCore;
using MoviesCore.Interfaces;
using MoviesCore.Models;
using System.Linq;

namespace Infrastructure.Service;

public class AdminService : IAdminService
{
    private readonly MoviesDbContext _context;

    public AdminService(MoviesDbContext context)
    {
        _context = context;
    }

    public async Task<Movie> AddMovie(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task<Person> AddPerson(Person person)
    {
        _context.People.Add(person);
        await _context.SaveChangesAsync();
        return person;
    }

    public async Task<ProductionCompany> AddProductionCompany(ProductionCompany productionCompany)
    {
        _context.ProductionCompanies.Add(productionCompany);
        await _context.SaveChangesAsync();
        return productionCompany;
    }

    public async Task DeleteMovie(int movieId)
    {
        var movie = await _context.Movies.FindAsync(movieId);
        if (movie != null)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeletePerson(int personId)
    {
        var person = await _context.People.FindAsync(personId);
        if (person != null)
        {
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteProductionCompany(int productionCompanyId)
    {
        var productionCompany = await _context.ProductionCompanies.FindAsync(productionCompanyId);
        if (productionCompany != null)
        {
            _context.ProductionCompanies.Remove(productionCompany);
            await _context.SaveChangesAsync();
        }
    }

    public async Task SetGenresToMovie(int movieId, int[] genresId)
    {
        var movie = await _context.Movies.FindAsync(movieId);
        if (movie != null)
        {
            var existingGenres = await _context.MovieGenres.Where(mg => mg.MovieId == movieId).ToListAsync();
            _context.MovieGenres.RemoveRange(existingGenres);

            var genresToAdd = await _context.Genres.Where(g => genresId.Contains(g.GenreId)).ToListAsync();
            var newMovieGenres = genresToAdd.Select(genre => new MovieGenre { MovieId = movieId, GenreId = genre.GenreId }).ToList();
            movie.MovieGenres = newMovieGenres;

            await _context.SaveChangesAsync();
        }
    }

    public async Task SetKeywordsToMovie(int movieId, int[] keywordsId)
    {
        var movie = await _context.Movies.FindAsync(movieId);
        if (movie != null)
        {
            var existingKeywords = await _context.MovieKeywords.Where(mk => mk.MovieId == movieId).ToListAsync();
            _context.MovieKeywords.RemoveRange(existingKeywords);

            var keywordsToAdd = await _context.Keywords.Where(k => keywordsId.Contains(k.KeywordId)).ToListAsync();
            movie.MovieKeywords = keywordsToAdd.Select(keyword => new MovieKeyword { MovieId = movieId, KeywordId = keyword.KeywordId }).ToList();
            await _context.SaveChangesAsync();
        }
    }

    public async Task SetProductionCountriesToMovie(int movieId, int[] productionCountriesId)
    {
        var movie = await _context.Movies.FindAsync(movieId);
        if (movie != null)
        {
            var existingProductionCountries = await _context.ProductionCountries.Where(pc => pc.MovieId == movieId).ToListAsync();
            _context.ProductionCountries.RemoveRange(existingProductionCountries);

            var countriesToAdd = await _context.Countries.Where(pc => productionCountriesId.Contains(pc.CountryId)).ToListAsync();
            movie.ProductionCountries = countriesToAdd.Select(country => new ProductionCountry { MovieId = movieId, CountryId = country.CountryId }).ToList(); ;
            await _context.SaveChangesAsync();
        }
    }
    public async Task SetMoviesToProdactionCompany(int productionCompanyId, int[] moviesId)
    {
        var prodactionCompany = await _context.ProductionCompanies.FindAsync(productionCompanyId);
        if(prodactionCompany != null) 
        {
            var existingMovies = await _context.MovieCompanies.Where(mc => mc.CompanyId == productionCompanyId).ToListAsync();
            _context.MovieCompanies.RemoveRange(existingMovies);

            var moviesToAdd = await _context.Movies.Where(m => moviesId.Contains(m.MovieId)).ToListAsync();
            prodactionCompany.MovieCompanies = moviesToAdd.Select(movie => new MovieCompany {CompanyId = productionCompanyId, MovieId = movie.MovieId }).ToList();
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Movie> UpdateMovie(Movie movie)
    {
        _context.Entry(movie).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task<Person> UpdatePerson(Person person)
    {
        _context.Entry(person).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return person;
    }

    public async Task<ProductionCompany> UpdateProductionCompany(ProductionCompany productionCompany)
    {
        _context.Entry(productionCompany).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return productionCompany;
    }

}

