using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesCore.Models;
[Table("language")]

public class Language
{
    [Key]
    public int LanguageId { get; set; }
    public string LanguageCode { get; set; }
    public string LanguageName { get; set; }
}