using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Services.Exceptions;

namespace MenuMinderAPI.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {

            // Error class format
            ErrorResponse errorResponse = new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.InternalServerError, // Default status 500
                Message = "An unexpected error occurred."
            };

            if (context.Exception is BadRequestException)
            {
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest; // 400
                BadRequestException? badRequestException = context.Exception as BadRequestException;
                if (badRequestException != null)
                {
                    errorResponse.Message = badRequestException.Message;
                }
            }
            else if (context.Exception is NotFoundException)
            {
                errorResponse.StatusCode = (int)HttpStatusCode.NotFound; // 404s
                NotFoundException? notFoundException = context.Exception as NotFoundException;
                if (notFoundException != null)
                {
                    errorResponse.Message = notFoundException.Message;
                }
            }
            else if (context.Exception is UnauthorizedException)
            {
                errorResponse.StatusCode = (int)HttpStatusCode.Unauthorized; // 401
                UnauthorizedException? unauthorizedException = context.Exception as UnauthorizedException;
                if (unauthorizedException != null)
                {
                    errorResponse.Message = unauthorizedException.Message;
                }
            }

            // result response
            var result = new ObjectResult(errorResponse)
            {
                StatusCode = (int)errorResponse.StatusCode
            };

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
