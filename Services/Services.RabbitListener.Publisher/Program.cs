
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.RabbitListener;
using Services.RabbitListener.Publisher.Services;
using Services.RabbitListener.RabbitMQ.Services;


CreateHostBuilder(args).Build().Run();


static IHostBuilder CreateHostBuilder(string[] args) =>
   Host.CreateDefaultBuilder(args)
       .ConfigureServices((hostContext, services) =>
       {
           services.AddHostedService<MqWorker>();
           services.AddScoped<IRabbitMQService, RabbitMQService>();
           services.AddScoped<IPublisherService, PublisherService>();
       });

