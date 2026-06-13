using Ch_Chatbot.Domain.Enums;
using Ch_Chatbot.Domain.ValueObjects;

namespace Ch_Chatbot.Application.DTOs;

/// <summary>
/// Normalised representation of an incoming Telegram update passed from Bot layer to Application layer.
/// Handlers build this DTO so services never depend on Telegram.Bot types directly.
/// </summary>
public sealed record MessageContext(
    TelegramUser Sender,
    string? Text,
    MessageType Type,
    long ChatId,
    int MessageId
);
