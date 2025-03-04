using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesCore.Models;

[Table("production_company")]
public class ProductionCompany
{
    [Key]
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public List<MovieCompany> MovieCompanies { get; set; }

}