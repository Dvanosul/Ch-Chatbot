using Ch_Chatbot.Application.Interfaces.Repositories;
using Ch_Chatbot.Domain.Entities;
using System.Collections.Concurrent;

namespace Ch_Chatbot.Infrastructure.Repositories;

/// <summary>
/// Temporary in-memory implementation of ICharacterRepository.
/// Seeded with a single demo character for testing purposes.
/// Must be replaced by a PostgreSQL-backed implementation in the next phase.
/// </summary>
public sealed class InMemoryCharacterRepository : ICharacterRepository
{
    private readonly ConcurrentDictionary<Guid, Character> _store;

    public InMemoryCharacterRepository()
    {
        // Seed a demo character so the bot has something to reference before the DB is ready.
        var demo = new Character
        {
            Name        = "Aria",
            Description = "A friendly and curious AI companion. (Demo character — replace via DB in next phase.)",
            IsActive    = true
        };
        _store = new ConcurrentDictionary<Guid, Character> { [demo.Id] = demo };
    }

    public Task<Character?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _store.TryGetValue(id, out var character);
        return Task.FromResult(character);
    }

    public Task<IReadOnlyList<Character>> GetAllActiveAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Character> result = _store.Values.Where(c => c.IsActive).ToList();
        return Task.FromResult(result);
    }

    public Task AddAsync(Character character, CancellationToken cancellationToken = default)
    {
        _store.TryAdd(character.Id, character);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Character character, CancellationToken cancellationToken = default)
    {
        _store[character.Id] = character;
        return Task.CompletedTask;
    }
}
