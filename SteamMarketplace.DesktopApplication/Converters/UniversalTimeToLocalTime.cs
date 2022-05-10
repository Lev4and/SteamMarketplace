using System;
using System.Globalization;
using System.Windows.Data;

namespace SteamMarketplace.DesktopApplication.Converters
{
    [ValueConversion(typeof(DateTime?), typeof(DateTime?))]
    public class UniversalTimeToLocalTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? System.Convert.ToDateTime(value).ToLocalTime() : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? System.Convert.ToDateTime(value).ToUniversalTime() : null;
        }
    }
}
