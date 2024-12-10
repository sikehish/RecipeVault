using Avalonia.Data.Converters;
using System;
using Avalonia.Controls;
using System.Globalization;

namespace RecipeManager.Converters
{
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && !string.IsNullOrEmpty(str))
            {
                return true;  // Visible
            }
            return false;  // Hidden
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Handle the reverse conversion (if needed)
            return null;
        }
    }
}
