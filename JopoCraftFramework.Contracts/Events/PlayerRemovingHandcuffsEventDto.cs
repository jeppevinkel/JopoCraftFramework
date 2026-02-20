using JopoCraftFramework.Contracts.Models;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when handcuffs are being removed from a player
    /// </summary>
    public class PlayerRemovingHandcuffsEventDto : BaseEventDto
    {
        public PlayerRemovingHandcuffsEventDto()
        {
            EventType = nameof(PlayerRemovingHandcuffsEventDto);
        }

        /// <summary>
        /// The player removing the handcuffs
        /// </summary>
        public PlayerDto Uncuffer { get; set; }

        /// <summary>
        /// The player being freed from handcuffs
        /// </summary>
        public PlayerDto Target { get; set; }
    }
}
