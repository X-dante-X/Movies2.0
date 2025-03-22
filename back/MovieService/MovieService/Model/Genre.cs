using System.ComponentModel.DataAnnotations;

namespace Models;

public class Genre
{
    [Key]
    public int GenreId { get; set; }
    public string GenreName { get; set; } = null!;
    public List<Movie>? Movies { get; set; }
}