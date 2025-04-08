namespace MovieService.RabbitMQService.Messages;

public class RabbitMQMovieResponse
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PosterPath { get; set; } = null!;

}
