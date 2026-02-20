using JopoCraftFramework.Contracts.Models;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when a player sends a message in admin chat
    /// </summary>
    public class PlayerSendingAdminChatMessageEventDto : BaseEventDto
    {
        public PlayerSendingAdminChatMessageEventDto()
        {
            EventType = nameof(PlayerSendingAdminChatMessageEventDto);
        }

        /// <summary>
        /// The staff member sending the admin chat message
        /// </summary>
        public PlayerDto Player { get; set; }

        /// <summary>
        /// The message content
        /// </summary>
        public string Message { get; set; }
    }
}
