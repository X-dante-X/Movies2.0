using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesCore.Models;
[Table("keyword")]

public class Keyword
{
    [Key]
    public int KeywordId { get; set; }
    public string KeywordName { get; set; }
}