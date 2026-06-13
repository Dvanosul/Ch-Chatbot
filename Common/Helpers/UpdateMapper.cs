using Ch_Chatbot.Application.DTOs;
using Ch_Chatbot.Common.Constants;
using Ch_Chatbot.Domain.Enums;
using Ch_Chatbot.Domain.ValueObjects;
using Telegram.Bot.Types;

namespace Ch_Chatbot.Common.Helpers;

/// <summary>
/// Maps raw Telegram.Bot types to internal application DTOs.
/// Keeps Telegram SDK types contained within the Bot/Infrastructure layers.
/// </summary>
public static class UpdateMapper
{
    public static MessageContext ToMessageContext(Message message)
    {
        var from = message.From ?? throw new ArgumentException("Message has no sender.", nameof(message));

        var sender = new TelegramUser(
            UserId:    from.Id,
            ChatId:    message.Chat.Id,
            Username:  from.Username,
            FirstName: from.FirstName,
            LastName:  from.LastName
        );

        var text = message.Text ?? string.Empty;
        var type = DetermineType(text);

        return new MessageContext(
            Sender:        sender,
            Text:          text,
            Type:          type,
            ChatId:        message.Chat.Id,
            MessageId:     message.MessageId
        );
    }

    private static MessageType DetermineType(string text) =>
        text.StartsWith(BotConstants.CommandPrefix) ? MessageType.Command : MessageType.Text;
}
