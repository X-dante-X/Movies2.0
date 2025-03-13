using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesCore.Models;
[Table("language_role")]

public class LanguageRole
{
    [Key]
    public int RoleId { get; set; }
    public string LanguageRoleName { get; set; }
}