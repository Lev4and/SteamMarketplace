using SteamMarketplace.Authorization.Models;
using SteamMarketplace.Model.Database.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace SteamMarketplace.HttpClients.Common.Services
{
    public class Authorization
    {
        private Login? _account;
        private ApplicationUser? _user;
        private JwtSecurityToken? _token;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public Authorization()
        {
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        private string GetClaimValue(string claimName)
        {
            if (string.IsNullOrEmpty(claimName))
            {
                throw new ArgumentNullException(nameof(claimName));
            }

            return _token != null ? _token.Claims.FirstOrDefault(claim => claim.Type == claimName)?.Value ?? "" : ""; 
        }

        public bool IsOverdue()
        {
            return _token == null || _token?.ValidTo.Subtract(DateTime.Now.ToUniversalTime()).Minutes < 20;
        }

        public bool LoggedIn()
        {
            return _account != null;
        }

        public string GetToken()
        {
            return _token?.RawData ?? "";
        }

        public Guid GetUserId()
        {
            return _token != null ? Guid.Parse(GetClaimValue("id")) : Guid.Empty;
        }

        public Guid GetCurrencyId()
        {
            return _token != null ? Guid.Parse(GetClaimValue("currencyId")) : Guid.Empty;
        }

        public Login GetAccount()
        {
            return _account;
        }

        public void SetAccessToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            _token = _tokenHandler.ReadJwtToken(accessToken);
        }

        public void Login(string emailOrLogin, string password)
        {
            if (string.IsNullOrEmpty(emailOrLogin))
            {
                throw new ArgumentNullException("emailOrLogin", "The email or login must not be empty.");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password", "The password must not be empty.");
            }

            _account = new Login(emailOrLogin, password);
        }

        public void LoginByAdministrator()
        {
            _account = new Login("Admin", "Admin");
        }

        public void Logout()
        {
            _account = null;
        }
    }
}
