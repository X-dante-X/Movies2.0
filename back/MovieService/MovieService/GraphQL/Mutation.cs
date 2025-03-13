using DBContext;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using Tag = Models.Tag;


namespace GraphQL;

public class Mutation
{
    public async Task<Movie> CreateMovie(Movie movie, [Service] Context ctx)
    {
        var country = await ctx.Countries.FirstOrDefaultAsync(c => c.CountryId == movie.ProductionCountry.CountryId);
        if (country != null)
        {
            movie.ProductionCountry = country;
        }

        var language = await ctx.Languages.FirstOrDefaultAsync(c => c.LanguageId == movie.ProductionLanguage.LanguageId);
        if (language != null)
        {
            movie.ProductionLanguage = language;
        }

        var productionCompany = await ctx.ProductionCompanies.FirstOrDefaultAsync(c => c.CompanyId == movie.ProductionCompany!.CompanyId);
        if (productionCompany != null)
        {
            movie.ProductionCompany = productionCompany;
        }

        var tags = await ctx.Tags.Where(t => movie.Tags.Select(tag => tag.TagId).Contains(t.TagId)).ToListAsync();
        if (tags.Any())
        {
            movie.Tags = tags;
        }

        var genres = await ctx.Genres.Where(g => movie.Genre.Select(genre => genre.GenreId).Contains(g.GenreId)).ToListAsync();
        if (genres.Any())
        {
            movie.Genre = genres;
        }

        ctx.Add(movie);
        await ctx.SaveChangesAsync();

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

    public Person CreatePerson(Person person, [Service] Context ctx)
    {
        var country = ctx.Countries.FirstOrDefault(c => c.CountryId == person.Nationality.CountryId);
        if (country != null)
        {
            ctx.Attach(country);
            person.Nationality = country;
        }
        ctx.Add(person);
        ctx.SaveChangesAsync();
        return person;
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
    public Tag CreateTag(Tag tag, [Service] Context ctx)
    {
        ctx.Add(tag);
        ctx.SaveChangesAsync();
        return tag;
    }
    public ProductionCompany CreateProductionCompany(ProductionCompany productionCompany, [Service] Context ctx)
    {
        var country = ctx.Countries.FirstOrDefault(c => c.CountryId == productionCompany.Country.CountryId);
        if (country != null)
        {
            ctx.Attach(country);
            productionCompany.Country = country;
        }
        ctx.Add(productionCompany);
        ctx.SaveChangesAsync();
        return productionCompany;
    }
}