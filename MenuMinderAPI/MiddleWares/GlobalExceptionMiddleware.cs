using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Services.Exceptions;

namespace MenuMinderAPI.MiddleWares
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
        {
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
                // Check reponse have not been send to client
                if (!context.Response.HasStarted)
                {
                    _logger.LogError(err, err.Message);
                    ProblemDetails problem = new ProblemDetails();
                    context.Response.ContentType = "application/json";

                    if (err is UnauthorizedException)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        problem.Status = (int)HttpStatusCode.Unauthorized;
                        problem.Type = "Unauthorized";
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        problem.Type = "Internal Server error";
                        problem.Detail = err.Message;

                    }
                    problem.Detail = err.Message;
                    string json = JsonSerializer.Serialize(problem);
                    await context.Response.WriteAsync(json);
                }
            }
        }
    }
}
