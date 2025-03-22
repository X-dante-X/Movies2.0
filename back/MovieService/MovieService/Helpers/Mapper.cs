using HotChocolate.Types;
using Models;
using Models.DTO;
using Tag = Models.Tag;

namespace MovieService.Helpers;

public static class Mapper
{
    public static Movie MovieDTOToMovie(MovieDTO movieDTO, string moviePath, string posterPath, string backdropPath, List<Genre>? genres, List<Tag>? tags)
    {
        return new Movie() 
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
            Genre = genres ?? [],
            Tags = tags ?? [],
        };
    }

    public static MovieCast MovieCastDTOToMovieCast(MovieCastDTO movieCastDTO, int movieId)
    {
        return new MovieCast()
        {
            MovieId = movieId,
            PersonId = movieCastDTO.PersonId,
            Job = movieCastDTO.Job,
            CharacterGender = movieCastDTO.CharacterGender,
            CharacterName = movieCastDTO.CharacterName,
        };
    }

    public static Person PersonDTOToPerson(PersonDTO personDTO, string photoPath) 
    {
        return new Person()
        {
            PersonName = personDTO.PersonName,
            Gender = personDTO.Gender,
            PhotoPath = photoPath,
            DateOfBirth = personDTO.DateOfBirth,
            CountryId = personDTO.CountryId,
            Biography = personDTO.Biography,
        };
    }

    public static ProductionCompany ProductionCompanyDTOToProductionCompany(ProductionCompanyDTO productionCompanyDTO, string logoPath) 
    {
        return new ProductionCompany()
        {
            CompanyName = productionCompanyDTO.CompanyName,
            LogoPath = logoPath,
            CountryId = productionCompanyDTO.CountryId,
        };
    }
}
