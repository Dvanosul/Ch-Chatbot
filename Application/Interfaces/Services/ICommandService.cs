using Ch_Chatbot.Application.DTOs;
using Ch_Chatbot.Domain.Enums;

namespace Ch_Chatbot.Application.Interfaces.Services;

/// <summary>
/// Resolves and executes slash command logic.
/// Each BotCommand enum value should have a corresponding handler wired through this service.
/// </summary>
public interface ICommandService
{
    Task<BotResponse> ExecuteAsync(BotCommand command, MessageContext context, CancellationToken cancellationToken = default);
}
