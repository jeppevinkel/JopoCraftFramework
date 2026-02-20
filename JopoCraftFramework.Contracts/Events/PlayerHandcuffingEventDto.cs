using JopoCraftFramework.Contracts.Models;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when a player is being handcuffed
    /// </summary>
    public class PlayerHandcuffingEventDto : BaseEventDto
    {
        public PlayerHandcuffingEventDto()
        {
            EventType = nameof(PlayerHandcuffingEventDto);
        }

        /// <summary>
        /// The player applying the handcuffs
        /// </summary>
        public PlayerDto Cuffer { get; set; }

        /// <summary>
        /// The player being handcuffed
        /// </summary>
        public PlayerDto Target { get; set; }
    }
}
