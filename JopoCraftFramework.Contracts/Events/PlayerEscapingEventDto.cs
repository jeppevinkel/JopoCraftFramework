using JopoCraftFramework.Contracts.Models;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when a player is escaping
    /// </summary>
    public class PlayerEscapingEventDto : BaseEventDto
    {
        public PlayerEscapingEventDto()
        {
            EventType = nameof(PlayerEscapingEventDto);
        }

        /// <summary>
        /// The player who is escaping
        /// </summary>
        public PlayerDto Player { get; set; }

        /// <summary>
        /// The role the player will receive after escaping
        /// </summary>
        public string NewRole { get; set; }

        /// <summary>
        /// Whether the player is handcuffed while escaping
        /// </summary>
        public bool IsHandcuffed { get; set; }
    }
}
