namespace Ch_Chatbot.Configuration;

/// <summary>
/// Strongly typed settings for the Telegram bot, bound from appsettings.json section "Telegram".
/// Example appsettings.json:
/// {
///   "Telegram": {
///     "BotToken": "your-bot-token",
///     "WebhookUrl": "https://yourdomain.com/webhook"
///   }
/// }
/// </summary>
public sealed class TelegramOptions
{
    public const string SectionName = "Telegram";

    /// <summary>Bot API token obtained from @BotFather.</summary>
    public string BotToken { get; init; } = string.Empty;

    /// <summary>
    /// Public HTTPS URL for receiving webhook updates.
    /// Leave empty to use polling mode instead (useful for local development).
    /// </summary>
    public string WebhookUrl { get; init; } = string.Empty;

    /// <summary>Returns true when a webhook URL is configured.</summary>
    public bool UseWebhook => !string.IsNullOrWhiteSpace(WebhookUrl);
}
