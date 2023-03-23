namespace Services.RabbitListener.Publisher.Services
{
    public interface IPublisherService
    {
        public void Publish(string queueName, string request);
    }
}
