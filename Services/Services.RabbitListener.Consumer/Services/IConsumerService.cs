namespace Services.RabbitListener.Publisher.Services
{
    public interface IConsumerService
    {
        public void Close();
        public void Consume(string queueName);
    }
}
