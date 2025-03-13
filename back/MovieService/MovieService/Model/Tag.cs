using System.ComponentModel.DataAnnotations;

namespace Models;

public class Tag
{
    [Key]
    public int TagId { get; set; }
    public string TagName { get; set; } = null!;
    public List<Movie>? Movies { get; set; }
}