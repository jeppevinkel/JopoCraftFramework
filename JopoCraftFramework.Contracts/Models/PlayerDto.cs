namespace JopoCraftFramework.Contracts.Models
{
    /// <summary>
    /// Represents player information
    /// </summary>
    public class PlayerDto
    {
        /// <summary>
        /// Player's user ID
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Player's nickname
        /// </summary>
        public string Nickname { get; set; }
        
        /// <summary>
        /// Player's current role (e.g., "ClassD", "NtfSergeant", "Scp173")
        /// </summary>
        public string Role { get; set; }
        
        /// <summary>
        /// Player's IP address (optional, for moderation purposes)
        /// </summary>
        public string IpAddress { get; set; }
    }
}
