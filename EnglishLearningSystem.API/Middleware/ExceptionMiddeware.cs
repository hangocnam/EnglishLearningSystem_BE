using Microsoft.AspNetCore.Mvc;

namespace EnglishLearningSystem.API.Middleware
{
    public class ExceptionMiddeware(RequestDelegate next, ILogger<ExceptionMiddeware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddeware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, title) = exception switch
            {
                ValidationException => (StatusCodes.Status400BadRequest, "Validation failed"),
                NotFoundException => (StatusCodes.Status404NotFound, "Resource not found"),
                ConflictException => (StatusCodes.Status409Conflict, "Conflict"),
                _ => (StatusCodes.Status500InternalServerError, "Server error")
            };

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message
            };

            if (exception is ValidationException validationEx)
                problem.Extensions["errors"] = validationEx.Errors;

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
