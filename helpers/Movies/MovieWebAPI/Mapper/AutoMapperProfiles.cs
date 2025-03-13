using AutoMapper;
using MoviesCore.Models;
using MovieWebAPI.DTO;

namespace MovieWebAPI.Mapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Movie, MovieDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MovieId))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => string.Join(", ", src.MovieGenres.Select(mg => mg.Genre.GenreName))))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => string.Join(", ", src.MovieCompanies.Select(mc => mc.ProductionCompany.CompanyName))));
        CreateMap<RequestMovieDTO, Movie>();

        CreateMap<MovieCrew, MovieCrewDTO>()
            .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.Movie.MovieId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Movie.Title))
            .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Person.PersonId))
            .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.Person.PersonName))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName));

        CreateMap<MovieCast, MovieCastDTO>()
            .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.Movie.MovieId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Movie.Title))
            .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Person.PersonId))
            .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.Person.PersonName))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.GenderName));


        CreateMap<List<MovieCast>, ActorDTO>()
            .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.FirstOrDefault().Person.PersonName))
            .ForMember(dest => dest.MoviesWherePlayThisActor, opt => opt.MapFrom(src => src.Select(mc => new MoviesWherePlayThisActorDTO
            {
                MovieId = mc.Movie.MovieId,
                Title = mc.Movie.Title,
                Gender = mc.Gender.GenderName,
                CharacterName = mc.CharacterName
            })));

        CreateMap<ProductionCompany, ProductionCompanyDTO>()
             .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
             .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.MovieCompanies.Select(mc => new MovieIdAndTitleDTO
             {
                 MovieId = mc.Movie.MovieId,
                 Title = mc.Movie.Title
             })));
    }
}
