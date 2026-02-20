using System;
using System.Net;
using Exiled.API.Features;
using Newtonsoft.Json;

namespace JopoCraftFramework.Plugin.Api
{
    /// <summary>
    /// HTTP implementation of <see cref="IApiClient"/> backed by
    /// <see cref="WebClient"/> and <see cref="JsonConvert"/>.
    /// </summary>
    public class HttpApiClient : IApiClient, IDisposable
    {
        private readonly Config _config;
        private readonly WebClient _http;

        public HttpApiClient(Config config)
        {
            this._config = config;
            _http = new WebClient();
            _http.Headers[HttpRequestHeader.ContentType] = "application/json";
            if (!string.IsNullOrEmpty(config.ApiKey))
                _http.Headers["X-Api-Key"] = config.ApiKey;
        }

        /// <inheritdoc/>
        public void SendEvent(object dto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(dto);
                if (_config.Debug)
                    Log.Debug($"[ApiClient] POST {json}");

                _http.UploadStringAsync(new Uri(_config.EventEndpointUrl), "POST", json);
            }
            catch (Exception ex)
            {
                Log.Error($"[ApiClient] SendEvent failed: {ex.Message}");
            }
        }

        /// <inheritdoc/>
        public string Get(string relativeUrl)
        {
            try
            {
                var url = _config.EventEndpointUrl.TrimEnd('/') + "/" + relativeUrl.TrimStart('/');
                if (_config.Debug)
                    Log.Debug($"[ApiClient] GET {url}");

                return _http.DownloadString(url);
            }
            catch (Exception ex)
            {
                Log.Error($"[ApiClient] GET {relativeUrl} failed: {ex.Message}");
                return null;
            }
        }

        public void Dispose()
        {
            _http?.Dispose();
        }
    }
}
