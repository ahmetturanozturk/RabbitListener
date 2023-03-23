using RabbitMQ.Client;

namespace Services.RabbitListener.RabbitMQ.Services
{
    public interface IRabbitMQService
    {
        public IConnection GetRabbitMQConnection();
    }
}
