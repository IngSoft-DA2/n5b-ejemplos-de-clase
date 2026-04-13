using Cine.BusinessLogic.Abstractions;
using Cine.Domain;
using Cine.Repository.Abstractions;

namespace Cine.BusinessLogic;

public sealed class SessionService(ISessionRepository sessionRepository) : ISessionService
{
    public async Task<Session?> AuthenticateAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return null;
        }

        var normalizedUsername = username.Trim();
        var user = await sessionRepository.GetUserByCredentialsAsync(normalizedUsername, password, cancellationToken);

        if (user is null)
        {
            return null;
        }

        var session = new Session
        {
            Token = Guid.NewGuid().ToString(),
            UserId = user.Id,
            IsActive = true,
            ExpirationDate = DateTime.UtcNow.AddHours(8)
        };

        await sessionRepository.AddSessionAsync(session, cancellationToken);

        session.User = user;
        return session;
    }

    public async Task<Session?> GetValidSessionAsync(string sessionId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
        {
            return null;
        }

        return await sessionRepository.GetValidSessionAsync(sessionId.Trim(), cancellationToken);
    }
}
