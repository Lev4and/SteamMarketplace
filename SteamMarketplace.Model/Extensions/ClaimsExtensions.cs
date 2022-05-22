using System.Security.Claims;

namespace SteamMarketplace.Model.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetValue(this IEnumerable<Claim> claims, string name)
        {
            if (claims == null)
            {
                throw new ArgumentNullException(nameof(claims));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return claims.First((claim) => claim.Type == name).Value ?? "";
        }
    }
}
