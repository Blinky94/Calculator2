using System.Windows;

namespace Udemy_Calculator
{
    public class SimpleMath
    {
        public static double Add(double p1, double p2)
        {
            return p1 + p2;
        }

        public static double Substract(double p1, double p2)
        {
            return p1 - p2;
        }

        public static double Multiply(double p1, double p2)
        {
            return p1 * p2;
        }

        public static double Divide(double p1, double p2)
        { 
            if (double.IsInfinity(p1 / p2))
            {
                MessageBox.Show("Invalid operation", "Error !!!", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;//Totot
            }

            return p1 / p2;
        }
    }
}
