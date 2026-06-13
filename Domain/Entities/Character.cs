namespace Ch_Chatbot.Domain.Entities;

/// <summary>
/// Represents a bot character with a defined persona.
/// Future phases will expand this with personality traits, memory references, and AI configuration.
/// </summary>
public class Character
{
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>Display name shown in responses.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Short description of the character's persona. Used as AI system prompt in future phases.</summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>Whether this character is currently active and can receive messages.</summary>
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
