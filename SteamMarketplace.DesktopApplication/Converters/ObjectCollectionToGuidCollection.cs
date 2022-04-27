using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SteamMarketplace.DesktopApplication.Converters
{
    [ValueConversion(typeof(List<object>), typeof(List<Guid>))]
    public class ObjectCollectionToGuidCollection : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? value : new List<object>();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? ((List<object>)(value)).Select(v => Guid.Parse(System.Convert.ToString(v))).ToList() : new List<Guid>();
        }
    }
}
