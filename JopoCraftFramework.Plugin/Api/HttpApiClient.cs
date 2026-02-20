using System;
using System.Net;
using System.Web.Script.Serialization;
using Exiled.API.Features;

namespace JopoCraftFramework.Plugin.Api
{
    /// <summary>
    /// HTTP implementation of <see cref="IApiClient"/> backed by
    /// <see cref="WebClient"/> and <see cref="JavaScriptSerializer"/>.
    /// </summary>
    public class HttpApiClient : IApiClient, IDisposable
    {
        private readonly Config config;
        private readonly WebClient http;
        private readonly JavaScriptSerializer serializer;

        public HttpApiClient(Config config)
        {
            this.config = config;
            serializer  = new JavaScriptSerializer();
            http        = new WebClient();
            http.Headers[HttpRequestHeader.ContentType] = "application/json";
        }

        /// <inheritdoc/>
        public void SendEvent(object dto)
        {
            try
            {
                var json = serializer.Serialize(dto);
                if (config.Debug)
                    Log.Debug($"[ApiClient] POST {json}");

                http.UploadStringAsync(new Uri(config.EventEndpointUrl), "POST", json);
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
                var url = config.EventEndpointUrl.TrimEnd('/') + "/" + relativeUrl.TrimStart('/');
                if (config.Debug)
                    Log.Debug($"[ApiClient] GET {url}");

                return http.DownloadString(url);
            }
            catch (Exception ex)
            {
                Log.Error($"[ApiClient] GET {relativeUrl} failed: {ex.Message}");
                return null;
            }
        }

        public void Dispose()
        {
            http?.Dispose();
        }
    }
}
