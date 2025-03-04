using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesCore.Models;
[Table("gender")]

public class Gender
{
    [Key]
    public int GenderId { get; set; }
    public string GenderName { get; set; }
}