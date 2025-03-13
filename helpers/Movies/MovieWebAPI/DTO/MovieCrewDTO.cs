namespace MovieWebAPI.DTO;

public class MovieCrewDTO
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public int PersonId { get; set; }
    public string PersonName { get; set; }
    public string DepartmentName { get; set; }
    public string Job { get; set; }
}
