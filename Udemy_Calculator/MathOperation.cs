using System;

namespace Udemy_Calculator
{
    public static class MathOperation
    {
        private static string CheckIfResultIsANumber(string pMethodName, double pResult)
        {
            string lResult;
            if (double.IsNaN(pResult) || double.IsInfinity(pResult))
            {
                TraceLogs.AddError($"{pMethodName}: result is not finite number ({pResult}) !!!");
                lResult = "0";
            }
            else
            {
                lResult = pResult.ToString();
            }

            return lResult;
        }

        public static decimal Add(decimal p1, decimal p2)
        {
            decimal lResult;

            try
            {
                TraceLogs.AddInfo($"Decimal addition: {p1} + {p2}");
                lResult = Decimal.Add(p1, p2);
            }
            catch (Exception e)
            {
                TraceLogs.AddError($"Decimal addition:\n {e.Message}");
                throw new Exception(e.Message);
            }

            TraceLogs.AddInfo($"Decimal addition result: {lResult}");
            return lResult;
        }

        public static string Add(double p1, double p2)
        {
            string lResult = string.Empty;

            try
            {
                TraceLogs.AddInfo($"Add: {p1} + {p2}");
                double lDouble = p1 + p2;
                lResult = CheckIfResultIsANumber("Add", lDouble);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError($"Add:\n {e.Message}");
            }

            TraceLogs.AddInfo($"Add result: {lResult}");
            return lResult;
        }

        public static decimal Substract(decimal p1, decimal p2)
        {
            decimal lResult;

            try
            {
                TraceLogs.AddInfo($"Decimal substraction: {p1} - {p2}");
                lResult = Decimal.Subtract(p1, p2);
            }
            catch (Exception e)
            {
                TraceLogs.AddError($"Decimal substraction:\n {e.Message}");
                throw new Exception(e.Message);
            }

            TraceLogs.AddInfo($"Decimal substraction result: {lResult}");
            return lResult;
        }

        public static string Substract(double p1, double p2)
        {
            string lResult = string.Empty;

            try
            {
                TraceLogs.AddInfo($"Substract: {p1} + {p2}");
                double lDouble = p1 - p2;
                lResult = CheckIfResultIsANumber("Substract", lDouble);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError($"Substract:\n {e.Message}");
            }

            TraceLogs.AddInfo($"Substract result: {lResult}");
            return lResult;
        }

        public static decimal Multiply(decimal p1, decimal p2)
        {
            decimal lResult;

            try
            {
                TraceLogs.AddInfo($"Decimal multiplication: {p1} x {p2}");
                lResult = Decimal.Multiply(p1, p2);
            }
            catch (Exception e)
            {
                TraceLogs.AddError($"Decimal multiplication:\n {e.Message}");
                throw new Exception(e.Message);
            }

            TraceLogs.AddInfo($"Decimal multiplication result: {lResult}");
            return lResult;
        }

        public static string Multiply(double p1, double p2)
        {
            string lResult = string.Empty;

            try
            {
                TraceLogs.AddInfo($"Multiply: {p1} x {p2}");
                double lDouble = p1 * p2;
                lResult = CheckIfResultIsANumber("Multiply", lDouble);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError($"Multiply:\n {e.Message}");
            }

            TraceLogs.AddInfo($"Multiply result: {lResult}");
            return lResult;
        }

        public static decimal Divide(decimal p1, decimal p2)
        {
            decimal lResult;

            try
            {
                TraceLogs.AddInfo($"Decimal division: {p1} x {p2}");
                lResult = Decimal.Divide(p1, p2);
            }
            catch (Exception e)
            {
                TraceLogs.AddError($"Decimal division:\n {e.Message}");
                throw new Exception(e.Message);
            }

            TraceLogs.AddInfo($"Decimal division result: {lResult}");
            return lResult;
        }

        public static string Divide(double p1, double p2)
        {
            string lResult = string.Empty;

            try
            {
                TraceLogs.AddInfo($"Divide: {p1} ÷ {p2}");
                double lDouble = p1 / p2;
                lResult = CheckIfResultIsANumber("Divide", lDouble);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError($"Divide:\n {e.Message}");
            }

            TraceLogs.AddInfo($"Divide result: {lResult}");
            return lResult;
        }

        public static decimal Exponent(decimal p1, decimal p2)
        {
            decimal lResult;

            try
            {
                TraceLogs.AddInfo($"Decimal exponent: {p1}^({p2})");
                lResult = Convert.ToDecimal(Math.Pow(Convert.ToDouble(p1), Convert.ToDouble(p2)));
            }
            catch (Exception e)
            {
                TraceLogs.AddError($"Decimal exponent:\n {e.Message}");
                throw new Exception(e.Message);
            }

            TraceLogs.AddInfo($"Decimal exponent result: {lResult}");
            return lResult;
        }

        public static string Exponent(double p1, double p2)
        {
            string lResult = string.Empty;

            try
            {
                TraceLogs.AddInfo($"Exponent: {p1}^({p2})");
                double lDouble = Math.Pow(p1, p2);
                lResult = CheckIfResultIsANumber("Exponent", lDouble);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError($"Exponent:\n {e.Message}");
            }

            TraceLogs.AddInfo($"Exponent result: {lResult}");
            return lResult;
        }

        public static decimal Sqrt(decimal p)
        {
            decimal lResult;

            try
            {
                TraceLogs.AddInfo($"Decimal square: √({p})");
                lResult = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(p)));
            }
            catch (Exception e)
            {
                TraceLogs.AddError($"Decimal square:\n {e.Message}");
                throw new Exception(e.Message);
            }

            TraceLogs.AddInfo($"Decimal square result: {lResult}");
            return lResult;
        }

        public static string Sqrt(double p)
        {
            string lResult = string.Empty;

            try
            {
                TraceLogs.AddInfo($"Sqrt: √({p})");
                double lDouble = Math.Sqrt(p);
                lResult = CheckIfResultIsANumber("Sqrt", lDouble);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError($"Sqrt:\n {e.Message}");
            }

            TraceLogs.AddInfo($"Sqrt result: {lResult}");
            return lResult;
        }
    }
}
