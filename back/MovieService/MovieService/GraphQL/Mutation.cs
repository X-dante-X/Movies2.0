using DBContext;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using MovieService.Services.Interfaces;
using Tag = Models.Tag;


namespace GraphQL;

public class Mutation
{
    public async Task<MovieDTO> CreateMovie(MovieDTO movieDTO, [Service] Context ctx, [Service] IUploadService uploadService)
    {
        var tags = await ctx.Tags.Where(t => movieDTO.Tags.Contains(t.TagId)).ToListAsync();

        var genres = await ctx.Genres.Where(g => movieDTO.Genre.Contains(g.GenreId)).ToListAsync();

        var releaseDate = movieDTO.ReleaseDate ?? DateTime.Now;
        var movieFileName = movieDTO.Title + releaseDate.Year + releaseDate.Month + releaseDate.Day;

        var moviePath = await uploadService.UploadMovieAsync(movieFileName, movieDTO.Movie);
        var posterPath = await uploadService.UploadPosterAsync(movieFileName, movieDTO.Poster);
        var backdropPath = await uploadService.UploadBackdropAsync(movieFileName, movieDTO.Backdrop);


        var movie = new Movie()
        {
            Title = movieDTO.Title,
            ReleaseDate = movieDTO.ReleaseDate,
            Budget = movieDTO.Budget,
            Description = movieDTO.Description,
            Popularity = movieDTO.Popularity,
            Runtime = movieDTO.Runtime,
            MovieStatus = movieDTO.MovieStatus,
            VoteAverage = movieDTO.VoteAverage,
            VoteCount = movieDTO.VoteCount,
            PEGI = movieDTO.PEGI,
            MoviePath = moviePath,
            PosterPath = posterPath,
            BackdropPath = backdropPath,
            ProductionCompanyId = movieDTO.ProductionCompanyId,
            CountryId = movieDTO.CountryId,
            LanguageId = movieDTO.LanguageId,
            Genre = genres??= [],
            Tags = tags??= [],
        };


        await ctx.Movies.AddAsync(movie);
        await ctx.SaveChangesAsync();

        if (movieDTO.movieCasts != null)
        {
            List<MovieCast> moviecasts = [];
            foreach(var moviecast in movieDTO.movieCasts)
            {
                moviecasts.Add(new MovieCast()
                {
                    MovieId = movie.MovieId,
                    PersonId = moviecast.PersonId,
                    Job = moviecast.Job,
                    CharacterGender = moviecast.CharacterGender,
                    CharacterName = moviecast.CharacterName,
                });
            }

            await ctx.MovieCasts.AddRangeAsync(moviecasts);
            await ctx.SaveChangesAsync();
        }

        return movieDTO;
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

    public async Task<PersonDTO> CreatePerson(PersonDTO personDTO, [Service] Context ctx, [Service] IUploadService uploadService)
    {
        var photoName = personDTO.PersonName + personDTO.DateOfBirth.Year + personDTO.DateOfBirth.Month + personDTO.DateOfBirth.Day;

        var photoPath = await uploadService.UploadPersonPhotoAsync(photoName, personDTO.Photo);

        var person = new Person()
        {
            PersonName = personDTO.PersonName,
            Gender = personDTO.Gender,
            PhotoPath = photoPath,
            DateOfBirth = personDTO.DateOfBirth,
            CountryId = personDTO.CountryId,
            Biography = personDTO.Biography,
        };

        ctx.People.Add(person);
        await ctx.SaveChangesAsync();
        return personDTO;
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
    public async Task<ProductionCompanyDTO> CreateProductionCompany(ProductionCompanyDTO productionCompanyDTO, [Service] Context ctx, [Service] IUploadService uploadService)
    {
        var logoName = productionCompanyDTO.CompanyName;

        var logoPath = await uploadService.UploadLogoAsync(logoName, productionCompanyDTO.Logo);

        var productionCompany = new ProductionCompany()
        {
            CompanyName = productionCompanyDTO.CompanyName,
            LogoPath = logoPath,
            CountryId = productionCompanyDTO.CountryId,
        };

        ctx.Add(productionCompany);
        await ctx.SaveChangesAsync();
        return productionCompanyDTO;
    }
}