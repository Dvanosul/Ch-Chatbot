using Ch_Chatbot.Application.Interfaces.Services;
using Ch_Chatbot.Bot.Handlers;
using Ch_Chatbot.Common.Helpers;
using DomainMessageType = Ch_Chatbot.Domain.Enums.MessageType;
using Telegram.Bot.Types;

namespace Ch_Chatbot.Bot.Updates;

/// <summary>
/// Entry point for all incoming Telegram updates.
/// Inspects update type and routes to the appropriate handler.
/// Future phases may add CallbackQueryHandler, InlineQueryHandler, etc.
/// </summary>
public sealed class UpdateRouter
{
    private readonly MessageHandler _messageHandler;
    private readonly CommandHandler _commandHandler;
    private readonly ILogger<UpdateRouter> _logger;

    public UpdateRouter(
        MessageHandler messageHandler,
        CommandHandler commandHandler,
        ILogger<UpdateRouter> logger)
    {
        _messageHandler = messageHandler;
        _commandHandler = commandHandler;
        _logger = logger;
    }

    public async Task RouteAsync(Update update, CancellationToken cancellationToken = default)
    {
        if (update.Message is null)
        {
            _logger.LogDebug("Skipping non-message update of type {Type}", update.Type);
            return;
        }

        var context = UpdateMapper.ToMessageContext(update.Message);

        await (context.Type == DomainMessageType.Command
            ? _commandHandler.HandleAsync(context, cancellationToken)
            : _messageHandler.HandleAsync(context, cancellationToken));
    }
}
