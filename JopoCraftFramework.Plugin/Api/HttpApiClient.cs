using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
        private static readonly HttpClient HttpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30),
        };

        public HttpApiClient(Config config)
        {
            _config = config;
            HttpClient.DefaultRequestHeaders.Add("User-Agent", "JopoCraftFramework/1.0");
            HttpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrEmpty(config.ApiKey))
                HttpClient.DefaultRequestHeaders.Add("X-Api-Key", config.ApiKey);
        }

        /// <inheritdoc/>
        public void SendEvent(object dto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(dto);
                if (_config.Debug)
                    Log.Debug($"[ApiClient] POST {json}");
                
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                HttpClient.PostAsync(_config.EventEndpointUrl, content);
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
                
                return HttpClient.GetStringAsync(url).Result;
            }
            catch (Exception ex)
            {
                Log.Error($"[ApiClient] GET {relativeUrl} failed: {ex.Message}");
                return null;
            }
        }

        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }
}
