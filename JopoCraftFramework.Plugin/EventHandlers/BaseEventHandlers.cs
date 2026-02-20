using JopoCraftFramework.Contracts.Models;
using JopoCraftFramework.Plugin.Api;
using ExiledPlayer = Exiled.API.Features.Player;

namespace JopoCraftFramework.Plugin.EventHandlers
{
    /// <summary>
    /// Shared base for all event handler classes.
    /// Provides access to <see cref="IApiClient"/> and the common
    /// <see cref="ToDto"/> player-mapping helper.
    /// </summary>
    public abstract class BaseEventHandlers
    {
        protected readonly IApiClient ApiClient;

        protected BaseEventHandlers(IApiClient apiClient)
        {
            ApiClient = apiClient;
        }

        /// <summary>
        /// Maps an EXILED <see cref="ExiledPlayer"/> to a serialisable <see cref="PlayerDto"/>.
        /// Returns <c>null</c> when <paramref name="p"/> is <c>null</c>.
        /// </summary>
        protected static PlayerDto ToDto(ExiledPlayer p)
        {
            if (p == null) return null;
            return new PlayerDto
            {
                UserId    = p.UserId,
                Nickname  = p.Nickname,
                Role      = p.Role?.Type.ToString(),
                IpAddress = p.IPAddress
            };
        }
    }
}
