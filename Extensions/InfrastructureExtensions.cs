using Ch_Chatbot.Application.Interfaces.Repositories;
using Ch_Chatbot.Bot.Handlers;
using Ch_Chatbot.Bot.Updates;
using Ch_Chatbot.Configuration;
using Ch_Chatbot.Infrastructure.Persistence;
using Ch_Chatbot.Infrastructure.Repositories;
using Ch_Chatbot.Infrastructure.Telegram;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;

namespace Ch_Chatbot.Extensions;

/// <summary>
/// DI registration for Infrastructure layer: EF Core, Telegram client, sender, repositories, and bot handlers.
/// </summary>
public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // ── Database ──────────────────────────────────────────────────────────
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // ── Repositories ──────────────────────────────────────────────────────
        services.AddScoped<ICharacterRepository, PostgresCharacterRepository>();
        services.AddScoped<ISessionRepository, PostgresSessionRepository>();

        // ── Telegram ──────────────────────────────────────────────────────────
        services.Configure<TelegramOptions>(configuration.GetSection(TelegramOptions.SectionName));

        var botToken = configuration[$"{TelegramOptions.SectionName}:BotToken"]
            ?? throw new InvalidOperationException($"Missing configuration: {TelegramOptions.SectionName}:BotToken");

        services.AddSingleton<ITelegramBotClient>(_ => new TelegramBotClient(botToken));
        services.AddScoped<ITelegramSender, TelegramSender>();

        // ── Bot routing & handlers ────────────────────────────────────────────
        services.AddScoped<UpdateRouter>();
        services.AddScoped<MessageHandler>();
        services.AddScoped<CommandHandler>();

        // Background polling — pulls updates from Telegram continuously.
        services.AddHostedService<TelegramPollingService>();

        return services;
    }
}
