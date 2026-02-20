using JopoCraftFramework.Contracts.Models;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when a player's mute is revoked
    /// </summary>
    public class PlayerMuteRevokedEventDto : BaseEventDto
    {
        public PlayerMuteRevokedEventDto()
        {
            EventType = nameof(PlayerMuteRevokedEventDto);
        }

        /// <summary>
        /// The player whose mute was revoked
        /// </summary>
        public PlayerDto Target { get; set; }

        /// <summary>
        /// The staff member who revoked the mute (null if revoked by console/server)
        /// </summary>
        public PlayerDto Issuer { get; set; }

        /// <summary>
        /// Whether this was an intercom mute (true) or a global voice mute (false)
        /// </summary>
        public bool IsIntercomMute { get; set; }
    }
}
