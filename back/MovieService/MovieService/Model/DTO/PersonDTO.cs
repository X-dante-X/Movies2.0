namespace Models.DTO;

public class PersonDTO
{
    public string PersonName { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public IFile Photo { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public int CountryId { get; set; }
    public string Biography { get; set; } = null!;
}
