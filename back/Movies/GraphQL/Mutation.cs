using DBContext;
using Models;

namespace GraphQL;

public class Mutation
{
    public Movie CreateMovie(Movie movie, [Service] Context ctx)
    {
        ctx.Add(movie);
        ctx.SaveChangesAsync();
        return movie;
    }

    public Movie UpdateMovie(Movie movie, [Service] Context ctx)
    {
        ctx.Update(movie);
        ctx.SaveChanges();
        return movie;
    }

    public Movie DeleteMovie(Movie movie, [Service] Context ctx)
    {
        ctx.Remove(movie);
        ctx.SaveChanges();
        return movie;
    }

    public Genre CreateGenre(Genre genre, [Service] Context ctx)
    {
        ctx.Add(genre);
        ctx.SaveChangesAsync();
        return genre;
    }
    public Country CreateCountry(Country country, [Service] Context ctx)
    {
        ctx.Add(country);
        ctx.SaveChangesAsync();
        return country;
    }
    public Language CreateLanguage(Language language, [Service] Context ctx)
    {
        ctx.Add(language);
        ctx.SaveChangesAsync();
        return language;
    }
    public Models.Tag CreateTag(Models.Tag tag, [Service] Context ctx)
    {
        ctx.Add(tag);
        ctx.SaveChangesAsync();
        return tag;
    }
    public ProductionCompany CreateProductionCompany(ProductionCompany productionCompany, [Service] Context ctx)
    {
        ctx.Add(productionCompany);
        ctx.SaveChangesAsync();
        return productionCompany;
    }
}