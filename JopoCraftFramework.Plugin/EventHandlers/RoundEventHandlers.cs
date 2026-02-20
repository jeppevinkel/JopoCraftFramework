using Exiled.Events.EventArgs.Server;
using JopoCraftFramework.Contracts.Events;
using JopoCraftFramework.Plugin.Api;
using ExiledPlayer = Exiled.API.Features.Player;
using ExiledRound = Exiled.API.Features.Round;

namespace JopoCraftFramework.Plugin.EventHandlers
{
    /// <summary>
    /// Handles round lifecycle events: round start and round end.
    /// </summary>
    public class RoundEventHandlers : BaseEventHandlers
    {
        public RoundEventHandlers(IApiClient apiClient) : base(apiClient) { }

        public void OnRoundStarted()
        {
            ApiClient.SendEvent(new RoundStartedEventDto
            {
                PlayerCount = ExiledPlayer.List.Count,
                RoundNumber = ExiledRound.UptimeRounds
            });
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            ApiClient.SendEvent(new RoundEndedEventDto
            {
                RoundDurationSeconds = ExiledRound.ElapsedTime.TotalSeconds,
                LeadingTeam          = ev.LeadingTeam.ToString(),
                MtfAndGuards         = ev.ClassList.mtf_and_guards,
                ChaosInsurgents      = ev.ClassList.chaos_insurgents,
                Scps                 = ev.ClassList.zombies + ev.ClassList.scps_except_zombies,
                Scientists           = ev.ClassList.scientists,
                ClassDs              = ev.ClassList.class_ds
            });
        }
    }
}
