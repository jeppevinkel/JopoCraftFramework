namespace JopoCraftFramework.Domain.Entities;

public class GameEvent
{
    public Guid EventId { get; set; } = Guid.NewGuid();
    public string EventType { get; set; } = string.Empty;
    public string? ServerId { get; set; }
    public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
    public string? UserId { get; set; }
    public string Payload { get; set; } = string.Empty;

    public Player? Player { get; set; }
}
