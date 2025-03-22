namespace MovieWebAPI.DTO;

public class ProductionCompanyDTO
{
    public string CompanyName { get; set; }
    public List<MovieIdAndTitleDTO> Movies { get; set; }
}

public class MovieIdAndTitleDTO
{
    public int MovieId { get; set; }
    public string Title { get; set; }
}