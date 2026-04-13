using Cine.Domain;

namespace Cine.Repository.Abstractions;

public interface ISessionRepository
{
    Task<User?> GetUserByCredentialsAsync(string username, string password, CancellationToken cancellationToken = default);
    Task<Session> AddSessionAsync(Session session, CancellationToken cancellationToken = default);
    Task<Session?> GetValidSessionAsync(string sessionId, CancellationToken cancellationToken = default);
}