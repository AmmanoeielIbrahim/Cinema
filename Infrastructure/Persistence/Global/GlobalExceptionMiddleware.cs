using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Persistence.Global
{
    public sealed class GlobalExceptionMiddleware
        (RequestDelegate next, ILogger<GlobalExceptionMiddleware> _logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var (status, title, detail) = ex switch
                {
                    NotFoundException e => (StatusCodes.Status404NotFound, "Not Found", e.Message),
                    UnauthenticationException e => (StatusCodes.Status401Unauthorized, "Unauthorized", e.Message),
                    AccountExceptions e => (StatusCodes.Status400BadRequest, "Bad Request", e.Message),
                    InvalidOperationException e => (StatusCodes.Status405MethodNotAllowed, "Can Complete Operation", e.Message),
                    _ => (StatusCodes.Status500InternalServerError, "Server Error", "Unknown Error")
                };

                if (status == StatusCodes.Status500InternalServerError)
                    _logger.LogError(ex, $"Unhandled exception on {context.Request.Path}");

                context.Response.StatusCode = status;
                await context.Response.WriteAsJsonAsync(
                    new ProblemDetails
                    {
                        Type = (status == StatusCodes.Status500InternalServerError ? null : ex.GetType().Name),
                        Status = status,
                        Title = title,
                        Detail = detail
                    });
            }
        }
    }
}