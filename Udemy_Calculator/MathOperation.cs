using System;
using System.Windows;

namespace Udemy_Calculator
{
    public class MathOperation
    {
        internal double Add(double p1, double p2)
        {
            try
            {
                return p1 + p2;
            }
            catch (OverflowException e)
            {
                MessageBox.Show(e.Message.ToString(), "Error !!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return default;
        }

        internal double Substract(double p1, double p2)
        {
            try
            {
                return p1 - p2;
            }
            catch (OverflowException e)
            {
                MessageBox.Show(e.Message.ToString(), "Error !!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return default;
        }

        internal double Multiply(double p1, double p2)
        {
            try
            {
                return p1 * p2;
            }
            catch (OverflowException e)
            {
                MessageBox.Show(e.Message.ToString(), "Error !!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return default;
        }

        internal double Divide(double p1, double p2)
        {
            try
            {
                return p1 / p2;
            }
            catch (DivideByZeroException e)
            {
                MessageBox.Show(e.Message.ToString(), "Error !!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return default;
        }

        internal double Exponent(double p1, double p2)
        {
            try
            {
                return Math.Pow(p1, p2);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "Error !!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return default;
        }
    }
}
