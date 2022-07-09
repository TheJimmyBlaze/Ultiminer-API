
namespace Config {

    public class UltiminerSettings {

        public const string ULTIMINER_SECTION = "Ultiminer";

        public CryptographySettings CryptographySettings {get; set;} = new CryptographySettings();
    }
}