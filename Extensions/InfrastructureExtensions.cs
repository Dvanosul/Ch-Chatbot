using Ch_Chatbot.Application.Interfaces.Repositories;
using Ch_Chatbot.Bot.Handlers;
using Ch_Chatbot.Bot.Updates;
using Ch_Chatbot.Configuration;
using Ch_Chatbot.Infrastructure.Repositories;
using Ch_Chatbot.Infrastructure.Telegram;
using Telegram.Bot;

namespace Ch_Chatbot.Extensions;

/// <summary>
/// DI registration for Infrastructure layer: Telegram client, sender, repositories, and bot handlers.
/// Future phase: swap in-memory repositories for PostgreSQL-backed ones here.
/// </summary>
public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Bind Telegram options from appsettings.json
        services.Configure<TelegramOptions>(configuration.GetSection(TelegramOptions.SectionName));

        var botToken = configuration[$"{TelegramOptions.SectionName}:BotToken"]
            ?? throw new InvalidOperationException($"Missing configuration: {TelegramOptions.SectionName}:BotToken");

        // Register Telegram bot client as a singleton (one client per application lifetime)
        services.AddSingleton<ITelegramBotClient>(_ => new TelegramBotClient(botToken));

        // Telegram sender wraps the bot client for outbound messages
        services.AddScoped<ITelegramSender, TelegramSender>();

        // In-memory repositories (replace with PostgreSQL implementations in next phase)
        services.AddSingleton<ISessionRepository, InMemorySessionRepository>();
        services.AddSingleton<ICharacterRepository, InMemoryCharacterRepository>();

        // Bot routing and handlers
        services.AddScoped<UpdateRouter>();
        services.AddScoped<MessageHandler>();
        services.AddScoped<CommandHandler>();

        return services;
    }
}
