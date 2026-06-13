namespace Ch_Chatbot.Domain.Enums;

/// <summary>
/// Categorises incoming Telegram messages so handlers can route them correctly.
/// </summary>
public enum MessageType
{
    Unknown = 0,
    Text,
    Command,
    Photo,
    Sticker,
    Voice,
    Document,
    CallbackQuery
}
