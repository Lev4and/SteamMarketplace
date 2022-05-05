﻿using SteamMarketplace.HttpClients.Common.Extensions;
using SteamMarketplace.HttpClients.CSMoney.ResponseModels;
using SteamMarketplace.Model.Common;
using Services = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class CSMoneyStoreHttpClient : ResourceAPIHttpClient
    {
        public CSMoneyStoreHttpClient(Services.Authorization authorization)
            : base(ResourceAPIRoutes.CSMoneyStorePath, authorization)
        {

        }

        public async Task<BaseResponseModel<BotInventory>> GetInventoryAsync(int limit, int offset, bool withStack = true)
        {
            if (limit <= 0)
            {
                throw new ArgumentOutOfRangeException("limit", "The limit must not be less than or equal to zero.");
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", "The offset must not be less than zero.");
            }

            await AuthorizeAsync();

            return await GetAsync<BaseResponseModel<BotInventory>>($"{ResourceAPIRoutes.CSMoneyStoreQuery}?limit={limit}" +
                $"&offset={offset}&withStack={withStack.ToString().ToLower()}");
        }
    }
}
