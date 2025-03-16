namespace Models.DTO;

public class MovieCastDTO
{
    public int PersonId { get; set; }
    public string? CharacterName { get; set; }
    public string? CharacterGender { get; set; }
    public string Job { get; set; } = null!;
}
