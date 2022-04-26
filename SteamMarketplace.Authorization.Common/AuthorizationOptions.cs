using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SteamMarketplace.Authorization.Common
{
    public class AuthorizationOptions
    {
        public const int TokenLifetime = 3600;

        public const string Secret = "SecretKey123456789+-";

        public const string Issuer = "AuthorizationServer";

        public const string Audience = "ResourceServer";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}
