using Ch_Chatbot.Application.Interfaces.Repositories;
using Ch_Chatbot.Domain.Entities;
using System.Collections.Concurrent;

namespace Ch_Chatbot.Infrastructure.Repositories;

/// <summary>
/// Temporary in-memory implementation of ISessionRepository.
/// Allows the application to run without a database during early development.
/// Must be replaced by a PostgreSQL-backed implementation in the next phase.
/// </summary>
public sealed class InMemorySessionRepository : ISessionRepository
{
    // Key: (TelegramUserId, ChatId)
    private readonly ConcurrentDictionary<(long, long), UserSession> _store = new();

    public Task<UserSession?> FindAsync(long telegramUserId, long chatId, CancellationToken cancellationToken = default)
    {
        _store.TryGetValue((telegramUserId, chatId), out var session);
        return Task.FromResult(session);
    }

    public Task AddAsync(UserSession session, CancellationToken cancellationToken = default)
    {
        _store.TryAdd((session.TelegramUserId, session.ChatId), session);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(UserSession session, CancellationToken cancellationToken = default)
    {
        _store[(session.TelegramUserId, session.ChatId)] = session;
        return Task.CompletedTask;
    }
}
