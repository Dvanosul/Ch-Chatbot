using Ch_Chatbot.Application.DTOs;
using Ch_Chatbot.Application.Interfaces.Services;
using Ch_Chatbot.Domain.Enums;

namespace Ch_Chatbot.Application.Services;

/// <summary>
/// Placeholder implementation of ICommandService.
/// Each BotCommand case will delegate to a dedicated command handler in future phases.
/// </summary>
public sealed class CommandService : ICommandService
{
    private readonly ILogger<CommandService> _logger;

    public CommandService(ILogger<CommandService> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public Task<BotResponse> ExecuteAsync(BotCommand command, MessageContext context, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Executing command {Command} for {UserId}", command, context.Sender.UserId);

        var text = command switch
        {
            BotCommand.Start  => "👋 Welcome! Use /help to see available commands.",
            BotCommand.Help   => "📖 Available commands:\n/start – begin\n/help – this menu\n/select – choose a character (coming soon)\n/info – character info (coming soon)\n/reset – reset session (coming soon)",
            BotCommand.Select => "🔧 Character selection coming soon.",
            BotCommand.Info   => "ℹ️ Character info coming soon.",
            BotCommand.Reset  => "🔄 Session reset coming soon.",
            _                 => "❓ Unknown command."
        };

        // TODO: implement real command logic per phase.
        return Task.FromResult(new BotResponse(context.ChatId, text));
    }
}
