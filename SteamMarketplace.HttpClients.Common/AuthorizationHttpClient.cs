using Newtonsoft.Json;
using SteamMarketplace.Authorization.Models;
using SteamMarketplace.HttpClients.Common.Extensions;
using SteamMarketplace.Model.Common;
using System.Net.Http.Headers;
using System.Text;

namespace SteamMarketplace.HttpClients.Common
{
    public class AuthorizationHttpClient : BaseHttpClient
    {
        private readonly string _authorizationUri;
        private readonly Services.Authorization _authorization;

        public AuthorizationHttpClient(string uri, string authorizationUri, Services.Authorization authorization)
            : base(uri)
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
            _authorization = authorization;
        }

        private async Task<string> GetAccessToken()
        {
            if (_authorization.LoggedIn())
            {
                var respponse = await (await Client.PostAsync(_authorizationUri,
                    new StringContent(JsonConvert.SerializeObject(_authorization.GetAccount()),
                        Encoding.UTF8, "application/json"))).GetJsonResultAsync<BaseResponseModel<JWT>>();

                if (respponse.Status.Code == System.Net.HttpStatusCode.OK)
                {
                    return respponse.Result.AccessToken;
                }
            }

            return "";
        }

        protected internal async Task AuthorizeAsync()
        {
            if (_authorization.IsOverdue())
            {
                _authorization.SetAccessToken(await GetAccessToken());
            }

            if (!string.IsNullOrEmpty(_authorization.GetToken())) Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authorization.GetToken());
        }
    }
}
