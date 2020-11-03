using System;
using System.Globalization;
using System.Windows.Data;

namespace Udemy_Calculator
{
    public class FontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && !string.IsNullOrEmpty(value as string))
            {
                if (CommonSkins.GetFontWeightList.Contains(value as string))
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
