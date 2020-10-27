using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Udemy_Calculator
{
    public class FontFamilyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string @string && !string.IsNullOrEmpty(value as string) ? Application.Current.Resources[@string] : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
