using System;
using System.Windows;

namespace Udemy_Calculator
{
    internal class MathCompute
    {
        private static string mFormula;


        public static bool SetCalculus(string pFormula, out decimal pResult)
        {
            pResult = default;

            if (!string.IsNullOrEmpty(pFormula))
            {
                mFormula = pFormula;
                return Compute(out pResult);            
            }

            return false;
        }

        private static bool Compute(out decimal pResult)
        {
            pResult = default;
            
            if (IsGoodFormula())
            {
                pResult = ComputeResultFromFormula();
            }

            return false;
        }

        /// <summary>
        /// Compute the final result from the formula
        /// </summary>
        /// <returns></returns>
        private static decimal ComputeResultFromFormula()
        {
            MakeTreeOperationOrder();
            throw new NotImplementedException();
        }

        /// <summary>
        /// Make the order operation tree
        /// </summary>
        private static void MakeTreeOperationOrder()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check if there are missing or mistake in the formula
        /// </summary>
        /// <param name="pFormula"></param>
        private static bool IsGoodFormula()
        {
            throw new NotImplementedException();
        }

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

            return 0;
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

            return 0;
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

            return 0;
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

            return 0;
        }
    }
}
