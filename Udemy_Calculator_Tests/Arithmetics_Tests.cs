using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Udemy_Calculator;

namespace Udemy_Calculator_Tests
{

    [TestClass]
    public class Arithmetics_Tests
    {
        private string mNewFormula = string.Empty;

        #region Test on parenthesis

        [TestMethod]
        public void ParenthesisEquivalence_BadNumberParenthesisOpenedAndClosed_ReturnsNotCompleted()
        {
            string lFormula = "4*((5/2)";

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            bool lboolParenthesis = lPEMDAS.AreParenthesisAreEquivalentNumber();
            Assert.IsFalse(lboolParenthesis);
        }

        [TestMethod]
        public void ParenthesisEquivalence_GoodNumberParenthesisOpenedAndClosed_ReturnsCompleted()
        {
            string lFormula = "4*((5/2))";

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            bool lboolParenthesis = lPEMDAS.AreParenthesisAreEquivalentNumber();
            Assert.IsTrue(lboolParenthesis);
        }

        #endregion

        #region Tests on exponents

        [TestMethod]
        public void ComplexeFormulaWithExponent_ReturnsResult()
        {
            Assert.Inconclusive();
            string lFormula = "(5^(4/2))-2/3*((7.5-(3^2))*(3*(7-2)))";

            mNewFormula = lFormula;
            //var lResult = ResolveFormula(ref mNewFormula);
           // Assert.IsTrue(lResult);
           // Assert.AreEqual("25-2/3*((7.5-9)*(3*(7-2)))", lFormula);
        }

        #endregion
    }
}
