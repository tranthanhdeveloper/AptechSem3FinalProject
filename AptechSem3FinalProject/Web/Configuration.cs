using System.Collections.Generic;
using PayPal.Api;

namespace Web
{
    public static class PaypalConfiguration
    {
        public static readonly string ClientId;
        public static readonly string ClientSecret;

        static PaypalConfiguration()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }

        public static Dictionary<string, string> GetConfig()
        {
            return ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            var accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }

        public static APIContext GetAPIContext()
        {
            var apiContext = new APIContext(GetAccessToken()) {Config = GetConfig()};
            return apiContext;
        }
    }
}