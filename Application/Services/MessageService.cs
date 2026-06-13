using Ch_Chatbot.Application.DTOs;
using Ch_Chatbot.Application.Interfaces.Services;

namespace Ch_Chatbot.Application.Services;

/// <summary>
/// Placeholder implementation of IMessageService.
/// Business logic (character resolution, AI delegation, etc.) will be added in future phases.
/// </summary>
public sealed class MessageService : IMessageService
{
    private readonly ILogger<MessageService> _logger;

    public MessageService(ILogger<MessageService> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public Task<BotResponse> HandleAsync(MessageContext context, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Received message from {UserId} in chat {ChatId}", context.Sender.UserId, context.ChatId);

        // TODO: resolve active character, call AI provider, build a rich response.
        var response = new BotResponse(context.ChatId, "👋 Bot is running. Full character support coming soon!");
        return Task.FromResult(response);
    }
}
