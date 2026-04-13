using Cine.Domain;
using Cine.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Cine.Repository;

public sealed class EfSessionRepository(AppDbContext dbContext) : ISessionRepository
{
    public async Task<User?> GetUserByCredentialsAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        return await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(
                u => u.Username == username && u.Password == password,
                cancellationToken);
    }

    public async Task<Session> AddSessionAsync(Session session, CancellationToken cancellationToken = default)
    {
        dbContext.Sessions.Add(session);
        await dbContext.SaveChangesAsync(cancellationToken);
        return session;
    }

    public async Task<Session?> GetValidSessionAsync(string sessionId, CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        return await dbContext.Sessions
            .AsNoTracking()
            .Include(s => s.User)
            .FirstOrDefaultAsync(
                s => s.Token == sessionId
                     && s.IsActive
                     && s.ExpirationDate > now
                     && s.User != null,
                cancellationToken);
    }
}