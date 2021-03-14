using System;
using System.Net;
using System.Threading.Tasks;
using Core.Domain.Abstractions.BuildingBlocks;
using Core.Utilities.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Core.Infrastructure.Mvc.Filters
{
    public sealed class ExceptionFilter : ExceptionFilterAttribute
    {
        private const string ResponseContentType = "application/json";
        
        private readonly ILogger _logger;

        public ExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }
        
        public override void OnException(ExceptionContext exceptionContext)
        {
            LogException(exceptionContext.Exception);
            
            var response = exceptionContext.HttpContext.Response;
            var message = exceptionContext.Exception.Message;
            var statusCode = FindCorrespondingStatusCode(exceptionContext.Exception);
            
            CreateResponseAsync(response, message, statusCode).Wait();
        }

        private void LogException(Exception exception)
        {
            if (exception is DomainException)
            {
                return;
            }

            var message = exception.GetDetailedMessage();
            _logger.LogError(exception, message);
        }
        
        private static async Task CreateResponseAsync(HttpResponse response,
            string errorMessage,
            HttpStatusCode statusCode)
        {
            var result = JsonConvert.SerializeObject(
                new
                {
                    Message = errorMessage,
                    StatusCode = (int) statusCode,
                });

            response.StatusCode = (int) statusCode;
            response.ContentType = ResponseContentType;
            await response.WriteAsync(result);
        }

        private static HttpStatusCode FindCorrespondingStatusCode(Exception exception) =>
            exception switch
            {
                ArgumentNullException _ => HttpStatusCode.NotFound,
                UnauthorizedAccessException _ => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError
            };
    }
}
