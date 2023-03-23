namespace RabbitListener.Shared.Services
{
    public interface ILoggingData
    {
        public void Log(string serviceName, string statusCode, string url);
    }
}
