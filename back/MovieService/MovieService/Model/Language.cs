using System.ComponentModel.DataAnnotations;

namespace Models;

public class Language
{
    [Key]
    public int LanguageId { get; set; }
    public string LanguageName { get; set; } = null!;
    public List<Movie>? Movies { get; set; }
}