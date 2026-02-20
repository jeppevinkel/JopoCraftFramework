using System;
using Exiled.API.Features;
using JopoCraftFramework.Plugin.Api;
using JopoCraftFramework.Plugin.EventHandlers;
using PlayerEvents = Exiled.Events.Handlers.Player;
using ServerEvents = Exiled.Events.Handlers.Server;

namespace JopoCraftFramework.Plugin
{
    /// <summary>
    /// EXILED plugin entry point for JopoCraftFramework.
    /// Subscribes to game events on load and unsubscribes on unload so that
    /// server reloads do not cause duplicate or dangling event handlers.
    /// </summary>
    public class JopoCraftPlugin : Plugin<Config>
    {
        private IApiClient apiClient;

        private PlayerLifecycleEventHandlers    lifecycleHandlers;
        private PlayerModerationEventHandlers   moderationHandlers;
        private PlayerRoleEventHandlers         roleHandlers;
        private PlayerCommunicationEventHandlers communicationHandlers;
        private RoundEventHandlers              roundHandlers;

        public override string Name        => "JopoCraftFramework";
        public override string Author      => "JopoCraft";
        public override string Prefix      => "jopo";
        public override Version Version    => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 0, 0);

        /// <summary>
        /// Called by EXILED when the plugin is enabled (initial load or reload).
        /// </summary>
        public override void OnEnabled()
        {
            apiClient = new HttpApiClient(Config);

            lifecycleHandlers     = new PlayerLifecycleEventHandlers(apiClient);
            moderationHandlers    = new PlayerModerationEventHandlers(apiClient);
            roleHandlers          = new PlayerRoleEventHandlers(apiClient);
            communicationHandlers = new PlayerCommunicationEventHandlers(apiClient);
            roundHandlers         = new RoundEventHandlers(apiClient);

            SubscribeEvents();
            base.OnEnabled();
            Log.Info($"{Name} v{Version} enabled. Dispatching events to {Config.EventEndpointUrl}");
        }

        /// <summary>
        /// Called by EXILED when the plugin is disabled (unload or reload).
        /// Unsubscribes all handlers to prevent duplicate registrations on reload.
        /// </summary>
        public override void OnDisabled()
        {
            UnsubscribeEvents();

            (apiClient as IDisposable)?.Dispose();
            apiClient             = null;
            lifecycleHandlers     = null;
            moderationHandlers    = null;
            roleHandlers          = null;
            communicationHandlers = null;
            roundHandlers         = null;

            base.OnDisabled();
            Log.Info($"{Name} disabled.");
        }

        private void SubscribeEvents()
        {
            // Player lifecycle
            PlayerEvents.Joined += lifecycleHandlers.OnJoined;
            PlayerEvents.Left   += lifecycleHandlers.OnLeft;
            PlayerEvents.Dying  += lifecycleHandlers.OnDying;

            // Moderation
            PlayerEvents.Kicking          += moderationHandlers.OnKicking;
            PlayerEvents.Kicked           += moderationHandlers.OnKicked;
            PlayerEvents.Banning          += moderationHandlers.OnBanning;
            PlayerEvents.Banned           += moderationHandlers.OnBanned;
            PlayerEvents.IssuingMute      += moderationHandlers.OnIssuingMute;
            PlayerEvents.RevokingMute     += moderationHandlers.OnRevokingMute;
            PlayerEvents.Handcuffing      += moderationHandlers.OnHandcuffing;
            PlayerEvents.RemovingHandcuffs += moderationHandlers.OnRemovingHandcuffs;

            // Role / identity
            PlayerEvents.ChangingRole      += roleHandlers.OnChangingRole;
            PlayerEvents.Escaping          += roleHandlers.OnEscaping;
            PlayerEvents.ChangingNickname  += roleHandlers.OnChangingNickname;
            PlayerEvents.TogglingOverwatch += roleHandlers.OnTogglingOverwatch;
            PlayerEvents.PreAuthenticating += roleHandlers.OnPreAuthenticating;

            // Communication
            PlayerEvents.IntercomSpeaking        += communicationHandlers.OnIntercomSpeaking;
            PlayerEvents.VoiceChatting           += communicationHandlers.OnVoiceChatting;
            PlayerEvents.SendingAdminChatMessage += communicationHandlers.OnSendingAdminChatMessage;

            // Round
            ServerEvents.RoundStarted += roundHandlers.OnRoundStarted;
            ServerEvents.RoundEnded   += roundHandlers.OnRoundEnded;
        }

        private void UnsubscribeEvents()
        {
            PlayerEvents.Joined -= lifecycleHandlers.OnJoined;
            PlayerEvents.Left   -= lifecycleHandlers.OnLeft;
            PlayerEvents.Dying  -= lifecycleHandlers.OnDying;

            PlayerEvents.Kicking           -= moderationHandlers.OnKicking;
            PlayerEvents.Kicked            -= moderationHandlers.OnKicked;
            PlayerEvents.Banning           -= moderationHandlers.OnBanning;
            PlayerEvents.Banned            -= moderationHandlers.OnBanned;
            PlayerEvents.IssuingMute       -= moderationHandlers.OnIssuingMute;
            PlayerEvents.RevokingMute      -= moderationHandlers.OnRevokingMute;
            PlayerEvents.Handcuffing       -= moderationHandlers.OnHandcuffing;
            PlayerEvents.RemovingHandcuffs -= moderationHandlers.OnRemovingHandcuffs;

            PlayerEvents.ChangingRole      -= roleHandlers.OnChangingRole;
            PlayerEvents.Escaping          -= roleHandlers.OnEscaping;
            PlayerEvents.ChangingNickname  -= roleHandlers.OnChangingNickname;
            PlayerEvents.TogglingOverwatch -= roleHandlers.OnTogglingOverwatch;
            PlayerEvents.PreAuthenticating -= roleHandlers.OnPreAuthenticating;

            PlayerEvents.IntercomSpeaking        -= communicationHandlers.OnIntercomSpeaking;
            PlayerEvents.VoiceChatting           -= communicationHandlers.OnVoiceChatting;
            PlayerEvents.SendingAdminChatMessage -= communicationHandlers.OnSendingAdminChatMessage;

            ServerEvents.RoundStarted -= roundHandlers.OnRoundStarted;
            ServerEvents.RoundEnded   -= roundHandlers.OnRoundEnded;
        }
    }
}
