namespace Ch_Chatbot.Domain.ValueObjects;

/// <summary>
/// Immutable snapshot of a Telegram user's identifying data extracted from an incoming Update.
/// Value objects carry no identity of their own — equality is based on their properties.
/// </summary>
public sealed record TelegramUser(
    long UserId,
    long ChatId,
    string? Username,
    string FirstName,
    string? LastName
)
{
    /// <summary>Returns the full display name, falling back gracefully.</summary>
    public string DisplayName =>
        string.IsNullOrWhiteSpace(LastName)
            ? FirstName
            : $"{FirstName} {LastName}";
}
