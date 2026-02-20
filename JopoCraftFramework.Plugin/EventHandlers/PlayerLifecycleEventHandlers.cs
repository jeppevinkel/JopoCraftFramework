using Exiled.Events.EventArgs.Player;
using JopoCraftFramework.Contracts.Events;
using JopoCraftFramework.Plugin.Api;

namespace JopoCraftFramework.Plugin.EventHandlers
{
    /// <summary>
    /// Handles player connection lifecycle events: join, leave, and death.
    /// </summary>
    public class PlayerLifecycleEventHandlers : BaseEventHandlers
    {
        public PlayerLifecycleEventHandlers(IApiClient apiClient) : base(apiClient) { }

        public void OnJoined(JoinedEventArgs ev)
        {
            ApiClient.SendEvent(new PlayerJoinedEventDto
            {
                Player = ToDto(ev.Player)
            });
        }

        public void OnLeft(LeftEventArgs ev)
        {
            ApiClient.SendEvent(new PlayerLeftEventDto
            {
                Player = ToDto(ev.Player)
            });
        }

        public void OnDying(DyingEventArgs ev)
        {
            ApiClient.SendEvent(new PlayerDiedEventDto
            {
                Victim     = ToDto(ev.Player),
                Attacker   = ToDto(ev.Attacker),
                DamageType = ev.DamageHandler.Type.ToString()
            });
        }
    }
}
