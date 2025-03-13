using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesCore.Models;

[Table("movie_crew")]
public class MovieCrew
{
    [ForeignKey("Movie")]
    public int? MovieId { get; set; }
    public Movie Movie { get; set; }

    [ForeignKey("Person")]
    public int? PersonId { get; set; }
    public Person Person { get; set; }

    [ForeignKey("Department")]
    public int? DepartmentId { get; set; }
    public Department Department { get; set; }

    public string Job { get; set; }
}