using System;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Base class for all game event DTOs
    /// </summary>
    public abstract class BaseEventDto
    {
        /// <summary>
        /// Unique identifier for this event
        /// </summary>
        public Guid EventId { get; set; } = Guid.NewGuid();
        
        /// <summary>
        /// UTC timestamp when the event occurred
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Type of event (e.g., "PlayerJoined", "RoundStarted")
        /// </summary>
        public string EventType { get; set; }
        
        /// <summary>
        /// Server identifier (optional, can be set by the plugin)
        /// </summary>
        public string ServerId { get; set; }
    }
}
