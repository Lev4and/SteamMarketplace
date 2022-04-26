namespace SteamMarketplace.HttpClients.Common
{
    public class QueryParam
    {
        public string Key { get; }

        public string Value { get; }

        public QueryParam(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "Key should not be empty.");
            }

            if (value == null)
            {
                throw new ArgumentNullException("value", "Value should not be empty.");
            }

            Key = key;
            Value = value;
        }
    }
}
