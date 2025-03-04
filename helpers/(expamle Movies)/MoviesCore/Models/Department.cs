using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesCore.Models;

[Table("department")]
public class Department
{
    [Key]
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
}