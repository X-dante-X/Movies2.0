using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesCore.Models;

[Table("person")]
public class Person
{
    [Key]

    public int PersonId { get; set; }
    public string PersonName { get; set; }
}