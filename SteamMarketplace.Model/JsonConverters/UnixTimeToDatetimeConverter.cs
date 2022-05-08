using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SteamMarketplace.Model.JsonConverters
{
    public class UnixTimeToDatetimeConverter : DateTimeConverterBase
    {
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue((Convert.ToDateTime(value).ToUniversalTime() - _epoch).TotalSeconds);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }

            return _epoch.AddTicks(long.Parse(reader.Value.ToString()) * 10000000).ToUniversalTime();
        }
    }
}
