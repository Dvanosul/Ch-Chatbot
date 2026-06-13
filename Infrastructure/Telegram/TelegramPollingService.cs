using Ch_Chatbot.Bot.Updates;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Ch_Chatbot.Infrastructure.Telegram;

/// <summary>
/// Long-running background service that pulls updates from Telegram using GetUpdates (polling).
/// Used during local development — no public URL required.
/// Replace with a webhook endpoint when deploying to production.
/// </summary>
public sealed class TelegramPollingService : BackgroundService
{
    private readonly ITelegramBotClient _botClient;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<TelegramPollingService> _logger;

    public TelegramPollingService(
        ITelegramBotClient botClient,
        IServiceScopeFactory scopeFactory,
        ILogger<TelegramPollingService> logger)
    {
        _botClient = botClient;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var me = await _botClient.GetMe(stoppingToken);
        _logger.LogInformation("Polling started — @{Username} (id: {Id})", me.Username, me.Id);

        var receiverOptions = new ReceiverOptions
        {
            // Only handle text messages and callback queries for now.
            // Expand this list as new update types are supported.
            AllowedUpdates = [UpdateType.Message, UpdateType.CallbackQuery],
            DropPendingUpdates = true   // ignore queued updates from while bot was offline
        };

        // ReceiveAsync overload: Func<client, update, ct, Task>, Func<client, exception, ct, Task>
        await _botClient.ReceiveAsync(
            updateHandler:     HandleUpdateAsync,
            errorHandler:      HandleErrorAsync,
            receiverOptions:   receiverOptions,
            cancellationToken: stoppingToken
        );
    }

    private async Task HandleUpdateAsync(ITelegramBotClient _, Update update, CancellationToken ct)
    {
        // Each update is processed in its own DI scope so scoped services work correctly.
        await using var scope = _scopeFactory.CreateAsyncScope();
        var router = scope.ServiceProvider.GetRequiredService<UpdateRouter>();

        try
        {
            await router.RouteAsync(update, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception while processing update {UpdateId}", update.Id);
        }
    }

    // Matches: Func<ITelegramBotClient, Exception, CancellationToken, Task>
    private Task HandleErrorAsync(ITelegramBotClient _, Exception exception, CancellationToken ct)
    {
        _logger.LogError(exception, "Telegram polling error");
        return Task.CompletedTask;
    }
}
