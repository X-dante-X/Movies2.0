using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesCore.Models;

[Table("movie_cast")]
public class MovieCast
{
    [ForeignKey("Movie")]
    public int? MovieId { get; set; }
    public Movie Movie { get; set; }

    [ForeignKey("Person")]
    public int? PersonId { get; set; }
    public Person Person { get; set; }

    public string CharacterName { get; set; }

    [ForeignKey("Gender")]
    public int? GenderId { get; set; }
    public Gender Gender { get; set; }

    public int? CastOrder { get; set; }
}