namespace SteamMarketplace.Model.Database.Extensions
{
    public static class DbValue
    {
        public static object GetDbValue(this bool? value)
        {
            return value != null ? value : DBNull.Value;
        }

        public static object GetDbValue(this int? value)
        {
            return value != null ? value : DBNull.Value;
        }

        public static object GetDbValue(this float? value)
        {
            return value != null ? value : DBNull.Value;
        }

        public static object GetDbValue(this double? value)
        {
            return value != null ? value : DBNull.Value;
        }

        public static object GetDbValue(this decimal? value)
        {
            return value != null ? value : DBNull.Value;
        }

        public static object GetDbValue(this string value)
        {
            return !string.IsNullOrEmpty(value) ? value : DBNull.Value;
        }

        public static object GetDbValue(this Guid? value)
        {
            return value != null ? value : DBNull.Value;
        }

        public static object GetDbValue(this DateTime? value)
        {
            return value != null ? value : DBNull.Value;
        }

        public static object GetDbValue(this DateTimeOffset? value)
        {
            return value != null ? value : DBNull.Value;
        }
    }
}
