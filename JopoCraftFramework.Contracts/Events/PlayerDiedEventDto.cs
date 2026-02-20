using JopoCraftFramework.Contracts.Models;

namespace JopoCraftFramework.Contracts.Events
{
    /// <summary>
    /// Event triggered when a player dies
    /// </summary>
    public class PlayerDiedEventDto : BaseEventDto
    {
        public PlayerDiedEventDto()
        {
            EventType = nameof(PlayerDiedEventDto);
        }
        
        /// <summary>
        /// Information about the player who died
        /// </summary>
        public PlayerDto Victim { get; set; }
        
        /// <summary>
        /// Information about the attacker (null if suicide or environmental death). Matches EXILED's DyingEventArgs.Attacker.
        /// </summary>
        public PlayerDto Attacker { get; set; }
        
        /// <summary>
        /// Human-readable damage type or cause of death (e.g., "Firearm", "Fall", "Scp173"). Derived from EXILED's CustomDamageHandler.
        /// </summary>
        public string DamageType { get; set; }
        
        /// <summary>
        /// Amount of damage dealt
        /// </summary>
        public float Damage { get; set; }
    }
}
