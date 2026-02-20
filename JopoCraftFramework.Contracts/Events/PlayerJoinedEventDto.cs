using JopoCraftFramework.Contracts.Models;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when a player joins the server
    /// </summary>
    public class PlayerJoinedEventDto : BaseEventDto
    {
        public PlayerJoinedEventDto()
        {
            EventType = nameof(PlayerJoinedEventDto);
        }
        
        /// <summary>
        /// Information about the player who joined
        /// </summary>
        public PlayerDto Player { get; set; }
    }
}
