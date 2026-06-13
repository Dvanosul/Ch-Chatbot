using Ch_Chatbot.Domain.Entities;

namespace Ch_Chatbot.Application.Interfaces.Repositories;

/// <summary>
/// Data-access contract for UserSession aggregates.
/// Future phase: backed by PostgreSQL via Entity Framework Core.
/// </summary>
public interface ISessionRepository
{
    Task<UserSession?> FindAsync(long telegramUserId, long chatId, CancellationToken cancellationToken = default);
    Task AddAsync(UserSession session, CancellationToken cancellationToken = default);
    Task UpdateAsync(UserSession session, CancellationToken cancellationToken = default);
}
