using System.Windows;

namespace Udemy_Calculator
{
    public class SimpleMath
    {
        public static decimal Add(decimal p1, decimal p2)
        {
            return p1 + p2;
        }

        public static decimal Substract(decimal p1, decimal p2)
        {
            return p1 - p2;
        }

        public static decimal Multiply(decimal p1, decimal p2)
        {
            return p1 * p2;
        }

        public static decimal Divide(decimal p1, decimal p2)
        { 
            if (p2 == 0)
            {
                MessageBox.Show("Invalid operation", "Error !!!", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }

            return p1 / p2;
        }
    }
}
