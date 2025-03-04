using System.ComponentModel.DataAnnotations;

namespace Models;

public class ProductionCompany
{
    [Key]
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = null!;
    public Country Country { get; set; } = null!;
}