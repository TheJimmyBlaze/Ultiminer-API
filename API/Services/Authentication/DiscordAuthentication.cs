
using Config;
using Models.Authentication;
using System.Text.Json;
using System.Net.Http.Headers;

namespace Services.Authentication {

    public class DiscordAuthentication {

        private readonly ILogger logger;

        private readonly DiscordSettings settings;
        private readonly IHttpClientFactory clientFactory;

        private const string AUTHENTICATION_HEADER_TYPE = "Bearer";
        private const string EXCHANGE_GRANT_TYPE = "authorization_code";
        private const string OAUTH_ROUTE = "/oauth2/token";
        private const string ME_ROUTE = "/users/@me";

        public DiscordAuthentication(ILogger<DiscordAuthentication> logger,
            UltiminerSettings settings, 
            IHttpClientFactory clientFactory) {

            this.logger = logger;

            this.settings = settings.Discord;
            this.clientFactory = clientFactory;
        }

        public async Task<DiscordToken> ExchangeAuthCode(string authCode) {

            logger.LogTrace("Attempting to exchange Discord auth code for token...");

            //Get a new HttpClient
            HttpClient client = clientFactory.CreateClient();

            //Create post body as form data
            Dictionary<string, string> request = new(){
                {"client_id", settings.ClientId},
                {"client_secret", settings.ClientSecret},
                {"grant_type", EXCHANGE_GRANT_TYPE},
                {"code", authCode},
                {"redirect_uri", settings.RedirectURL}
            };
            FormUrlEncodedContent content = new(request);

            //Submit request
            string requestURL = $"{settings.APIEndpoint}{OAUTH_ROUTE}";
            HttpResponseMessage response = await client.PostAsync(requestURL, content);

            //Handle response failure
            if (!response.IsSuccessStatusCode) {

                logger.LogDebug("Discord token exchange error: Bad response status code: {statusCode}", response.StatusCode);
                throw new Exception($"Discord token exchange was not successful: {response.ReasonPhrase}");
            }

            //Deserialize response into token
            string responseContent = await response.Content.ReadAsStringAsync();
            DiscordToken? token = JsonSerializer.Deserialize<DiscordToken>(responseContent);
            if (token == null) {

                logger.LogDebug("Discord token deserialization not successful");
                throw new Exception("Failed to deserialize JSON response received from discord token exchange");
            }

            logger.LogTrace("Token exchange completed");
            return token;
        }

        public async Task<DiscordIdentity> GetIdentityFromToken(DiscordToken token) {

            logger.LogTrace("Collecting Discord identity from token...");

            //Get a new HttpClient
            HttpClient client = clientFactory.CreateClient();

            //Add bearer auth
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AUTHENTICATION_HEADER_TYPE, token.AccessToken);

            //Get @me from discord
            string requestURL = $"{settings.APIEndpoint}{ME_ROUTE}";
            HttpResponseMessage response = await client.GetAsync(requestURL);

            //Handle response failure
            if (!response.IsSuccessStatusCode) {

                logger.LogDebug("Discord identity error: Bad response status code: {statusCode}", response.StatusCode);
                throw new Exception($"Discord user identity request was not successful: {response.ReasonPhrase}");
            }

            //Deserialize response into identity
            string responseContent = await response.Content.ReadAsStringAsync();
            DiscordIdentity? identity = JsonSerializer.Deserialize<DiscordIdentity>(responseContent);
            if (identity == null) {

                logger.LogDebug("Discord identity deserialization not successful");
                throw new Exception("Failed to deserialize JSON response received from discord user identity request");
            }

            logger.LogTrace("Discord identity collected");
            return identity;
        }
    }
}