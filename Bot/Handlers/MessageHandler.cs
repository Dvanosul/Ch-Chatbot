using Ch_Chatbot.Application.DTOs;
using Ch_Chatbot.Application.Interfaces.Services;
using Ch_Chatbot.Infrastructure.Telegram;

namespace Ch_Chatbot.Bot.Handlers;

/// <summary>
/// Handles plain text messages (non-commands).
/// Delegates business logic entirely to IMessageService and forwards the result via ITelegramSender.
/// </summary>
public sealed class MessageHandler
{
    private readonly IMessageService _messageService;
    private readonly ITelegramSender _telegramSender;
    private readonly ILogger<MessageHandler> _logger;

    public MessageHandler(
        IMessageService messageService,
        ITelegramSender telegramSender,
        ILogger<MessageHandler> logger)
    {
        _messageService = messageService;
        _telegramSender = telegramSender;
        _logger = logger;
    }

    public async Task HandleAsync(MessageContext context, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("MessageHandler processing text from {UserId}", context.Sender.UserId);

        var response = await _messageService.HandleAsync(context, cancellationToken);
        await _telegramSender.SendTextAsync(response, cancellationToken);
    }
}
