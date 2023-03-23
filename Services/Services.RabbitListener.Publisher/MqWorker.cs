using Microsoft.Extensions.Hosting;
using Services.RabbitListener.Publisher.Services;

namespace Services.RabbitListener
{
    public class MqWorker : BackgroundService
    {
        private readonly IPublisherService _publisherService;

        public MqWorker(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string[] urls = new string[]
                {
                    "http://mynet.com",
                    "http://arogames.net",
                    "http://google.com",
                    "http://yandex.com",
                    "http://testdenemetest.net",
                    "microsoft.com",
                    "http://gmail.net"
                };
                foreach (var url in urls)
                {
                    _publisherService.Publish("urls", $"{url}");
                    Thread.Sleep(5000);
                }
      
            }
        }

    }
}
