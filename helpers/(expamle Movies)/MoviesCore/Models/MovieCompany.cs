using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesCore.Models;
[Table("movie_company")]

public class MovieCompany
{
    [ForeignKey("Movie")]
    public int? MovieId { get; set; }
    public Movie Movie { get; set; }

    [ForeignKey("ProductionCompany")]
    public int? CompanyId { get; set; }
    public ProductionCompany ProductionCompany { get; set; }
}