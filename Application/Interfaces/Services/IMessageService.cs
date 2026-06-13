using Ch_Chatbot.Application.DTOs;

namespace Ch_Chatbot.Application.Interfaces.Services;

/// <summary>
/// Orchestrates incoming message processing.
/// Implementations will validate context, resolve the active character,
/// delegate to the AI provider (future), and return a typed response.
/// </summary>
public interface IMessageService
{
    Task<BotResponse> HandleAsync(MessageContext context, CancellationToken cancellationToken = default);
}
