using SteamMarketplace.HttpClients.Common.Attributes;

namespace SteamMarketplace.HttpClients.Common.Extensions
{
    public static class EnumeratorExtensions
    {
        public static string GetValue(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value", "The value should not be empty.");
            }

            var attributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(false);

            if (attributes != null)
            {
                var attribute = attributes.FirstOrDefault(attribute => attribute.GetType() == typeof(DisplayValueAttribute));

                if (attribute != null)
                {
                    return ((DisplayValueAttribute)attribute).Value;
                }
            }

            return $"{value}";
        }
    }
}
