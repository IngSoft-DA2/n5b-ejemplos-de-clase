using Cine.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cine.Filters;

public sealed class ApiExceptionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        var (statusCode, errorCode) = context.Exception switch
        {
            ArgumentNullException => (StatusCodes.Status400BadRequest, "ARGUMENT_NULL"),
            ArgumentException => (StatusCodes.Status400BadRequest, "ARGUMENT_INVALID"),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "UNAUTHORIZED"),
            KeyNotFoundException => (StatusCodes.Status404NotFound, "NOT_FOUND"),
            _ => (StatusCodes.Status500InternalServerError, "INTERNAL_SERVER_ERROR")
        };

        var response = new ApiResponseDto
        {
            ExecutionSuccessful = false,
            Message = statusCode == StatusCodes.Status500InternalServerError
                ? "An unexpected error occurred."
                : context.Exception.Message,
            ErrorCode = errorCode,
            Details = context.Exception.Message
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
        return Task.CompletedTask;
    }
}
