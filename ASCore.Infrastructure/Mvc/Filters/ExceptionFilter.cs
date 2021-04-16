using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ASCore.Infrastructure.Mvc.Filters
{
    public sealed class ExceptionFilter : ExceptionFilterAttribute
    {
        private const string ResponseContentType = "application/json";

        public override void OnException(ExceptionContext exceptionContext)
        {
            var response = exceptionContext.HttpContext.Response;
            var message = exceptionContext.Exception.Message;
            var statusCode = FindCorrespondingStatusCode(exceptionContext.Exception);
            
            CreateResponseAsync(response, message, statusCode).Wait();
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
                NullReferenceException _ => HttpStatusCode.NotFound,
                UnauthorizedAccessException _ => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError
            };
    }
}
