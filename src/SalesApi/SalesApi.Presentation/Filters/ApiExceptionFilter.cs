using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using FluentValidation;

namespace SalesApi.Presentation.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;
    private readonly IHostEnvironment _environment;

    public ApiExceptionFilter(
        ILogger<ApiExceptionFilter> logger,
        IHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "An unhandled exception occurred");

        var errorResponse = new ErrorResponse();
        HttpStatusCode statusCode;

        switch (context.Exception)
        {
            case ValidationException validationEx:
                statusCode = HttpStatusCode.BadRequest;
                errorResponse.Type = "ValidationError";
                errorResponse.Error = "Validation failed";
                errorResponse.Detail = string.Join("; ", validationEx.Errors.Select(e => e.ErrorMessage));
                break;

            case KeyNotFoundException notFoundEx:
                statusCode = HttpStatusCode.NotFound;
                errorResponse.Type = "NotFound";
                errorResponse.Error = "Resource not found";
                errorResponse.Detail = notFoundEx.Message;
                break;

            case ArgumentException argEx:
                statusCode = HttpStatusCode.BadRequest;
                errorResponse.Type = "BadRequest";
                errorResponse.Error = "Invalid argument";
                errorResponse.Detail = argEx.Message;
                break;

            case InvalidOperationException invOpEx:
                statusCode = HttpStatusCode.BadRequest;
                errorResponse.Type = "BadRequest";
                errorResponse.Error = "Invalid operation";
                errorResponse.Detail = invOpEx.Message;
                break;

            default:
                statusCode = HttpStatusCode.InternalServerError;
                errorResponse.Type = "ServerError";
                errorResponse.Error = "An unexpected error occurred";
                errorResponse.Detail = _environment.IsDevelopment() 
                    ? context.Exception.Message 
                    : "Please contact the administrator if the problem persists.";
                break;
        }

        context.Result = new ObjectResult(errorResponse)
        {
            StatusCode = (int)statusCode
        };

        context.ExceptionHandled = true;
    }

    private class ErrorResponse
    {
        public string Type { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
    }
}
