using System;
using System.Reflection;
using System.Windows;

namespace Udemy_Calculator
{
    public static class MathOperation
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
            if(p2 == 0)
            {
                throw new DivideByZeroException("The division by zero is forbidden !!!");
            }

            return p1 / p2;
        }

        public static decimal Exponent(decimal p1, decimal p2)
        {
            return Convert.ToDecimal(Math.Pow(Convert.ToDouble(p1), Convert.ToDouble(p2)));
        }

        public static decimal Sqrt(decimal p)
        {
            return Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(p)));
        }
    }
}
