namespace MovieService.RabbitMQService.Messages
{
    public class RabbitMQMovieResponse
    {
        public int Id { get; set; } 
        public string Title { get; set; }   
        public string Description { get; set; }
        public string PosterPath { get; set; }   

    }
}
