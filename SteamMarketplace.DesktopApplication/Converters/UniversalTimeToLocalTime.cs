using System;
using System.Globalization;
using System.Windows.Data;

namespace SteamMarketplace.DesktopApplication.Converters
{
    [ValueConversion(typeof(DateTime), typeof(DateTime))]
    public class UniversalTimeToLocalTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDateTime(value).ToLocalTime();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDateTime(value).ToUniversalTime();
        }
    }
}
