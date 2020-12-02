using System;
using System.Globalization;

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

        public static string Add(double p1, double p2)
        {
            string lResult = string.Empty;

            try
            {
                TraceLogs.AddInfo($"{GlobalUsage.GetCurrentMethodName}: {p1} + {p2}");
                double lDouble = p1 + p2;
                lResult = CheckIfResultIsANumber(GlobalUsage.GetCurrentMethodName, lDouble);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError($"{GlobalUsage.GetCurrentMethodName}:\n {e.Message}");
            }

            TraceLogs.AddInfo($"{GlobalUsage.GetCurrentMethodName}: {lResult}");
            return lResult;
        }

        public static string Substract(double p1, double p2)
        {
            string lResult = string.Empty;

            try
            {
                TraceLogs.AddInfo($"{GlobalUsage.GetCurrentMethodName}: {p1} - {p2}");
                double lDouble = p1 - p2;
                lResult = CheckIfResultIsANumber(GlobalUsage.GetCurrentMethodName, lDouble);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError($"{GlobalUsage.GetCurrentMethodName}:\n {e.Message}");
            }

            TraceLogs.AddInfo($"{GlobalUsage.GetCurrentMethodName}: {lResult}");
            return lResult;
        }

        public static string Multiply(double p1, double p2)
        {
            string lResult = string.Empty;

            try
            {
                TraceLogs.AddInfo($"{GlobalUsage.GetCurrentMethodName}: {p1} x {p2}");
                double lDouble = p1 * p2;
                lResult = CheckIfResultIsANumber(GlobalUsage.GetCurrentMethodName, lDouble);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError($"{GlobalUsage.GetCurrentMethodName}:\n {e.Message}");
            }

            TraceLogs.AddInfo($"{GlobalUsage.GetCurrentMethodName}: {lResult}");
            return lResult;
        }

        public static string Divide(double p1, double p2)
        {
            string lResult = string.Empty;

            try
            {
                TraceLogs.AddInfo($"{GlobalUsage.GetCurrentMethodName}: {p1} ÷ {p2}");
                double lDouble = p1 / p2;
                lResult = CheckIfResultIsANumber(GlobalUsage.GetCurrentMethodName, lDouble);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError($"{GlobalUsage.GetCurrentMethodName}:\n {e.Message}");
            }

            TraceLogs.AddInfo($"{GlobalUsage.GetCurrentMethodName}: {lResult}");
            return lResult;
        }

        public static string Exponent(double pBase)
        {
            string lResult = string.Empty;

            try
            {
                TraceLogs.AddInfo($"{GlobalUsage.GetCurrentMethodName}: {pBase}");

                lResult = decimal.Parse(pBase.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture).ToString();
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError($"{GlobalUsage.GetCurrentMethodName}:\n {e.Message}");
            }

            TraceLogs.AddInfo($"{GlobalUsage.GetCurrentMethodName}: {lResult}");
            return lResult;
        }

        public static string Sqrt(double p)
        {
            string lResult = string.Empty;

            try
            {
                TraceLogs.AddInfo($"{GlobalUsage.GetCurrentMethodName}: √({p})");
                double lDouble = Math.Sqrt(p);
                lResult = CheckIfResultIsANumber(GlobalUsage.GetCurrentMethodName, lDouble);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError($"{GlobalUsage.GetCurrentMethodName}:\n {e.Message}");
            }

            TraceLogs.AddInfo($"{GlobalUsage.GetCurrentMethodName}: {lResult}");
            return lResult;
        }
    }
}
