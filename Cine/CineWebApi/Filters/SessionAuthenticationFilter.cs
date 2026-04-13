using Cine.BusinessLogic.Abstractions;
using Cine.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cine.Filters;

public sealed class SessionAuthenticationFilter(ISessionService sessionService) : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var sessionId = TryGetSessionId(context.HttpContext.Request.Headers);
        if (string.IsNullOrWhiteSpace(sessionId))
        {
            context.Result = BuildUnauthorizedResult("Session token was not provided.");
            return;
        }

        var session = await sessionService.GetValidSessionAsync(sessionId, context.HttpContext.RequestAborted);
        if (session is null)
        {
            context.Result = BuildUnauthorizedResult("Session is invalid, inactive, or expired.");
            return;
        }

        context.HttpContext.Items["Session"] = session;
        if (session.User is not null)
        {
            context.HttpContext.Items["User"] = session.User;
        }
    }

    private static string? TryGetSessionId(IHeaderDictionary headers)
    {
        if (headers.TryGetValue("X-Session-Id", out var customHeaderValue))
        {
            var sessionId = customHeaderValue.ToString().Trim();
            if (!string.IsNullOrWhiteSpace(sessionId))
            {
                return sessionId;
            }
        }

        return null;
    }

    private static UnauthorizedObjectResult BuildUnauthorizedResult(string message)
    {
        return new UnauthorizedObjectResult(new ApiResponseDto
        {
            ExecutionSuccessful = false,
            Message = message,
            ErrorCode = "UNAUTHORIZED"
        });
    }
}
