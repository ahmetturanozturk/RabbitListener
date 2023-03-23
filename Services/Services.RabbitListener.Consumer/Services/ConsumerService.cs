using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.RabbitListener.Consumer;
using Services.RabbitListener.RabbitMQ.Services;
using System.Text;
using System.Threading.Channels;

namespace Services.RabbitListener.Publisher.Services
{
    public class ConsumerService : IConsumerService
    {
        private readonly IRabbitMQService _rabbitMQService;
        private IModel? _channel;
        public ConsumerService(IRabbitMQService rabbitMQService) => _rabbitMQService = rabbitMQService;

        public void Consume(string queueName)
        {
            string url = string.Empty;
            var connection = _rabbitMQService.GetRabbitMQConnection();
            {
                if (connection == null)
                    return;
                _channel = connection.CreateModel();
                {
                    var consumer = new EventingBasicConsumer(_channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        url = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"Received {url}");
                        Data.urls.Add(url);
                    };

                    _channel.BasicConsume(queueName, true, consumer);
                }
            }
        }

        public void Close()
        {
            Data.IsStart = false;
            if (_channel != null)
                _channel.Close();
        }
    }
}
