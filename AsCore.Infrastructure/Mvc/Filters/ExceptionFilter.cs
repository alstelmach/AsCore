using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace AsCore.Infrastructure.Mvc.Filters
{
    public sealed class ExceptionFilter : IExceptionFilter
    {
        private const string ResponseContentType = "application/json";

        public void OnException(ExceptionContext exceptionContext)
        {
            var response = exceptionContext.HttpContext.Response;
            var message = exceptionContext.Exception.Message;
            var statusCode = FindCorrespondingStatusCode(exceptionContext.Exception);
            
            CreateResponse(response, message, statusCode);
            exceptionContext.ExceptionHandled = true;
        }
        
        private static void CreateResponse(HttpResponse response,
            string errorMessage,
            HttpStatusCode httpStatusCode)
        {
            var statusCode = (int) httpStatusCode;
            var body = JsonConvert.SerializeObject(
                new
                {
                    Message = errorMessage,
                    StatusCode = statusCode,
                });

            response.StatusCode = statusCode;
            response.ContentType = ResponseContentType;
            response.WriteAsync(body).Wait();
        }

        private static HttpStatusCode FindCorrespondingStatusCode(Exception exception) =>
            exception switch
            {
                UnauthorizedAccessException _ => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError
            };
    }
}
