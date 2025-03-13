namespace Models;

public class Person
{
    public int PersonId { get; set; }
    public string PersonName { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public Country Nationality { get; set; } = null!;
    public string Biography { get; set; } = null!;
    public List<Movie>? Filmography { get; set; }
}
