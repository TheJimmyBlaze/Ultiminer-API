
namespace Config {

    public class UltiminerSettings {

        public const string ULTIMINER_SECTION = "Ultiminer";

        public CryptographySettings Cryptography {get; set;}

        public DiscordSettings Discord {get; set;}
    }
}