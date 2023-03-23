using RabbitMQ.Client;

namespace Services.RabbitListener.RabbitMQ.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        public static bool IsRunningInContainer => bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var inDocker) && inDocker;
        private readonly string _hostName = IsRunningInContainer ? "rabbitmq" : "localhost";
        private readonly string _userName = "guest";
        private readonly string _password = "guest";

        public IConnection GetRabbitMQConnection()
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory()
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password,
                    Port = 5672
                };
                factory.AutomaticRecoveryEnabled = true;
                factory.TopologyRecoveryEnabled = false;
                factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(10);
                return factory.CreateConnection();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
