using Newtonsoft.Json;
using SteamMarketplace.Authorization.Models;
using SteamMarketplace.HttpClients.Common.Extensions;
using SteamMarketplace.Model.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;

namespace SteamMarketplace.HttpClients.Common
{
    public class AuthorizationHttpClient : BaseHttpClient
    {
        private Login? _account;
        private JwtSecurityToken? _token;
        private readonly string _authorizationUri;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public AuthorizationHttpClient(string uri, string authorizationUri) : base(uri)
        {
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentNullException("uri", "The server uri must not be empty.");
            }

            if (string.IsNullOrEmpty(authorizationUri))
            {
                throw new ArgumentNullException("authenticationUri", "The authorization uri must not be empty.");
            }

            _authorizationUri = authorizationUri;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        private bool CheckValidToken()
        {
            return _token != null || _token?.ValidTo.Subtract(DateTime.Now.ToUniversalTime()).Minutes > 20;
        }

        private async Task<string> GetAccessToken()
        {
            if (_account == null)
            {
                throw new ArgumentNullException("_account", "The account must not be empty.");
            }

            var respponse = await (await Client.PostAsync(_authorizationUri,
                new StringContent(JsonConvert.SerializeObject(_account),
                    Encoding.UTF8, "application/json"))).GetJsonResultAsync<BaseResponseModel<JWT>>();

            if (respponse.Status.Code == System.Net.HttpStatusCode.OK)
            {
                return respponse.Result.AccessToken;
            }

            return "";
        }

        protected internal async Task AuthorizeAsync()
        {
            if (!CheckValidToken())
            {
                var accessToken = await GetAccessToken();

                if (!string.IsNullOrEmpty(accessToken)) _token = _tokenHandler.ReadJwtToken(accessToken);
            }

            if (_token != null) Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.RawData);
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

        public void Logout()
        {
            _account = null;
        }
    }
}
