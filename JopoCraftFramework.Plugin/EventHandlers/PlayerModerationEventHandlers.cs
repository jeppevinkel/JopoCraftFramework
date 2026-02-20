using System;
using Exiled.Events.EventArgs.Player;
using JopoCraftFramework.Contracts.Events;
using JopoCraftFramework.Plugin.Api;

namespace JopoCraftFramework.Plugin.EventHandlers
{
    /// <summary>
    /// Handles moderation events: kicks, bans, mutes, and handcuffs.
    /// </summary>
    public class PlayerModerationEventHandlers : BaseEventHandlers
    {
        public PlayerModerationEventHandlers(IApiClient apiClient) : base(apiClient) { }

        public void OnKicking(KickingEventArgs ev)
        {
            ApiClient.SendEvent(new PlayerKickingEventDto
            {
                Target = ToDto(ev.Target),
                Issuer = ToDto(ev.Player),
                Reason = ev.Reason
            });
        }

        public void OnKicked(KickedEventArgs ev)
        {
            ApiClient.SendEvent(new PlayerKickedEventDto
            {
                Target = ToDto(ev.Player),
                Reason = ev.Reason
            });
        }

        public void OnBanning(BanningEventArgs ev)
        {
            ApiClient.SendEvent(new PlayerBanningEventDto
            {
                Target    = ToDto(ev.Target),
                Issuer    = ToDto(ev.Player),
                Reason    = ev.Reason,
                Duration  = TimeSpan.FromSeconds(ev.Duration),
                ExpiresAt = DateTime.UtcNow.AddSeconds(ev.Duration)
            });
        }

        public void OnBanned(BannedEventArgs ev)
        {
            ApiClient.SendEvent(new PlayerBannedEventDto
            {
                Target    = ToDto(ev.Target),
                Issuer    = ToDto(ev.Player),
                Reason    = ev.Details.Reason,
                Duration  = TimeSpan.FromSeconds(ev.Details.Expires - ev.Details.IssuanceTime),
                ExpiresAt = DateTimeOffset.FromUnixTimeSeconds(ev.Details.Expires).UtcDateTime
            });
        }

        public void OnIssuingMute(IssuingMuteEventArgs ev)
        {
            ApiClient.SendEvent(new PlayerMuteIssuedEventDto
            {
                Target         = ToDto(ev.Player),
                IsIntercomMute = ev.IsIntercom
            });
        }

        public void OnRevokingMute(RevokingMuteEventArgs ev)
        {
            ApiClient.SendEvent(new PlayerMuteRevokedEventDto
            {
                Target         = ToDto(ev.Player),
                IsIntercomMute = ev.IsIntercom
            });
        }

        public void OnHandcuffing(HandcuffingEventArgs ev)
        {
            ApiClient.SendEvent(new PlayerHandcuffingEventDto
            {
                Cuffer = ToDto(ev.Player),
                Target = ToDto(ev.Target)
            });
        }

        public void OnRemovingHandcuffs(RemovingHandcuffsEventArgs ev)
        {
            ApiClient.SendEvent(new PlayerRemovingHandcuffsEventDto
            {
                Uncuffer = ToDto(ev.Player),
                Target   = ToDto(ev.Target)
            });
        }
    }
}
