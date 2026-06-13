using Ch_Chatbot.Application.Interfaces.Services;
using Ch_Chatbot.Application.Services;

namespace Ch_Chatbot.Extensions;

/// <summary>
/// DI registration for all Application layer services.
/// Add new application services here as they are implemented.
/// </summary>
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<ICommandService, CommandService>();
        services.AddScoped<ISessionService, SessionService>();

        return services;
    }
}
