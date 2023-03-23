using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using RabbitListener.Shared.Services;
using Services.RabbitListener.Consumer;
using Services.RabbitListener.Publisher.Controllers;
using Services.RabbitListener.Publisher.Services;
using Services.RabbitListener.RabbitMQ.Services;

namespace Services.RabbitListener.NUnitTests
{
    [TestFixture]
    public class Tests
    {
        private ServiceCollection services;
        private IPublisherService _publisherService;
        private IConsumerService _consumerService;
        private IRabbitMQService _rabbitMQService;

        [SetUp]
        public void Setup()
        {
            services = new ServiceCollection();
            services.AddLogging(configure => configure.AddConsole());
            services.AddSingleton<IPublisherService, PublisherService>();
            services.AddSingleton<IConsumerService, ConsumerService>();
            _rabbitMQService = new RabbitMQService();
            _publisherService = new PublisherService(_rabbitMQService);
            _consumerService = new ConsumerService(_rabbitMQService);
        }

        [TestCase("http://google.com", "test_url")]
        public void rabbitmq_message_publisher_and_consumer_test(string test_url, string queuename)
        {
            Thread.Sleep(2000);
            _consumerService.Consume(queuename);
            Assert.GreaterOrEqual(0, Data.urls.IndexOf(test_url));
        }

        [TestCase("http://google.com", "test_url")]
        public async Task queue_controller_testAsync(string test_url, string queuename)
        {
            _publisherService.Publish(queuename, test_url);
            var mock = new Mock<ILoggingData>();
            ILoggingData loggingData = mock.Object;

            loggingData = Mock.Of<ILoggingData>();

            QueueController queueController = new(_consumerService, loggingData);
            await queueController.Start();
            var result = await queueController.GetUrl();
            Assert.IsNotNull(result);
        }

    }
}