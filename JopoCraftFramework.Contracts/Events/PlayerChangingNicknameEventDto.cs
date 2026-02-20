using JopoCraftFramework.Contracts.Models;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when a player's nickname is changing
    /// </summary>
    public class PlayerChangingNicknameEventDto : BaseEventDto
    {
        public PlayerChangingNicknameEventDto()
        {
            EventType = nameof(PlayerChangingNicknameEventDto);
        }

        /// <summary>
        /// The player whose nickname is changing
        /// </summary>
        public PlayerDto Player { get; set; }

        /// <summary>
        /// The player's previous nickname
        /// </summary>
        public string OldNickname { get; set; }

        /// <summary>
        /// The player's new nickname
        /// </summary>
        public string NewNickname { get; set; }
    }
}
