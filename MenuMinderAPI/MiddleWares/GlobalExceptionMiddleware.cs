using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace MenuMinderAPI.MiddleWares
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        //private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
        {
            //_next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(Exception err)
            {
                _logger.LogError(err, err.Message);
                
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
               
                ProblemDetails detail = new ProblemDetails 
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Internal Server error",
                    Detail = err.Message
                };

                string json = JsonSerializer.Serialize(detail);
                
                await context.Response.WriteAsync(json);
                
                context.Response.ContentType = "application/json";
            }
        }
    }
}
