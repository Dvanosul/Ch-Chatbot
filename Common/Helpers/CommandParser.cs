using Ch_Chatbot.Common.Constants;
using Ch_Chatbot.Domain.Enums;

namespace Ch_Chatbot.Common.Helpers;

/// <summary>
/// Parses raw Telegram command strings into typed BotCommand enum values.
/// Strips bot username suffix (e.g. "/start@MyBot" → BotCommand.Start).
/// </summary>
public static class CommandParser
{
    public static BotCommand Parse(string? text)
    {
        if (string.IsNullOrWhiteSpace(text) || !text.StartsWith(BotConstants.CommandPrefix))
            return BotCommand.Unknown;

        // Strip optional @BotUsername suffix
        var raw = text.Split(' ', '@')[0].ToLowerInvariant();

        return raw switch
        {
            BotConstants.CmdStart  => BotCommand.Start,
            BotConstants.CmdHelp   => BotCommand.Help,
            BotConstants.CmdSelect => BotCommand.Select,
            BotConstants.CmdInfo   => BotCommand.Info,
            BotConstants.CmdReset  => BotCommand.Reset,
            _                      => BotCommand.Unknown
        };
    }
}
