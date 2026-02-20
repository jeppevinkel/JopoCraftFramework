using Exiled.Events.EventArgs.Player;
using JopoCraftFramework.Contracts.Events;
using JopoCraftFramework.Plugin.Api;

namespace JopoCraftFramework.Plugin.EventHandlers
{
    /// <summary>
    /// Handles role and escape events, as well as identity changes and overwatch toggling.
    /// </summary>
    public class PlayerRoleEventHandlers : BaseEventHandlers
    {
        public PlayerRoleEventHandlers(IApiClient apiClient) : base(apiClient) { }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            ApiClient.SendEvent(new PlayerChangingRoleEventDto
            {
                Player       = ToDto(ev.Player),
                OldRole      = ev.Player?.Role?.Type.ToString(),
                NewRole      = ev.NewRole.ToString(),
                ChangeReason = ev.Reason.ToString()
            });
        }

        public void OnEscaping(EscapingEventArgs ev)
        {
            ApiClient.SendEvent(new PlayerEscapingEventDto
            {
                Player       = ToDto(ev.Player),
                NewRole      = ev.NewRole.ToString(),
                IsHandcuffed = ev.EscapeScenario.ToString().StartsWith("Cuffed")
            });
        }

        public void OnChangingNickname(ChangingNicknameEventArgs ev)
        {
            ApiClient.SendEvent(new PlayerChangingNicknameEventDto
            {
                Player      = ToDto(ev.Player),
                OldNickname = ev.OldName,
                NewNickname = ev.NewName
            });
        }

        public void OnTogglingOverwatch(TogglingOverwatchEventArgs ev)
        {
            ApiClient.SendEvent(new PlayerTogglingOverwatchEventDto
            {
                Player     = ToDto(ev.Player),
                IsEnabled  = ev.IsEnabled
            });
        }

        public void OnPreAuthenticating(PreAuthenticatingEventArgs ev)
        {
            ApiClient.SendEvent(new PlayerPreAuthenticatingEventDto
            {
                UserId      = ev.UserId,
                IpAddress   = ev.IpAddress,
                CountryCode = ev.Country
            });
        }
    }
}
