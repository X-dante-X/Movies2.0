using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class ProductionCompany
{
    [Key]
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string? LogoPath { get; set; }

    [ForeignKey("Country")]
    public int CountryId { get; set; }
    public Country Country { get; set; } = null!;
}