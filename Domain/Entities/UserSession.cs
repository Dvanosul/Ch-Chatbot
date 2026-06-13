namespace Ch_Chatbot.Domain.Entities;

/// <summary>
/// Tracks per-user session data for a Telegram conversation.
/// Future phases will store active character, mood state, and relationship score here.
/// </summary>
public class UserSession
{
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>Telegram user ID.</summary>
    public long TelegramUserId { get; set; }

    /// <summary>Telegram chat ID where the session is active.</summary>
    public long ChatId { get; set; }

    /// <summary>ID of the character assigned to this session. Null means no character selected.</summary>
    public Guid? ActiveCharacterId { get; set; }

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime LastInteractionAt { get; set; } = DateTime.UtcNow;
}
