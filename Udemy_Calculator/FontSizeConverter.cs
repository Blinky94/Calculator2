using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Udemy_Calculator
{
    public class FontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && !string.IsNullOrEmpty(value as string))
            {
                int lValue = int.Parse(value as string);
                var lValStr = ((SizeOfFont)lValue).ToString();

                if (Enum.GetNames(typeof(SizeOfFont)).ToList().Contains(lValStr))
                {
                    return value as string;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
