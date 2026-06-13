using Ch_Chatbot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ch_Chatbot.Infrastructure.Persistence;

/// <summary>
/// Single EF Core DbContext for the entire application.
/// All DbSet properties and model configurations are centralised here.
/// Never access this class outside the Infrastructure layer.
/// </summary>
public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Character> Characters => Set<Character>();
    public DbSet<UserSession> UserSessions => Set<UserSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all IEntityTypeConfiguration classes found in this assembly automatically.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
