using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models.DTO;

public class ProductionCompanyDTO
{
    public string CompanyName { get; set; } = null!;
    public int CountryId { get; set; }
}
