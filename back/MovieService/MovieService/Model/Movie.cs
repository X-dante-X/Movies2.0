using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Movie
{
    public int MovieId { get; set; }
    public string Title { get; set; } = null!;
    public DateTime? ReleaseDate { get; set; }
    public int? Budget { get; set; }
    public string Description { get; set; } = null!;
    public decimal? Popularity { get; set; }
    public int? Runtime { get; set; }
    public string MovieStatus { get; set; } = null!;
    public decimal? VoteAverage { get; set; }
    public int? VoteCount { get; set; }
    public string PEGI { get; set; } = null!;

    public string? MoviePath { get; set; }
    public string? PosterPath { get; set; }
    public string? BackdropPath { get; set; }

    public List<Tag> Tags { get; set; } = null!;
    public List<Genre> Genre { get; set; } = null!;
    public List<MovieCast>? MovieCasts { get; set; }

    [ForeignKey("ProductionCompany")]
    public int? ProductionCompanyId { get; set; }
    public ProductionCompany? ProductionCompany { get; set; }

    [ForeignKey("Language")]
    public int LanguageId { get; set; }
    public Language ProductionLanguage { get; set; } = null!;

    [ForeignKey("Country")]
    public int CountryId { get; set; }
    public Country ProductionCountry { get; set; } = null!;
}