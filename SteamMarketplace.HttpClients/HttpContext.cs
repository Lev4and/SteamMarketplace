using SteamMarketplace.HttpClients.AuthorizationAPI;
using SteamMarketplace.HttpClients.CSMoney;
using SteamMarketplace.HttpClients.ResourceAPI;

namespace SteamMarketplace.HttpClients
{
    public class HttpContext
    {
        public AuthorizationAPIHttpContext AuthorizationAPI { get; }

        public CSMoneyHttpContext CSMoney { get; }

        public ResourceAPIHttpContext ResourceAPI { get; }

        public HttpContext(AuthorizationAPIHttpContext authorizationAPI, CSMoneyHttpContext cSMoney,
            ResourceAPIHttpContext resourceAPI)
        {
            AuthorizationAPI = authorizationAPI;
            CSMoney = cSMoney;
            ResourceAPI = resourceAPI;
        }
    }
}
