using JopoCraftFramework.Contracts.Models;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when a player is being kicked
    /// </summary>
    public class PlayerKickingEventDto : BaseEventDto
    {
        public PlayerKickingEventDto()
        {
            EventType = nameof(PlayerKickingEventDto);
        }

        /// <summary>
        /// The player being kicked
        /// </summary>
        public PlayerDto Target { get; set; }

        /// <summary>
        /// The staff member issuing the kick (null if issued by console/server)
        /// </summary>
        public PlayerDto Issuer { get; set; }

        /// <summary>
        /// Reason for the kick
        /// </summary>
        public string Reason { get; set; }
    }
}
