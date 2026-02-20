namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when a round ends
    /// </summary>
    public class RoundEndedEventDto : BaseEventDto
    {
        public RoundEndedEventDto()
        {
            EventType = nameof(RoundEndedEventDto);
        }
        
        /// <summary>
        /// Duration of the round in seconds
        /// </summary>
        public double RoundDurationSeconds { get; set; }
        
        /// <summary>
        /// Winning team. Matches EXILED's LeadingTeam enum: "ChaosInsurgency", "FacilityForces", "Anomalies", or "Draw".
        /// </summary>
        public string LeadingTeam { get; set; }

        /// <summary>
        /// Number of surviving MTF and guards. Derived from EXILED's SumInfo_ClassList.mtf_and_guards.
        /// </summary>
        public int MtfAndGuards { get; set; }

        /// <summary>
        /// Number of surviving Chaos Insurgents. Derived from EXILED's SumInfo_ClassList.chaos_insurgents.
        /// </summary>
        public int ChaosInsurgents { get; set; }

        /// <summary>
        /// Number of surviving SCPs. Derived from EXILED's SumInfo_ClassList.scps.
        /// </summary>
        public int Scps { get; set; }

        /// <summary>
        /// Number of surviving Scientists. Derived from EXILED's SumInfo_ClassList.scientists.
        /// </summary>
        public int Scientists { get; set; }

        /// <summary>
        /// Number of surviving Class-D personnel. Derived from EXILED's SumInfo_ClassList.class_ds.
        /// </summary>
        public int ClassDs { get; set; }
    }
}
