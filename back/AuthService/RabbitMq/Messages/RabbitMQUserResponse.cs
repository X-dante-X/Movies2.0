namespace AuthService.RabbitMq.Messages
{
    public class RabbitMQUserResponse
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}
