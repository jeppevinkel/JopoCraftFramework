namespace JopoCraftFramework.Plugin.Api
{
    /// <summary>
    /// Abstraction over HTTP communication with the JopoCraft API.
    /// Supports both sending data (event dispatch) and retrieving data
    /// (e.g. moderator lists, player stats) so the implementation can
    /// be extended without changing call sites.
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// Serialises <paramref name="dto"/> to JSON and sends it to the
        /// configured event endpoint asynchronously.
        /// Implementations must swallow all exceptions and log them instead
        /// of propagating — a dispatch failure must never crash the server.
        /// </summary>
        void SendEvent(object dto);

        /// <summary>
        /// Sends a GET request to <paramref name="relativeUrl"/> and returns
        /// the raw JSON response body, or <c>null</c> on failure.
        /// Intended for future use (e.g. fetching moderator lists, player stats).
        /// </summary>
        string Get(string relativeUrl);
    }
}
