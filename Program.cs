using Ch_Chatbot.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ── Services ─────────────────────────────────────────────────────────────────
builder.Services
    .AddApplicationServices()
    .AddInfrastructure(builder.Configuration);

// ── App pipeline ──────────────────────────────────────────────────────────────
var app = builder.Build();

app.UseHttpsRedirection();

// Health-check endpoint — useful for uptime monitoring and webhook validation.
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }))
   .WithName("HealthCheck");

// TODO (next phase): add webhook endpoint -> POST /webhook -> UpdateRouter.RouteAsync(update)
// TODO (next phase): configure Telegram webhook registration on startup.

app.Run();
