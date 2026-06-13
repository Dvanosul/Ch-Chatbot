using Ch_Chatbot.Application.DTOs;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Ch_Chatbot.Infrastructure.Telegram;

/// <summary>
/// Concrete Telegram sender that wraps ITelegramBotClient.
/// All outbound Telegram API calls are centralised here.
/// Future phases may add support for sending photos, stickers, and inline keyboards.
/// </summary>
public sealed class TelegramSender : ITelegramSender
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<TelegramSender> _logger;

    public TelegramSender(ITelegramBotClient botClient, ILogger<TelegramSender> logger)
    {
        _botClient = botClient;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task SendTextAsync(BotResponse response, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Sending message to chat {ChatId}", response.ChatId);

        ReplyParameters? replyParams = response.ReplyToMessageId.HasValue
            ? new ReplyParameters { MessageId = response.ReplyToMessageId.Value }
            : null;

        await _botClient.SendMessage(
            chatId:            response.ChatId,
            text:              response.Text,
            parseMode:         ParseMode.None,
            replyParameters:   replyParams,
            cancellationToken: cancellationToken
        );
    }
}
