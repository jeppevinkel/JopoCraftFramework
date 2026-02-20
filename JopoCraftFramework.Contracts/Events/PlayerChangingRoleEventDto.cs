using JopoCraftFramework.Contracts.Models;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when a player's role is changing
    /// </summary>
    public class PlayerChangingRoleEventDto : BaseEventDto
    {
        public PlayerChangingRoleEventDto()
        {
            EventType = nameof(PlayerChangingRoleEventDto);
        }

        /// <summary>
        /// The player whose role is changing
        /// </summary>
        public PlayerDto Player { get; set; }

        /// <summary>
        /// The player's previous role
        /// </summary>
        public string OldRole { get; set; }

        /// <summary>
        /// The player's new role
        /// </summary>
        public string NewRole { get; set; }

        /// <summary>
        /// Reason for the role change (e.g., "Respawn", "Escaped", "AdminForced", "RoundStart")
        /// </summary>
        public string ChangeReason { get; set; }
    }
}
