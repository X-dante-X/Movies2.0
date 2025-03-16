using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Person
{
    public int PersonId { get; set; }
    public string PersonName { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    [ForeignKey("Country")]
    public int CountryId { get; set; }
    public Country Nationality { get; set; } = new();
    public string Biography { get; set; } = null!;
    public List<Movie>? Filmography { get; set; }
}
