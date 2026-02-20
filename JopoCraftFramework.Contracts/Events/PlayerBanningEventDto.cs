using System;
using JopoCraftFramework.Contracts.Models;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when a player is being banned
    /// </summary>
    public class PlayerBanningEventDto : BaseEventDto
    {
        public PlayerBanningEventDto()
        {
            EventType = nameof(PlayerBanningEventDto);
        }

        /// <summary>
        /// The player being banned
        /// </summary>
        public PlayerDto Target { get; set; }

        /// <summary>
        /// The staff member issuing the ban (null if issued by console/server)
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
