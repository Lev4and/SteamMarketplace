using SteamMarketplace.HttpClients.Common.Extensions;

namespace SteamMarketplace.HttpClients.Common.Services
{
    public static class HttpQueryBuilder
    {
        private static List<QueryParam> GetQueryParams(object queryParams)
        {
            if (queryParams == null)
            {
                throw new ArgumentNullException("queryParams", "Query params should not be empty.");
            }

            var queryParamCollection = new List<QueryParam>();

            foreach (var property in queryParams.GetType().GetProperties())
            {
                if (property.IsQueryParam())
                {
                    queryParamCollection.Add(new QueryParam(property.GetParamName(), property.GetParamValue(queryParams)));
                }
            }

            return queryParamCollection;
        }

        public static string Build(object queryParams)
        {
            if (queryParams == null)
            {
                throw new ArgumentNullException("queryParams", "Query params should not be empty.");
            }

            return $"{string.Join('&', GetQueryParams(queryParams).Select(param => $"{param.Key}={param.Value}"))}";
        }
    }
}
