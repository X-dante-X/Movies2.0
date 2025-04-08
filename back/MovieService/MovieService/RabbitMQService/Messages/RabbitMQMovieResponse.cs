<<<<<<< HEAD
﻿namespace MovieService.RabbitMQService.Messages
{
    public class RabbitMQMovieResponse
    {
        public int Id { get; set; } 
        public string Title { get; set; }   
        public string Description { get; set; }
        public string PosterPath { get; set; }   
=======
﻿namespace MovieService.RabbitMQService.Messages;

public class RabbitMQMovieResponse
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PosterPath { get; set; } = null!;
>>>>>>> 13caaae709cd760fddea1627fdfa01ffabd4b709

}
