using System.Text;

namespace Config {

    public class CryptographySettings {

        public const string CRYPTO_SECTION = "Cryptography";

        public string TokenSecret {get; set;}
        public int TokenMinsToLive { get; set; }

        public byte[] GetSecret() {
            return Encoding.ASCII.GetBytes(TokenSecret);
        }
    }
}