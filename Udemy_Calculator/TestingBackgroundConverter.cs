using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Udemy_Calculator
{
    public class TestingBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush lSolidBrush)
            {
                var r = (int)lSolidBrush.Color.R;
                var g = (int)lSolidBrush.Color.G;
                var b = (int)lSolidBrush.Color.B;

                //dark < 382 < light
                return r + g + b < 382;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
