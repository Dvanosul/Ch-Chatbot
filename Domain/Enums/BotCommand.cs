namespace Ch_Chatbot.Domain.Enums;

/// <summary>
/// Enumeration of supported Telegram slash commands.
/// New commands must be added here before implementing a handler.
/// </summary>
public enum BotCommand
{
    Unknown = 0,
    Start,
    Help,
    Select,  // future: /select <character>
    Info,    // future: /info – shows active character details
    Reset    // future: /reset – clears the current session
}
