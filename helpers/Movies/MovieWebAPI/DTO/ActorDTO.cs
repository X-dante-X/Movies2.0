namespace MovieWebAPI.DTO;

public class ActorDTO
{
    public int PersonId { get; set; }
    public string PersonName { get; set; }
    public List<MoviesWherePlayThisActorDTO> MoviesWherePlayThisActor { get; set; }
}

public class MoviesWherePlayThisActorDTO
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public string Gender { get; set; }
    public string CharacterName { get; set; }
}