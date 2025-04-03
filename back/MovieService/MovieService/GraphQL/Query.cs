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
        return ctx.People;
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
        return ctx.ProductionCompanies;
    }
}

