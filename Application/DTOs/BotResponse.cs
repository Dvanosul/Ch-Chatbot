namespace Ch_Chatbot.Application.DTOs;

/// <summary>
/// Encapsulates a reply to be sent back to Telegram.
/// The Bot layer reads this and calls the appropriate Telegram API method.
/// Future phases may add media attachments, inline keyboards, etc.
/// </summary>
public sealed record BotResponse(
    long ChatId,
    string Text,
    int? ReplyToMessageId = null
);
