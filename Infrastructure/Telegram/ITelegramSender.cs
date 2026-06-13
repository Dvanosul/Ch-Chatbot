using Ch_Chatbot.Application.DTOs;

namespace Ch_Chatbot.Infrastructure.Telegram;

/// <summary>
/// Abstraction over the Telegram Bot API for sending messages.
/// Decouples Bot handlers from the Telegram SDK so they can be tested without a live bot.
/// Implementations will use ITelegramBotClient internally.
/// </summary>
public interface ITelegramSender
{
    /// <summary>Sends a plain-text message to the specified chat.</summary>
    Task SendTextAsync(BotResponse response, CancellationToken cancellationToken = default);
}
