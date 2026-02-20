using JopoCraftFramework.Contracts.Models;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when a player is muted
    /// </summary>
    public class PlayerMuteIssuedEventDto : BaseEventDto
    {
        public PlayerMuteIssuedEventDto()
        {
            EventType = nameof(PlayerMuteIssuedEventDto);
        }

        /// <summary>
        /// The player who was muted
        /// </summary>
        public PlayerDto Target { get; set; }

        /// <summary>
        /// The staff member who issued the mute (null if issued by console/server)
        /// </summary>
        public PlayerDto Issuer { get; set; }

        /// <summary>
        /// Whether this is an intercom mute (true) or a global voice mute (false)
        /// </summary>
        public bool IsIntercomMute { get; set; }
    }
}
