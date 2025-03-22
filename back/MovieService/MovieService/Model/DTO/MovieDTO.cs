namespace Models.DTO;

public class MovieDTO
{
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
    public IFile Movie { get; set; } = null!;
    public IFile Poster { get; set; } = null!;
    public IFile Backdrop { get; set; } = null!;

    public List<int> Tags { get; set; } = [];
    public List<int> Genre { get; set; } = [];

    public int? ProductionCompanyId { get; set; }
    public int LanguageId { get; set; }
    public int CountryId { get; set; }

    public List<MovieCastDTO>? movieCasts { get; set; }
}
