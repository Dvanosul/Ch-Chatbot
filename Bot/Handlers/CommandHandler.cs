using Ch_Chatbot.Application.DTOs;
using Ch_Chatbot.Application.Interfaces.Services;
using Ch_Chatbot.Common.Helpers;
using Ch_Chatbot.Infrastructure.Telegram;

namespace Ch_Chatbot.Bot.Handlers;

/// <summary>
/// Handles Telegram slash commands (/start, /help, etc.).
/// Parses the command string into a BotCommand enum and delegates to ICommandService.
/// </summary>
public sealed class CommandHandler
{
    private readonly ICommandService _commandService;
    private readonly ITelegramSender _telegramSender;
    private readonly ILogger<CommandHandler> _logger;

    public CommandHandler(
        ICommandService commandService,
        ITelegramSender telegramSender,
        ILogger<CommandHandler> logger)
    {
        _commandService = commandService;
        _telegramSender = telegramSender;
        _logger = logger;
    }

    public async Task HandleAsync(MessageContext context, CancellationToken cancellationToken = default)
    {
        var command = CommandParser.Parse(context.Text);

        _logger.LogDebug("CommandHandler executing {Command} for {UserId}", command, context.Sender.UserId);

        var response = await _commandService.ExecuteAsync(command, context, cancellationToken);
        await _telegramSender.SendTextAsync(response, cancellationToken);
    }
}
