using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SteamMarketplace.DesktopApplication.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToCollapsedVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToBoolean(value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Visibility)value == Visibility.Visible ? true : false;
        }
    }
}
