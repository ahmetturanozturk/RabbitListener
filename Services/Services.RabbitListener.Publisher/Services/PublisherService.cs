using RabbitMQ.Client;
using Services.RabbitListener.RabbitMQ.Services;
using System.Text;

namespace Services.RabbitListener.Publisher.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IRabbitMQService _rabbitMQService;

        public PublisherService(IRabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        public void Publish(string queueName, string request)
        {

            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                if (connection != null)
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: queueName,
                                             durable: true,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);
                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true;
                        channel.BasicPublish("", queueName, null, Encoding.UTF8.GetBytes(request));
                        Console.WriteLine("{0} ---> {1}", queueName, request);

                    }
            }
        }
    }
}
