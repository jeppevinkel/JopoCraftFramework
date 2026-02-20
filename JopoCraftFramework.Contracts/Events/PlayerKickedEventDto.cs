using JopoCraftFramework.Contracts.Models;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered after a player has been kicked
    /// </summary>
    public class PlayerKickedEventDto : BaseEventDto
    {
        public PlayerKickedEventDto()
        {
            EventType = nameof(PlayerKickedEventDto);
        }

        /// <summary>
        /// The player who was kicked
        /// </summary>
        public PlayerDto Target { get; set; }

        /// <summary>
        /// The staff member who issued the kick (null if issued by console/server)
        /// </summary>
        public PlayerDto Issuer { get; set; }

        /// <summary>
        /// Reason for the kick
        /// </summary>
        public string Reason { get; set; }
    }
}
