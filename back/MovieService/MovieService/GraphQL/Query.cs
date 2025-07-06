using DBContext;
using Models;
using Tag = Models.Tag;


namespace GraphQL;

public class Query
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Movie> Movies([Service] Context ctx)
    {
        return ctx.Movies;
    }

    [UsePaging]
    [UseProjection]
    public IQueryable<Movie> FindMoviesByTitle([Service] Context ctx, string partOfTitle)
    {
        return ctx.Movies
                  .Where(m => m.Title.ToLower().Contains(partOfTitle.ToLower()));
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Country> Countries([Service] Context ctx)
    {
        return ctx.Countries;
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Person> People([Service] Context ctx)
    {
        return ctx.People
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
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<MovieCast> MovieCasts([Service] Context ctx)
    {
        return ctx.MovieCasts;
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Genre> Genres([Service] Context ctx)
    {
        return ctx.Genres;
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Language> Languages([Service] Context ctx)
    {
        return ctx.Languages;
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Tag> Tags([Service] Context ctx)
    {
        return ctx.Tags;
    }

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

