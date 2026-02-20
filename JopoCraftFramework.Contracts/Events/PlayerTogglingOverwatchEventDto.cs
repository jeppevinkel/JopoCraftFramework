using JopoCraftFramework.Contracts.Models;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when a player toggles overwatch mode
    /// </summary>
    public class PlayerTogglingOverwatchEventDto : BaseEventDto
    {
        public PlayerTogglingOverwatchEventDto()
        {
            EventType = nameof(PlayerTogglingOverwatchEventDto);
        }

        /// <summary>
        /// The player toggling overwatch
        /// </summary>
        public PlayerDto Player { get; set; }

        /// <summary>
        /// Whether overwatch is being enabled (true) or disabled (false)
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
