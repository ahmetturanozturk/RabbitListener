using Newtonsoft.Json;
using RabbitListener.Shared.Model;
using Services.RabbitListener.RabbitMQ.Services;

namespace RabbitListener.Shared.Services
{
    public class LoggingData : ILoggingData
    {
        private readonly string path = RabbitMQService.IsRunningInContainer ? "/app/wwwroot/urls.json": Directory.GetCurrentDirectory() + "\\urls.json";

        public LoggingData()
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "[]");
            }
        }

        public void Log(string serviceName, string statusCode, string url)
        {
            Console.WriteLine("Turan ==================== "+path);
            string output = File.ReadAllText(path);
            var urls = JsonConvert.DeserializeObject<List<CheckedUrl>>(output);
            urls.Add(new CheckedUrl()
            {
                ServiceName = serviceName,
                StatusCode = statusCode,
                Url = url
            });
            output = JsonConvert.SerializeObject(urls, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(path, output);
        }
    }
}
