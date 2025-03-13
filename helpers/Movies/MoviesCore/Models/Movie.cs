using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesCore.Models;

[Table("movie")]
public class Movie
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public int? Budget { get; set; }
    public string Homepage { get; set; }
    public string Overview { get; set; }
    public decimal? Popularity { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public long? Revenue { get; set; }
    public int? Runtime { get; set; }
    public string MovieStatus { get; set; }
    public string Tagline { get; set; }
    public decimal? VoteAverage { get; set; }
    public int? VoteCount { get; set; }

    public List<MovieCast> MovieCasts { get; set; }
    public List<MovieCompany> MovieCompanies { get; set; }
    public List<MovieCrew> MovieCrews { get; set; }
    public List<MovieGenre> MovieGenres { get; set; }
    public List<MovieKeyword> MovieKeywords { get; set; }
    public List<MovieLanguage> MovieLanguages { get; set; }
    public List<ProductionCountry> ProductionCountries { get; set; }
}