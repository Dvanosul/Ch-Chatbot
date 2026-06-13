using Ch_Chatbot.Domain.Entities;

namespace Ch_Chatbot.Application.Interfaces.Repositories;

/// <summary>
/// Data-access contract for Character aggregates.
/// Concrete implementations live in Infrastructure and will target PostgreSQL.
/// </summary>
public interface ICharacterRepository
{
    Task<Character?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Character>> GetAllActiveAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Character character, CancellationToken cancellationToken = default);
    Task UpdateAsync(Character character, CancellationToken cancellationToken = default);
}
