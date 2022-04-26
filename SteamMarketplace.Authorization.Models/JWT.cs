namespace SteamMarketplace.Authorization.Models
{
    public class JWT
    {
        public string AccessToken { get; set; }

        public JWT()
        {
            AccessToken = "";
        }

        public JWT(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken), "The accessToken must not be empty.");
            }

            AccessToken = accessToken;
        }
    }
}
