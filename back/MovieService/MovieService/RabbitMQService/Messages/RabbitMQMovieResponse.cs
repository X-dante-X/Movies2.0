namespace MovieService.RabbitMQService.Messages;

public class RabbitMQMovieResponse
{
    public int Id { get; set; } 
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PosterPath { get; set; } = null!;

}
