using System;
using System.Globalization;
using Microsoft.Maui.Controls;

// Converts null values to "None" for display purposes in MAUI.
namespace Asana.Maui.Views
{

    public class NullToNoneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value == null ? "None" : value.ToString();

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value?.ToString() == "None" ? null : value;
    }
}