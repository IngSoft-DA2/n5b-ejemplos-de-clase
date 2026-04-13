using Cine.BusinessLogic.Abstractions;
using Cine.Contracts;
using Cine.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cine.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class AuthController(ISessionService sessionService) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request, CancellationToken cancellationToken)
    {
        var session = await sessionService.AuthenticateAsync(request.Username, request.Password, cancellationToken);
        if (session is null)
        {
            return Unauthorized(new ApiResponseDto
            {
                ExecutionSuccessful = false,
                Message = "Nombre de usuario o contrasena invalido.",
                ErrorCode = "INVALID_CREDENTIALS"
            });
        }

        return Ok(new LoginResponseDto
        {
            ExecutionSuccessful = true,
            Message = "Login exitoso.",
            SessionId = session.Token,
            Username = request.Username,
            ExpirationDate = session.ExpirationDate
        });
    }

    [HttpGet("profile")]
    [ServiceFilter(typeof(SessionAuthenticationFilter))]
    public IActionResult GetProfile()
    {
        var session = HttpContext.Items["Session"] as Cine.Domain.Session;
        var user = HttpContext.Items["User"] as Cine.Domain.User;

        if (session is null || user is null)
        {
            return Unauthorized(new ApiResponseDto
            {
                ExecutionSuccessful = false,
                Message = "Session data is missing.",
                ErrorCode = "UNAUTHORIZED"
            });
        }

        return Ok(new
        {
            ExecutionSuccessful = true,
            Message = "Session is valid.",
            Session = new
            {
                session.Id,
                SessionId = session.Token,
                session.UserId,
                session.ExpirationDate,
                session.IsActive
            },
            User = new
            {
                user.Id,
                user.Username
            }
        });
    }
}
