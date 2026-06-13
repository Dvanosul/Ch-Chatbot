using Ch_Chatbot.Application.Interfaces.Repositories;
using Ch_Chatbot.Domain.Entities;
using Ch_Chatbot.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ch_Chatbot.Infrastructure.Repositories;

/// <summary>
/// PostgreSQL-backed implementation of ICharacterRepository using EF Core.
/// Replaces InMemoryCharacterRepository for all non-test environments.
/// </summary>
public sealed class PostgresCharacterRepository : ICharacterRepository
{
    private readonly AppDbContext _db;

    public PostgresCharacterRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Character?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _db.Characters.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<Character>> GetAllActiveAsync(CancellationToken cancellationToken = default)
        => await _db.Characters
            .Where(c => c.IsActive)
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);

    public async Task AddAsync(Character character, CancellationToken cancellationToken = default)
    {
        await _db.Characters.AddAsync(character, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Character character, CancellationToken cancellationToken = default)
    {
        character.UpdatedAt = DateTime.UtcNow;
        _db.Characters.Update(character);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
