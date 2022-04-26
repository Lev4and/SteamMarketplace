using Newtonsoft.Json;

namespace SteamMarketplace.Model.JsonConverters
{
    public class ToDictionaryJsonConverter<K, V> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var result = new Dictionary<K, V>();
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Convert.ToString(reader.Value));

            foreach (var key in dictionary.Keys)
            {
                result.Add(JsonConvert.DeserializeObject<K>(Convert.ToString(key)),
                    JsonConvert.DeserializeObject<V>(Convert.ToString(dictionary[key])));
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
