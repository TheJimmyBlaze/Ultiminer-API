using System.Diagnostics;

namespace Config {

    public class DiscordSettings {

        public const string DISCORD_SECTION = "Discord";

        public string APIEndpoint {get; set;}
        public string ClientId {get; set;}
        public string ClientSecret {get; set;}
        public string RedirectURL {get; set;}
    }
}