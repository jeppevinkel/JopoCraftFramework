namespace JopoCraftFramework.Domain.Entities;

public class Player
{
    public string UserId { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public string? IpAddress { get; set; }
    public DateTime FirstSeenUtc { get; set; } = DateTime.UtcNow;
    public DateTime LastSeenUtc { get; set; } = DateTime.UtcNow;

    public ICollection<GameEvent> GameEvents { get; set; } = new List<GameEvent>();
}
