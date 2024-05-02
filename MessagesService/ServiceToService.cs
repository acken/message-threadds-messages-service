using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Uniscale.Core;

namespace MessagesService
{
    public class ServiceToService
    {
        private readonly ILogger _logger;
        private readonly UniscaleSession _uniscaleSession;

        public ServiceToService(ILoggerFactory loggerFactory, UniscaleSession uniscaleSession)
        {
            _logger = loggerFactory.CreateLogger<ServiceToModule>();
            _uniscaleSession = uniscaleSession;
        }

        [Function("ServiceToService")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            var session = await _uniscaleSession.GetPlatformSession();
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            Result<object> result;
            try {
                result = await session.AcceptGatewayRequest(body); 
            } catch (Exception e) {
                result = Result<object>.BadRequest("Platform.Fundamentals.SDK.InvalidRequestInformation", e.ToString());
                _logger.LogError(e, "Error processing request");
            }
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            await response.WriteStringAsync(result.ToJson());
            return response;
        }
    }
}
