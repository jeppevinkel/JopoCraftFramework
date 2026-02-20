using System;
using JopoCraftFramework.Contracts.Models;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered after a player has been banned
    /// </summary>
    public class PlayerBannedEventDto : BaseEventDto
    {
        public PlayerBannedEventDto()
        {
            EventType = nameof(PlayerBannedEventDto);
        }

        /// <summary>
        /// The player who was banned
        /// </summary>
        public PlayerDto Target { get; set; }

        /// <summary>
        /// The staff member who issued the ban (null if issued by console/server)
        /// </summary>
        public PlayerDto Issuer { get; set; }

        /// <summary>
        /// Reason for the ban
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Duration of the ban
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// UTC time when the ban expires
        /// </summary>
        public DateTime ExpiresAt { get; set; }
    }
}
