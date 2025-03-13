using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesCore.Models;

[Table("movie_keywords")]
public class MovieKeyword
{
    [ForeignKey("Movie")]
    public int? MovieId { get; set; }
    public Movie Movie { get; set; }

    [ForeignKey("Keyword")]
    public int? KeywordId { get; set; }
    public Keyword Keyword { get; set; }
}