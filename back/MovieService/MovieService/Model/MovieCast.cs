using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class MovieCast
{
    [ForeignKey("Movie")]
    public int? MovieId { get; set; }
    public Movie Movie { get; set; } = null!;

    [ForeignKey("Person")]
    public int? PersonId { get; set; }
    public Person Person { get; set; } = null!;
    public string? CharacterName { get; set; }
    public string? CharacterGender { get; set; }
    public string Job { get; set; } = null!;
}
