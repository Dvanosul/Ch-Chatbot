using Ch_Chatbot.Domain.Entities;

namespace Ch_Chatbot.Application.Interfaces.Services;

/// <summary>
/// Manages per-user session lifecycle.
/// Future phases will persist sessions in PostgreSQL and track active character assignments.
/// </summary>
public interface ISessionService
{
    Task<UserSession> GetOrCreateAsync(long telegramUserId, long chatId, CancellationToken cancellationToken = default);
    Task UpdateAsync(UserSession session, CancellationToken cancellationToken = default);
}
