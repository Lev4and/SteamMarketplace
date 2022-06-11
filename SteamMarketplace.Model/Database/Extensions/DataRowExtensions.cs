using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace SteamMarketplace.Model.Database.Extensions
{
    public static class DataRowExtensions
    {
        public static T ToObject<T>(this DataRow dataRow) where T : new()
        {
            var item = new T();

            foreach (DataColumn column in dataRow.Table.Columns)
            {
                var property = GetProperty(typeof(T), column.ColumnName);

                if (property != null && dataRow[column] != DBNull.Value && dataRow[column].ToString() != "NULL")
                {
                    property.SetValue(item, ChangeType(dataRow[column], property.PropertyType), null);
                }
            }

            return item;
        }

        private static PropertyInfo GetProperty(Type type, string attributeName)
        {
            var property = type.GetProperty(attributeName);

            if (property != null)
            {
                return property;
            }

            return type.GetProperties().Where(property => property.IsDefined(typeof(DisplayAttribute), false) &&
                property.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().Single().Name == 
                    attributeName).FirstOrDefault();
        }

        public static object ChangeType(object value, Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                return Convert.ChangeType(value, Nullable.GetUnderlyingType(type));
            }

            return Convert.ChangeType(value, type);
        }
    }
}
