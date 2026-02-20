using JopoCraftFramework.Contracts.Models;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when a player sends a voice message
    /// </summary>
    public class PlayerVoiceChattingEventDto : BaseEventDto
    {
        public PlayerVoiceChattingEventDto()
        {
            EventType = nameof(PlayerVoiceChattingEventDto);
        }

        /// <summary>
        /// The player who is voice chatting
        /// </summary>
        public PlayerDto Player { get; set; }

        /// <summary>
        /// The voice channel being used (e.g., "Proximity", "Radio", "Spectator", "Scp")
        /// </summary>
        public string VoiceChannel { get; set; }
    }
}
