using DBContext;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using MovieService.Helpers;
using MovieService.Services.Interfaces;
using Tag = Models.Tag;

namespace GraphQL;

/// <summary>
/// GraphQL mutation root for creating, updating and deleting domain entities.
/// Handles Movie, Person, Genre, Country, Language, Tag and ProductionCompany mutations.
/// </summary>
public class Mutation
{
    /// <summary>
    /// Creates a new movie, uploads media files (video, poster, backdrop),
    /// maps DTO to entity and persists it with related tags, genres and casts.
    /// </summary>
    public async Task<MovieDTO> CreateMovie(
        MovieDTO movieDTO,
        [Service] Context ctx,
        [Service] IUploadService uploadService)
    {
        // Resolve related tags and genres
        var tags = await ctx.Tags.Where(t => movieDTO.Tags.Contains(t.TagId)).ToListAsync();
        var genres = await ctx.Genres.Where(g => movieDTO.Genre.Contains(g.GenreId)).ToListAsync();

        // Upload files to storage via gRPC
        var movieFileName = ExtractFileName.ExtractMovieFileName(movieDTO);
        var moviePath = await uploadService.UploadVideoAsync(movieFileName, movieDTO.Movie);
        var posterPath = await uploadService.UploadImageAsync(movieFileName, "poster", movieDTO.Poster);
        var backdropPath = await uploadService.UploadImageAsync(movieFileName, "backdrop", movieDTO.Backdrop);

        // Map and save main movie entity
        var movie = Mapper.MovieDTOToMovie(movieDTO, moviePath, posterPath, backdropPath, genres, tags);
        await ctx.Movies.AddAsync(movie);
        await ctx.SaveChangesAsync();

        // Save cast if provided
        if (movieDTO.movieCasts != null)
        {
            var movieCasts = movieDTO.movieCasts
                .Select(c => Mapper.MovieCastDTOToMovieCast(c, movie.MovieId))
                .ToList();

            await ctx.MovieCasts.AddRangeAsync(movieCasts);
            await ctx.SaveChangesAsync();
        }

        return movieDTO;
    }

    /// <summary>
    /// Updates an existing movie entity.
    /// </summary>
    public Movie UpdateMovie(Movie movie, [Service] Context ctx)
    {
        ctx.Update(movie);
        ctx.SaveChanges();
        return movie;
    }

    /// <summary>
    /// Deletes an existing movie entity.
    /// </summary>
    public Movie DeleteMovie(Movie movie, [Service] Context ctx)
    {
        ctx.Remove(movie);
        ctx.SaveChanges();
        return movie;
    }

    /// <summary>
    /// Creates a new person entity and uploads photo.
    /// </summary>
    public async Task<PersonDTO> CreatePerson(
        PersonDTO personDTO,
        [Service] Context ctx,
        [Service] IUploadService uploadService)
    {
        var photoName = ExtractFileName.ExtractPhotoFileName(personDTO);
        var photoPath = await uploadService.UploadImageAsync(photoName, "personPhoto", personDTO.Photo);

        var person = Mapper.PersonDTOToPerson(personDTO, photoPath);
        ctx.People.Add(person);
        await ctx.SaveChangesAsync();

        return personDTO;
    }

    /// <summary>
    /// Creates a new genre entity.
    /// </summary>
    public Genre CreateGenre(Genre genre, [Service] Context ctx)
    {
        ctx.Add(genre);
        ctx.SaveChangesAsync();
        return genre;
    }

    /// <summary>
    /// Creates a new country entity.
    /// </summary>
    public Country CreateCountry(Country country, [Service] Context ctx)
    {
        ctx.Add(country);
        ctx.SaveChangesAsync();
        return country;
    }

    /// <summary>
    /// Creates a new language entity.
    /// </summary>
    public Language CreateLanguage(Language language, [Service] Context ctx)
    {
        ctx.Add(language);
        ctx.SaveChangesAsync();
        return language;
    }

    /// <summary>
    /// Creates a new tag entity.
    /// </summary>
    public Tag CreateTag(Tag tag, [Service] Context ctx)
    {
        ctx.Add(tag);
        ctx.SaveChangesAsync();
        return tag;
    }

    /// <summary>
    /// Creates a new production company and uploads company logo.
    /// </summary>
    public async Task<ProductionCompanyDTO> CreateProductionCompany(
        ProductionCompanyDTO productionCompanyDTO,
        [Service] Context ctx,
        [Service] IUploadService uploadService)
    {
        var logoName = ExtractFileName.ExtractLogoFileName(productionCompanyDTO);
        var logoPath = await uploadService.UploadImageAsync(logoName, "logo", productionCompanyDTO.Logo);

        var productionCompany = Mapper.ProductionCompanyDTOToProductionCompany(productionCompanyDTO, logoPath);
        ctx.Add(productionCompany);
        await ctx.SaveChangesAsync();

        return productionCompanyDTO;
    }
}
