using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Language
{
    [Key]
    public int LanguageId { get; set; }
    public string LanguageName { get; set; } = null!;
    public List<Movie>? Movies { get; set; }
}