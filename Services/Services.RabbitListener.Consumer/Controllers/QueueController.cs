using Microsoft.AspNetCore.Mvc;
using RabbitListener.Shared.Services;
using Services.RabbitListener.Consumer;
using Services.RabbitListener.Publisher.Services;

namespace Services.RabbitListener.Publisher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueueController : ControllerBase
    {

        private readonly ILoggingData _loggingData;
        private readonly IConsumerService _consumerService;

        public QueueController(IConsumerService consumerService, ILoggingData loggingData)
        {
            _consumerService = consumerService;
            _loggingData = loggingData;
        }

        [HttpGet]
        [Route("Start")]
        public async Task<IActionResult> Start()
        {
            Data.IsStart = true;
            _consumerService.Consume("urls");
            return Ok();
        }


        [HttpGet]
        [Route("Stop")]
        public async Task<IActionResult> Stop()
        {
            _consumerService.Close();
            return Ok();
        }

        [HttpGet]
        [Route("isstart")]
        public async Task<IActionResult> IsStart()
        {
            return Ok(Data.IsStart);
        }

        [HttpGet]
        [Route("GetUrl")]
        public async Task<IActionResult> GetUrl()
        {
            var url = string.Empty;

            if (Data.urls.Any())
            {
                url = Data.urls.Last();
                Data.urls.RemoveAt(Data.urls.Count - 1);
            }

            if (string.IsNullOrEmpty(url))
            {
                return NoContent();
            }
            else
            {
                HttpClient client = new HttpClient();

                var statusCode = string.Empty;
                try
                {
                    var result = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
                    statusCode = result.StatusCode.ToString();
                }
                catch (Exception ex)
                {
                    statusCode = ex.Message.ToString();
                }

                _loggingData.Log("RabbitListener", statusCode, url);
                return Ok(new
                {
                    url = url,
                    result = statusCode
                });
            }

        }
    }
}