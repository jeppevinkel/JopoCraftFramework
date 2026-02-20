using System.ComponentModel;
using Exiled.API.Interfaces;

namespace JopoCraftFramework.Plugin
{
    public class Config : IConfig
    {
        [Description("Whether the plugin is enabled.")]
        public bool IsEnabled { get; set; } = false;

        [Description("Whether debug logging is enabled.")]
        public bool Debug { get; set; } = false;

        [Description("The endpoint URL to POST event DTOs to.")]
        public string EventEndpointUrl { get; set; } = "http://localhost:5000/api/events";

        [Description("The API key sent in the X-Api-Key header for every request.")]
        public string ApiKey { get; set; } = "";
    }
}
