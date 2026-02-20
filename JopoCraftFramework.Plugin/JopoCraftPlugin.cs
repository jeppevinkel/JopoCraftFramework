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
        private IApiClient _apiClient;

        private PlayerLifecycleEventHandlers    _lifecycleHandlers;
        private PlayerModerationEventHandlers   _moderationHandlers;
        private PlayerRoleEventHandlers         _roleHandlers;
        private PlayerCommunicationEventHandlers _communicationHandlers;
        private RoundEventHandlers              _roundHandlers;

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
            _apiClient = new HttpApiClient(Config);

            _lifecycleHandlers     = new PlayerLifecycleEventHandlers(_apiClient);
            _moderationHandlers    = new PlayerModerationEventHandlers(_apiClient);
            _roleHandlers          = new PlayerRoleEventHandlers(_apiClient);
            _communicationHandlers = new PlayerCommunicationEventHandlers(_apiClient);
            _roundHandlers         = new RoundEventHandlers(_apiClient);

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

            (_apiClient as IDisposable)?.Dispose();
            _apiClient             = null;
            _lifecycleHandlers     = null;
            _moderationHandlers    = null;
            _roleHandlers          = null;
            _communicationHandlers = null;
            _roundHandlers         = null;

            base.OnDisabled();
            Log.Info($"{Name} disabled.");
        }

        private void SubscribeEvents()
        {
            // Player lifecycle
            PlayerEvents.Joined += _lifecycleHandlers.OnJoined;
            PlayerEvents.Left   += _lifecycleHandlers.OnLeft;
            PlayerEvents.Dying  += _lifecycleHandlers.OnDying;

            // Moderation
            PlayerEvents.Kicking          += _moderationHandlers.OnKicking;
            PlayerEvents.Kicked           += _moderationHandlers.OnKicked;
            PlayerEvents.Banning          += _moderationHandlers.OnBanning;
            PlayerEvents.Banned           += _moderationHandlers.OnBanned;
            PlayerEvents.IssuingMute      += _moderationHandlers.OnIssuingMute;
            PlayerEvents.RevokingMute     += _moderationHandlers.OnRevokingMute;
            PlayerEvents.Handcuffing      += _moderationHandlers.OnHandcuffing;
            PlayerEvents.RemovingHandcuffs += _moderationHandlers.OnRemovingHandcuffs;

            // Role / identity
            PlayerEvents.ChangingRole      += _roleHandlers.OnChangingRole;
            PlayerEvents.Escaping          += _roleHandlers.OnEscaping;
            PlayerEvents.ChangingNickname  += _roleHandlers.OnChangingNickname;
            PlayerEvents.TogglingOverwatch += _roleHandlers.OnTogglingOverwatch;
            PlayerEvents.PreAuthenticating += _roleHandlers.OnPreAuthenticating;

            // Communication
            PlayerEvents.IntercomSpeaking        += _communicationHandlers.OnIntercomSpeaking;
            PlayerEvents.VoiceChatting           += _communicationHandlers.OnVoiceChatting;
            PlayerEvents.SendingAdminChatMessage += _communicationHandlers.OnSendingAdminChatMessage;

            // Round
            ServerEvents.RoundStarted += _roundHandlers.OnRoundStarted;
            ServerEvents.RoundEnded   += _roundHandlers.OnRoundEnded;
        }

        private void UnsubscribeEvents()
        {
            PlayerEvents.Joined -= _lifecycleHandlers.OnJoined;
            PlayerEvents.Left   -= _lifecycleHandlers.OnLeft;
            PlayerEvents.Dying  -= _lifecycleHandlers.OnDying;

            PlayerEvents.Kicking           -= _moderationHandlers.OnKicking;
            PlayerEvents.Kicked            -= _moderationHandlers.OnKicked;
            PlayerEvents.Banning           -= _moderationHandlers.OnBanning;
            PlayerEvents.Banned            -= _moderationHandlers.OnBanned;
            PlayerEvents.IssuingMute       -= _moderationHandlers.OnIssuingMute;
            PlayerEvents.RevokingMute      -= _moderationHandlers.OnRevokingMute;
            PlayerEvents.Handcuffing       -= _moderationHandlers.OnHandcuffing;
            PlayerEvents.RemovingHandcuffs -= _moderationHandlers.OnRemovingHandcuffs;

            PlayerEvents.ChangingRole      -= _roleHandlers.OnChangingRole;
            PlayerEvents.Escaping          -= _roleHandlers.OnEscaping;
            PlayerEvents.ChangingNickname  -= _roleHandlers.OnChangingNickname;
            PlayerEvents.TogglingOverwatch -= _roleHandlers.OnTogglingOverwatch;
            PlayerEvents.PreAuthenticating -= _roleHandlers.OnPreAuthenticating;

            PlayerEvents.IntercomSpeaking        -= _communicationHandlers.OnIntercomSpeaking;
            PlayerEvents.VoiceChatting           -= _communicationHandlers.OnVoiceChatting;
            PlayerEvents.SendingAdminChatMessage -= _communicationHandlers.OnSendingAdminChatMessage;

            ServerEvents.RoundStarted -= _roundHandlers.OnRoundStarted;
            ServerEvents.RoundEnded   -= _roundHandlers.OnRoundEnded;
        }
    }
}
