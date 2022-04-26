using Newtonsoft.Json;

namespace SteamMarketplace.Model.Json
{
    public class JsonObject
    {
        private IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
