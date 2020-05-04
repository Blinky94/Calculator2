﻿using System;
using System.Reflection;
using System.Windows;

namespace Udemy_Calculator
{
    public static class MathOperation
    {
        /// <summary>
        /// Get the result of the operation or throw exception
        /// </summary>
        /// <param name="pResult"></param>
        /// <param name="pMethodName"></param>
        /// <param name="pAdditionnalInfo"></param>
        /// <returns></returns>
        internal static double ComputeOperation(double pResult, string pMethodName, string pAdditionnalInfo = default)
        {
            if (double.IsInfinity(pResult) || double.IsNaN(pResult))
            {
                throw new Exception($"The {pMethodName} operation failed !!! " + pAdditionnalInfo);
            }

            return pResult;
        }

        public static double Add(double p1, double p2)
        {
            return ComputeOperation(p1 + p2, MethodBase.GetCurrentMethod().Name);
        }

        public static double Substract(double p1, double p2)
        {
            return ComputeOperation(p1 - p2, MethodBase.GetCurrentMethod().Name);
        }

        public static double Multiply(double p1, double p2)
        {
            return ComputeOperation(p1 * p2, MethodBase.GetCurrentMethod().Name);
        }

        public static double Divide(double p1, double p2)
        {
            return ComputeOperation(p1 / p2, MethodBase.GetCurrentMethod().Name);
        }

        public static double Exponent(double p1, double p2)
        {
            return ComputeOperation(Math.Pow(p1, p2), MethodBase.GetCurrentMethod().Name);
        }

        public static double Sqrt(double p)
        {
            return ComputeOperation(Math.Sqrt(p), MethodBase.GetCurrentMethod().Name);
        }
    }
}
