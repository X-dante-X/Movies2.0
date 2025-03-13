using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesCore.Models;

[Table("production_country")]
public class ProductionCountry
{
    [ForeignKey("Movie")]
    public int? MovieId { get; set; }
    public Movie Movie { get; set; }

    [ForeignKey("Country")]
    public int? CountryId { get; set; }
    public Country Country { get; set; }
}

