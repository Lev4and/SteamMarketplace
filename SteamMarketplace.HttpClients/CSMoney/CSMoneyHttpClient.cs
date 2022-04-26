using SteamMarketplace.HttpClients.Common;

namespace SteamMarketplace.HttpClients.CSMoney
{
    public class CSMoneyHttpClient : BaseHttpClient
    {
        public CSMoneyHttpClient(string domain, string path) : base($"https://{domain}/{path}")
        {
            if (domain == null)
            {
                throw new ArgumentNullException("path", "The domain should not be empty.");
            }

            if (path == null)
            {
                throw new ArgumentNullException("path", "The path should not be empty.");
            }
        }

        public CSMoneyHttpClient(string path) : base($"https://{CSMoneyRoutes.Domain}/{path}")
        {
            if (path == null)
            {
                throw new ArgumentNullException("path", "The path should not be empty.");
            }
        }

        protected override void UseCookie()
        {
            base.UseCookie();

            Client.DefaultRequestHeaders.Add("Cookie", GetCookiesString($"https://{CSMoneyRoutes.Domain}/"));
        }
    }
}
