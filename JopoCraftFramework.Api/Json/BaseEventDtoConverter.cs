using System.Text.Json;
using System.Text.Json.Serialization;
using JopoCraftFramework.Contracts.Events;

namespace JopoCraftFramework.Api.Json;

public class BaseEventDtoConverter : JsonConverter<BaseEventDto>
{
    private static readonly Dictionary<string, Type> TypeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        [nameof(PlayerJoinedEventDto)] = typeof(PlayerJoinedEventDto),
        [nameof(PlayerLeftEventDto)] = typeof(PlayerLeftEventDto),
        [nameof(PlayerDiedEventDto)] = typeof(PlayerDiedEventDto),
        [nameof(PlayerBannedEventDto)] = typeof(PlayerBannedEventDto),
        [nameof(PlayerBanningEventDto)] = typeof(PlayerBanningEventDto),
        [nameof(PlayerChangingNicknameEventDto)] = typeof(PlayerChangingNicknameEventDto),
        [nameof(PlayerChangingRoleEventDto)] = typeof(PlayerChangingRoleEventDto),
        [nameof(PlayerEscapingEventDto)] = typeof(PlayerEscapingEventDto),
        [nameof(PlayerHandcuffingEventDto)] = typeof(PlayerHandcuffingEventDto),
        [nameof(PlayerIntercomSpeakingEventDto)] = typeof(PlayerIntercomSpeakingEventDto),
        [nameof(PlayerKickedEventDto)] = typeof(PlayerKickedEventDto),
        [nameof(PlayerKickingEventDto)] = typeof(PlayerKickingEventDto),
        [nameof(PlayerMuteIssuedEventDto)] = typeof(PlayerMuteIssuedEventDto),
        [nameof(PlayerMuteRevokedEventDto)] = typeof(PlayerMuteRevokedEventDto),
        [nameof(PlayerPreAuthenticatingEventDto)] = typeof(PlayerPreAuthenticatingEventDto),
        [nameof(PlayerRemovingHandcuffsEventDto)] = typeof(PlayerRemovingHandcuffsEventDto),
        [nameof(PlayerSendingAdminChatMessageEventDto)] = typeof(PlayerSendingAdminChatMessageEventDto),
        [nameof(PlayerTogglingOverwatchEventDto)] = typeof(PlayerTogglingOverwatchEventDto),
        [nameof(PlayerVoiceChattingEventDto)] = typeof(PlayerVoiceChattingEventDto),
        [nameof(RoundStartedEventDto)] = typeof(RoundStartedEventDto),
        [nameof(RoundEndedEventDto)] = typeof(RoundEndedEventDto),
    };

    public override BaseEventDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
        JsonElement root = doc.RootElement;

        if (!root.TryGetProperty("eventType", out JsonElement typeElement) &&
            !root.TryGetProperty("EventType", out typeElement))
        {
            throw new JsonException("Missing 'EventType' discriminator property.");
        }

        string eventType = typeElement.GetString()
            ?? throw new JsonException("'EventType' property is null.");

        if (!TypeMap.TryGetValue(eventType, out Type concreteType))
            throw new JsonException($"Unknown EventType '{eventType}'.");

        return (BaseEventDto)JsonSerializer.Deserialize(root.GetRawText(), concreteType, options)!;
    }

    public override void Write(Utf8JsonWriter writer, BaseEventDto value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, options);
    }
}
