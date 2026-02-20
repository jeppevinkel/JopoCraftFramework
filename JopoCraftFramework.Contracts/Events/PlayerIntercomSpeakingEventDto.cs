using JopoCraftFramework.Contracts.Models;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when a player starts speaking on the intercom
    /// </summary>
    public class PlayerIntercomSpeakingEventDto : BaseEventDto
    {
        public PlayerIntercomSpeakingEventDto()
        {
            EventType = nameof(PlayerIntercomSpeakingEventDto);
        }

        /// <summary>
        /// The player speaking on the intercom
        /// </summary>
        public PlayerDto Player { get; set; }
    }
}
