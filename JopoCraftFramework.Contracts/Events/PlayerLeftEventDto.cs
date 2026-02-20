using JopoCraftFramework.Contracts.Models;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when a player leaves the server
    /// </summary>
    public class PlayerLeftEventDto : BaseEventDto
    {
        public PlayerLeftEventDto()
        {
            EventType = nameof(PlayerLeftEventDto);
        }
        
        /// <summary>
        /// Information about the player who left
        /// </summary>
        public PlayerDto Player { get; set; }
    }
}
