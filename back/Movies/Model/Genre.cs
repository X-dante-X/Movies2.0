using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Genre
{
    [Key]
    public int GenreId { get; set; }
    public string GenreName { get; set; } = null!;
    public List<Movie>? Movies { get; set; }
}