namespace Ch_Chatbot.Common.Constants;

/// <summary>
/// Compile-time constants shared across all layers.
/// Centralising them here prevents magic strings from scattering through the codebase.
/// </summary>
public static class BotConstants
{
    // ── Command prefixes ─────────────────────────────────────────────────────
    public const string CommandPrefix = "/";

    // ── Command strings ──────────────────────────────────────────────────────
    public const string CmdStart  = "/start";
    public const string CmdHelp   = "/help";
    public const string CmdSelect = "/select";
    public const string CmdInfo   = "/info";
    public const string CmdReset  = "/reset";

    // ── Fallback messages ────────────────────────────────────────────────────
    public const string FallbackReply   = "🤖 I didn't understand that. Try /help.";
    public const string UnknownCommand  = "❓ Unknown command. Use /help for a list.";
    public const string ServiceUnavailable = "⚠️ Service temporarily unavailable. Please try again later.";
}
