using System;
using System.Globalization;
using System.Windows.Data;

namespace Udemy_Calculator
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string timeString = value.ToString();

            DateTime dateVal = default;

            if (timeString.Length == 16)
            {
                dateVal = DateTime.ParseExact(timeString, "yyyyMMddHHmmss.f", culture);
            }
            else if (timeString.Length == 17)
            {
                dateVal = DateTime.ParseExact(timeString, "yyyyMMddHHmmss.ff", culture);
            }
            else if (timeString.Length == 18)
            {
                dateVal = DateTime.ParseExact(timeString, "yyyyMMddHHmmss.fff", culture);
            }

            return dateVal.ToString("dd/MM/yyyy HH:mm:ss.fff");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
