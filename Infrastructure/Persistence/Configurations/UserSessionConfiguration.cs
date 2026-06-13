using Ch_Chatbot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ch_Chatbot.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core table mapping for the UserSession entity.
/// Future phases: add columns for conversation history, mood state, relationship score, etc.
/// </summary>
public sealed class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
{
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder.ToTable("user_sessions");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(s => s.TelegramUserId)
            .HasColumnName("telegram_user_id")
            .IsRequired();

        builder.Property(s => s.ChatId)
            .HasColumnName("chat_id")
            .IsRequired();

        builder.Property(s => s.ActiveCharacterId)
            .HasColumnName("active_character_id");

        builder.Property(s => s.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("now()");

        builder.Property(s => s.LastInteractionAt)
            .HasColumnName("last_interaction_at")
            .HasDefaultValueSql("now()");

        // One session per user per chat.
        builder.HasIndex(s => new { s.TelegramUserId, s.ChatId })
            .IsUnique()
            .HasDatabaseName("ix_user_sessions_user_chat");
    }
}
