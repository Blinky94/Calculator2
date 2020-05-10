using System;
using System.Reflection;
using System.Windows;

namespace Udemy_Calculator
{
    public static class MathOperation
    {
        public static decimal Add(decimal p1, decimal p2)
        {
            decimal lResult;

            try
            {
                lResult = decimal.Add(p1, p2);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return lResult;
        }

        public static decimal Substract(decimal p1, decimal p2)
        {
            decimal lResult;

            try
            {
                lResult = decimal.Subtract(p1, p2);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return lResult;
        }

        public static decimal Multiply(decimal p1, decimal p2)
        {
            decimal lResult;

            try
            {
                lResult = decimal.Multiply(p1, p2);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return lResult;
        }

        public static decimal Divide(decimal p1, decimal p2)
        {
            decimal lResult;

            try
            {
                lResult = decimal.Divide(p1, p2);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return lResult;
        }

        public static decimal Exponent(decimal p1, decimal p2)
        {
            decimal lResult;

            try
            {
                lResult = Convert.ToDecimal(Math.Pow(Convert.ToDouble(p1), Convert.ToDouble(p2)));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return lResult;
        }

        public static decimal Sqrt(decimal p)
        {
            decimal lResult;

            try
            {
                lResult = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(p)));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return lResult;
        }
    }
}
