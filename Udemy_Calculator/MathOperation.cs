using System;

namespace Udemy_Calculator
{
    public static class MathOperation
    {
        public static decimal Add(decimal p1, decimal p2)
        {
            decimal lResult;

            try
            {
                lResult = Decimal.Add(p1, p2);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return lResult;
        }

        public static string Add(double p1, double p2)
        {
            string lResult;

            try
            {
                double lDouble = p1 + p2;
                lResult = lDouble.ToString();
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
                lResult = Decimal.Subtract(p1, p2);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return lResult;
        }

        public static string Substract(double p1, double p2)
        {
            string lResult;

            try
            {
                double lDouble = p1 - p2;
                lResult = lDouble.ToString();
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
                lResult = Decimal.Multiply(p1, p2);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return lResult;
        }

        public static string Multiply(double p1, double p2)
        {
            string lResult;

            try
            {
                double lDouble = p1 * p2;
                lResult = lDouble.ToString();
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
                lResult = Decimal.Divide(p1, p2);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return lResult;
        }

        public static string Divide(double p1, double p2)
        {
            string lResult;

            try
            {
                double lDouble = p1 / p2;
                lResult = lDouble.ToString();
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

        public static string Exponent(double p1, double p2)
        {
            string lResult;

            try
            {
                double lDouble = Math.Pow(p1, p2);
                lResult = lDouble.ToString();
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

        public static string Sqrt(double p)
        {
            string lResult;

            try
            {
                double lDouble = Math.Sqrt(p);
                lResult = lDouble.ToString();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return lResult;
        }
    }
}
