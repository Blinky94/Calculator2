using System;
using System.Windows;

namespace Udemy_Calculator
{
    public class MathOperation
    {    
        private decimal Add(decimal p1, decimal p2)
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

        private decimal Substract(decimal p1, decimal p2)
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

        private decimal Multiply(decimal p1, decimal p2)
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

        private decimal Divide(decimal p1, decimal p2)
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
    }
}
