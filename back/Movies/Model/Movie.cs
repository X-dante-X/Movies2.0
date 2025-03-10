
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

    public List<Tag> Tags { get; set; } = [];
    public List<Genre> Genre { get; set; } = [];
    public List<MovieCast>? MovieCasts { get; set; }
    public ProductionCompany? ProductionCompany { get; set; }
    public Language ProductionLanguage { get; set; } = null!;
    public Country ProductionCountry { get; set; } = null!;
}