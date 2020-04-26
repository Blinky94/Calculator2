using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Udemy_Calculator_Tests
{
    [TestClass]
    public class Arithmetics_Tests
    {
        private string mNewFormula = string.Empty;

        [TestMethod]
        public void ComplexeFormulaWithExponent_ReturnsResult()
        {
            string lFormula = "(5^(4/2))-2/3*((7.5-(3^2))*(3*(7-2)))";

            mNewFormula = lFormula;
            var lResult = ResolveFormula(ref mNewFormula);
            Assert.IsTrue(lResult);
            Assert.AreEqual("25-2/3*((7.5-9)*(3*(7-2)))", lFormula);
        }

        // Tant qu'il y a des parentheses, aller dans la parenthese
        // Calculer l'expression
        // Remplacer l'expression par le resultat
        // Retourner la formule modifiée

        private bool EvaluateParenthesis(string pOutput)
        {
            if (pOutput.Substring(0, pOutput.IndexOf('(')).Count() == pOutput.Substring(0, pOutput.IndexOf(')')).Count())
            {
                return true;
            }
            else
            {
                throw new ArithmeticException("The formula doesn't not contains equivalent number of opened and closed brakets !!!");
            }
        }

        private string ComputeParenthesis(string pOutput)
        {
            string lResult = string.Empty;

            if (!EvaluateParenthesis(pOutput))
            {
                return null;
            }
            
            while(pOutput.Substring(0, pOutput.IndexOf('(')).Count() + pOutput.Substring(0, pOutput.IndexOf(')')).Count() >= 2)
            {
                lResult = ComputeParenthesis(pOutput);
            }        

            return lResult;
        }

        private bool ResolveFormula(ref string mNewFormula)
        {
            bool lIsResolved = false;

            while (!lIsResolved)
            {
                lIsResolved = ResolveParentheses(ref mNewFormula) && ResolveExponents(ref mNewFormula) && ResolveMultiplicationAnDivision(ref mNewFormula) && ResolveAdditionAndSubstraction(ref mNewFormula);
            }

            return false;
        }

        private bool ResolveParentheses(ref string mNewFormula)
        {
            return false;
        }

        private bool ResolveExponents(ref string mNewFormula)
        {
            return false;
        }

        private bool ResolveMultiplicationAnDivision(ref string mNewFormula)
        {
            return false;
        }

        private bool ResolveAdditionAndSubstraction(ref string mNewFormula)
        {
            return false;
        }

        private string Add(string pExpression)
        {
            string lResult = string.Empty;

            return lResult;
        }

        private string Substract(string pExpression)
        {
            string lResult = string.Empty;

            return lResult;
        }

        private string Multiply(string pExpression)
        {
            string lResult = string.Empty;

            return lResult;
        }

        private string Divide(string pExpression)
        {
            string lResult = string.Empty;

            return lResult;
        }

        private string Exponent(string pExpression)
        {
            string lResult = string.Empty;



            return lResult;
        }
    }
}
