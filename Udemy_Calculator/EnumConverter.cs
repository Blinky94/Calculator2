using System;
using System.Windows.Data;

namespace Udemy_Calculator
{
    public class EnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Enum enumValue = default;
            if (parameter is Type type)
            {
                enumValue = (Enum)Enum.Parse(type, value.ToString());
            }
            return enumValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int returnValue = 0;
            if (parameter is Type type)
            {
                returnValue = (int)Enum.Parse(type, value.ToString());
            }
            return returnValue;
        }
    }
}
