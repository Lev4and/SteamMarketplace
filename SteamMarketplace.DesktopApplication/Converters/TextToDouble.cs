using System;
using System.Globalization;
using System.Windows.Data;

namespace SteamMarketplace.DesktopApplication.Converters
{
    [ValueConversion(typeof(string), typeof(double))]
    public class TextToDouble : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (!string.IsNullOrEmpty(System.Convert.ToString(value)) ? double.Parse(System.Convert.ToString(value)) : 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value != null ? System.Convert.ToString(value) : "");
        }
    }
}
