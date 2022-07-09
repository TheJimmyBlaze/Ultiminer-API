using Config;

namespace Services.Token {

    public class DiscordToken {

        private readonly ILogger logger;
        private readonly DiscordSettings settings;
        private readonly IHttpClientFactory clientFactory;

        private const string EXCHANGE_GRANT_TYPE = "authorization_code";
        private const string OAUTH_ROUTE = "/oauth2/token";

        public DiscordToken(ILogger<DiscordToken> logger, UltiminerSettings settings, IHttpClientFactory clientFactory) {
            this.logger = logger;
            this.settings = settings.Discord;
            this.clientFactory = clientFactory;
        }

        public async Task<string> ExchangeAuthCode(string authCode) {

            //Get a new HttpClient
            HttpClient client = clientFactory.CreateClient();

            //Create post body as form data
            Dictionary<string, string> request = new (){
                {"client_id", settings.ClientId},
                {"client_secret", settings.ClientSecret},
                {"grant_type", EXCHANGE_GRANT_TYPE},
                {"code", authCode},
                {"redirect_uri", settings.RedirectURL}
            };
            FormUrlEncodedContent content = new (request);

            logger.LogInformation("Request: {request}", request);

            //Submit request
            string requestURL = $"{settings.APIEndpoint}{OAUTH_ROUTE}";
            HttpResponseMessage response = await client.PostAsync(requestURL, content);

            string responseContent = await response.Content.ReadAsStringAsync();
            logger.LogInformation("Response: {responseContent}", responseContent); 

            return string.Empty;
        }
    }
}