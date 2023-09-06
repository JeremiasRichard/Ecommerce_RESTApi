using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using RemitoApi.DTOs;
using System.Net;

namespace RemitoApi.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomException ex)
            { 

                await HandleGlobalExceptionsAsync(httpContext, ex);
            }
        }

        private static async Task HandleGlobalExceptionsAsync(HttpContext httpContext, CustomException exception)
        {
            
            httpContext.Response.StatusCode = exception.StatusCode;
            httpContext.Response.ContentType = "application/json";
            exception.HResult = exception.HResult;
            var jsonReponse = JsonConvert.SerializeObject(new ResponseDTO { Message = exception.Message, StatusCode = exception.StatusCode.ToString() });

            await httpContext.Response.WriteAsync(jsonReponse);
        }

    }
}
