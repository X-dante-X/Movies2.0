using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesCore.Models;
[Table("genre")]

public class Genre
{
    [Key]
    public int GenreId { get; set; }
    public string GenreName { get; set; }
}