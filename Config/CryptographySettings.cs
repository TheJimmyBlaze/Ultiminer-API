using System.Text;

namespace Config {

    public class CryptographySettings {

        public const string CRYPTO_SECTION = "Cryptography";

        //Defaults
        public const string DEFAULT_TOKEN_SECRET = "InsecureDefaultSecret";
        public const int DEFAULT_MINS_TO_LIVE = 1080;

        //Properties
        public string TokenSecretString {get; set;} = DEFAULT_TOKEN_SECRET;
        public int TokenMinsToLive { get; set; } = DEFAULT_MINS_TO_LIVE;

        //Converters
        public byte[] GetSecret() {
            return Encoding.ASCII.GetBytes(TokenSecretString);
        }
    }
}