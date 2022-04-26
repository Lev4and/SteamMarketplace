using SteamMarketplace.HttpClients.Common.Attributes;
using SteamMarketplace.HttpClients.Common.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SteamMarketplace.HttpClients.Common.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static bool IsQueryParam(this PropertyInfo property)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property", "The property should not be empty.");
            }

            return property.GetCustomAttribute<QueryParamAttribute>() != null;
        }

        public static bool IsQueryParamRequired(this PropertyInfo property)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property", "The property should not be empty.");
            }

            return property.GetCustomAttribute<RequiredAttribute>() != null;
        }

        public static string GetParamName(this PropertyInfo property)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property", "The property should not be empty.");
            }

            var attribute = property.GetCustomAttribute<ParamNameAttribute>();

            if (attribute != null)
            {
                return attribute.Name;
            }

            throw new AttributeNotFoundException(property.Name, typeof(ParamNameAttribute).Name);
        }

        public static string GetParamValue(this PropertyInfo property, object obj)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property", "The property should not be empty.");
            }

            var value = property.GetValue(obj);

            if (value == null)
            {
                if (property.IsQueryParamRequired())
                {
                    throw new ArgumentNullException("value", $"The value property {property.Name} should not be empty.");
                }

                return "";
            }

            return $"{value}";
        }
    }
}
