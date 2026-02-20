namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when a player is pre-authenticating with the server
    /// </summary>
    public class PlayerPreAuthenticatingEventDto : BaseEventDto
    {
        public PlayerPreAuthenticatingEventDto()
        {
            EventType = nameof(PlayerPreAuthenticatingEventDto);
        }

        /// <summary>
        /// The player's user ID attempting to authenticate
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The player's IP address
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// The player's country code derived from their IP (if available)
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// The authentication region the player is connecting from
        /// </summary>
        public string Region { get; set; }
    }
}
