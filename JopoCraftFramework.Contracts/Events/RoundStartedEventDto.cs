namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when a round starts
    /// </summary>
    public class RoundStartedEventDto : BaseEventDto
    {
        public RoundStartedEventDto()
        {
            EventType = nameof(RoundStartedEventDto);
        }
        
        /// <summary>
        /// Number of players at round start
        /// </summary>
        public int PlayerCount { get; set; }
        
        /// <summary>
        /// Round number (if tracked by the plugin)
        /// </summary>
        public int RoundNumber { get; set; }
    }
}
