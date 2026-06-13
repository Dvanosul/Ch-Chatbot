using Ch_Chatbot.Application.Interfaces.Repositories;
using Ch_Chatbot.Domain.Entities;
using Ch_Chatbot.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ch_Chatbot.Infrastructure.Repositories;

/// <summary>
/// PostgreSQL-backed implementation of ISessionRepository using EF Core.
/// Replaces InMemorySessionRepository for all non-test environments.
/// </summary>
public sealed class PostgresSessionRepository : ISessionRepository
{
    private readonly AppDbContext _db;

    public PostgresSessionRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<UserSession?> FindAsync(long telegramUserId, long chatId, CancellationToken cancellationToken = default)
        => await _db.UserSessions
            .FirstOrDefaultAsync(s => s.TelegramUserId == telegramUserId && s.ChatId == chatId, cancellationToken);

    public async Task AddAsync(UserSession session, CancellationToken cancellationToken = default)
    {
        await _db.UserSessions.AddAsync(session, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(UserSession session, CancellationToken cancellationToken = default)
    {
        _db.UserSessions.Update(session);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
