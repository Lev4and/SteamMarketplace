using SteamMarketplace.HttpClients.AuthorizationAPI;
using SteamMarketplace.HttpClients.CBR;
using SteamMarketplace.HttpClients.CSMoney;
using SteamMarketplace.HttpClients.ResourceAPI;

namespace SteamMarketplace.HttpClients
{
    public class HttpContext
    {
        public AuthorizationAPIHttpContext AuthorizationAPI { get; }

        public CBRHttpContext CBR { get; }

        public CSMoneyHttpContext CSMoney { get; }

        public ResourceAPIHttpContext ResourceAPI { get; }

        public HttpContext(AuthorizationAPIHttpContext authorizationAPI, CBRHttpContext cBR, CSMoneyHttpContext cSMoney,
            ResourceAPIHttpContext resourceAPI)
        {
            AuthorizationAPI = authorizationAPI;
            CBR = cBR;
            CSMoney = cSMoney;
            ResourceAPI = resourceAPI;
        }
    }
}
