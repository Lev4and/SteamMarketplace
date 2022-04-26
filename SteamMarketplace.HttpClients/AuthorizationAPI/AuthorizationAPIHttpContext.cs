namespace SteamMarketplace.HttpClients.AuthorizationAPI
{
    public class AuthorizationAPIHttpContext
    {
        public AuthorizationHttpClient Authorization { get; }

        public AuthorizationAPIHttpContext(AuthorizationHttpClient authorization)
        {
            Authorization = authorization;
        }
    }
}
