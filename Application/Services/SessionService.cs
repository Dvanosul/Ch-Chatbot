using Ch_Chatbot.Application.Interfaces.Repositories;
using Ch_Chatbot.Application.Interfaces.Services;
using Ch_Chatbot.Domain.Entities;

namespace Ch_Chatbot.Application.Services;

/// <summary>
/// Manages user sessions. Currently uses in-memory fallback until PostgreSQL is available.
/// Future phase: fully persistent via ISessionRepository.
/// </summary>
public sealed class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ILogger<SessionService> _logger;

    public SessionService(ISessionRepository sessionRepository, ILogger<SessionService> logger)
    {
        _sessionRepository = sessionRepository;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<UserSession> GetOrCreateAsync(long telegramUserId, long chatId, CancellationToken cancellationToken = default)
    {
        var existing = await _sessionRepository.FindAsync(telegramUserId, chatId, cancellationToken);
        if (existing is not null)
            return existing;

        _logger.LogInformation("Creating new session for user {UserId}", telegramUserId);

        var session = new UserSession
        {
            TelegramUserId = telegramUserId,
            ChatId         = chatId
        };

        await _sessionRepository.AddAsync(session, cancellationToken);
        return session;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(UserSession session, CancellationToken cancellationToken = default)
    {
        session.LastInteractionAt = DateTime.UtcNow;
        await _sessionRepository.UpdateAsync(session, cancellationToken);
    }
}
