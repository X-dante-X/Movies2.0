using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesCore.Models;

[Table("country")]
public class Country
{
    public int CountryId { get; set; }
    public string CountryIsoCode { get; set; }
    public string CountryName { get; set; }
}