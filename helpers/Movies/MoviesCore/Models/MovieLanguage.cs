using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesCore.Models;

[Table("movie_languages")]
public class MovieLanguage
{
    [ForeignKey("Movie")]
    public int? MovieId { get; set; }
    public Movie Movie { get; set; }

    [ForeignKey("Language")]
    public int? LanguageId { get; set; }
    public Language Language { get; set; }

    [ForeignKey("LanguageRole")]
    public int? LanguageRoleId { get; set; }
    public LanguageRole LanguageRole { get; set; }
}