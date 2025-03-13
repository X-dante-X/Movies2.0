using MoviesCore.Models;

namespace MovieWebAPI.DTO;

public class MovieDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal? Popularity { get; set; }
    public string Company { get; set; }
    public string Genre { get; set; }
}
