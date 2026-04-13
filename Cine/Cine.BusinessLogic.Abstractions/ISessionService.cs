using Cine.Domain;

namespace Cine.BusinessLogic.Abstractions;

public interface ISessionService
{
    Task<Session?> AuthenticateAsync(string username, string password, CancellationToken cancellationToken = default);
    Task<Session?> GetValidSessionAsync(string sessionId, CancellationToken cancellationToken = default);
}
