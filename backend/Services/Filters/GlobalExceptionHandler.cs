using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FluentValidation;
using Expense.Tracker.Application.Exceptions;

namespace Expense.Tracker.Services.Filters
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {
            var problemDetails = CreateProblemDetails(httpContext, exception);
            
            httpContext.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
            
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            
            return true;
        }

        private static ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
        {
            return exception switch
            {
                ValidationException validationEx => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = "Validation Error",
                    Detail = "One or more validation errors occurred",
                    Instance = context.Request.Path,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Extensions = { ["errors"] = validationEx.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }) }
                },
                BadRequestException  => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = "Bad Request",
                    Detail = exception.Message,
                    Instance = context.Request.Path,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                },
                NotFoundException => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Title = "Not Found",
                    Detail = exception.Message,
                    Instance = context.Request.Path,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
                },
                ForbiddenAccessException => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.Forbidden,
                    Title = "Forbidden",
                    Detail = "You do not have permission to access this resource",
                    Instance = context.Request.Path,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
                },
                ArgumentNullException argNullEx => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "A required service parameter was not provided",
                    Instance = context.Request.Path,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                },
                UnauthorizedAccessException => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.Unauthorized,
                    Title = "Unauthorized",
                    Detail = "Access to the requested resource is unauthorized",
                    Instance = context.Request.Path,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.3.1"
                },
                TimeoutException => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.RequestTimeout,
                    Title = "Request Timeout",
                    Detail = "The request timed out",
                    Instance = context.Request.Path,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.7"
                },
                _ => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred",
                    Instance = context.Request.Path,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                }
            };
        }
    }
}
