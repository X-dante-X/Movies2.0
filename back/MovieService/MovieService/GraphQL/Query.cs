using DBContext;
using Models;
using Tag = Models.Tag;

namespace GraphQL;

/// <summary>
/// GraphQL query root that exposes read access to domain data.
/// Includes Movies, People, Countries, Tags, Genres, Languages and ProductionCompanies.
/// Supports filtering, sorting, projections and paging (where appropriate).
/// </summary>
public class Query
{
    /// <summary>
    /// Returns all movies with support for paging, filtering, sorting and projection.
    /// </summary>
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Movie> Movies([Service] Context ctx)
        => ctx.Movies;

    /// <summary>
    /// Finds movies containing the specified text in their title.
    /// Supports paging and projection.
    /// </summary>
    [UsePaging]
    [UseProjection]
    public IQueryable<Movie> FindMoviesByTitle([Service] Context ctx, string partOfTitle)
        => ctx.Movies
              .Where(m => m.Title.ToLower().Contains(partOfTitle.ToLower()));

    /// <summary>
    /// Returns all countries with filtering, sorting and projection.
    /// </summary>
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Country> Countries([Service] Context ctx)
        => ctx.Countries;

    /// <summary>
    /// Returns all people, projecting their top 12 most popular movies as filmography.
    /// Supports filtering, sorting and projection.
    /// </summary>
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Person> People([Service] Context ctx)
        => ctx.People
            .Select(p => new Person
            {
                PersonId = p.PersonId,
                PersonName = p.PersonName,
                Gender = p.Gender,
                PhotoPath = p.PhotoPath,
                DateOfBirth = p.DateOfBirth,
                CountryId = p.CountryId,
                Nationality = p.Nationality,
                Biography = p.Biography,
                Filmography = ctx.MovieCasts
                    .Where(mc => mc.PersonId == p.PersonId)
                    .OrderByDescending(mc => mc.Movie.Popularity)
                    .Select(mc => mc.Movie)
                    .Take(12)
                    .ToList()
            });

    /// <summary>
    /// Returns all movie casts with filtering, sorting and projection.
    /// </summary>
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<MovieCast> MovieCasts([Service] Context ctx)
        => ctx.MovieCasts;

    /// <summary>
    /// Returns all genres with filtering, sorting and projection.
    /// </summary>
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Genre> Genres([Service] Context ctx)
        => ctx.Genres;

    /// <summary>
    /// Returns all languages with filtering, sorting and projection.
    /// </summary>
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Language> Languages([Service] Context ctx)
        => ctx.Languages;

    /// <summary>
    /// Returns all tags with filtering, sorting and projection.
    /// </summary>
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Tag> Tags([Service] Context ctx)
        => ctx.Tags;

    /// <summary>
    /// Returns all production companies with their top 12 most popular movies in filmography.
    /// Supports filtering, sorting and projection.
    /// </summary>
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ProductionCompany> ProductionCompanies([Service] Context ctx)
    {
        return ctx.ProductionCompanies
            .Select(pc => new ProductionCompany
            {
                CompanyId = pc.CompanyId,
                CompanyName = pc.CompanyName,
                LogoPath = pc.LogoPath,
                CountryId = pc.CountryId,
                Country = pc.Country,
                Filmography = ctx.Movies
                    .Where(m => m.ProductionCompanyId == pc.CompanyId)
                    .OrderByDescending(m => m.Popularity)
                    .Take(12)
                    .ToList()
            });
    }
}
