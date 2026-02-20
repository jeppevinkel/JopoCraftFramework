using Exiled.Events.EventArgs.Player;
using JopoCraftFramework.Contracts.Events;
using JopoCraftFramework.Plugin.Api;

namespace JopoCraftFramework.Plugin.EventHandlers
{
    /// <summary>
    /// Handles voice and text communication events: voice chat, intercom, and admin chat.
    /// </summary>
    public class PlayerCommunicationEventHandlers : BaseEventHandlers
    {
        public PlayerCommunicationEventHandlers(IApiClient apiClient) : base(apiClient) { }

        public void OnIntercomSpeaking(IntercomSpeakingEventArgs ev)
        {
            ApiClient.SendEvent(new PlayerIntercomSpeakingEventDto
            {
                Player = ToDto(ev.Player)
            });
        }

        public void OnVoiceChatting(VoiceChattingEventArgs ev)
        {
            ApiClient.SendEvent(new PlayerVoiceChattingEventDto
            {
                Player       = ToDto(ev.Player),
                VoiceChannel = ev.VoiceMessage.Channel.ToString()
            });
        }

        public void OnSendingAdminChatMessage(SendingAdminChatMessageEventsArgs ev)
        {
            ApiClient.SendEvent(new PlayerSendingAdminChatMessageEventDto
            {
                Player  = ToDto(ev.Player),
                Message = ev.Message
            });
        }
    }
}
