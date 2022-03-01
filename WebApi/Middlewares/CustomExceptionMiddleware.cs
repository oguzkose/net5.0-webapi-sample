using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Miidlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();

            try
            {

                string requestMessage = "[Request]  HTTP " + context.Request.Method + " - " + "PATH ~" + context.Request.Path;
                _loggerService.Write(requestMessage);
                await _next(context);

                watch.Stop();

                string responseMessage = "[Response] HTTP " + context.Request.Method + " - " + "PATH ~" + context.Request.Path + " responsed " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + "ms";
                _loggerService.Write(responseMessage);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);

            }

        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string errorMessage = "[Error]    HTTP " + context.Request.Method + " - " + "Status Code " + context.Response.StatusCode + " Error Message " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + "ms";

            _loggerService.Write(errorMessage);

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}