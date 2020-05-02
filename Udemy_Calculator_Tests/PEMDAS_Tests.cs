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
    public class PEMDAS_Tests
    {

        #region Test on Constructor PEMDAS

        [TestMethod]
        public void PEMDAS_Constructor_ComputeFormula_ReturnsStreamBuilder()
        {
            string lFormula = "4*((5/2)";

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            Assert.AreEqual(lFormula, lPEMDAS.mSb.ToString());
        }

        #endregion

        #region Test on parenthesis

        [TestMethod]
        public void PEMDAS_ParenthesisEquivalence_BadNumberParenthesisOpenedAndClosed_ReturnsNotCompleted()
        {
            string lFormula = "4*((5/2)";

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            bool lboolParenthesis = lPEMDAS.ParenthesisAreEquivalent();
            Assert.IsFalse(lboolParenthesis);
        }

        [TestMethod]
        public void PEMDAS_ParenthesisEquivalence_GoodNumberParenthesisOpenedAndClosed_ReturnsCompleted()
        {
            string lFormula = "4*((5/2))";

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            bool lboolParenthesis = lPEMDAS.ParenthesisAreEquivalent();

            Assert.IsTrue(lboolParenthesis);
        }

        [TestMethod]
        public void PEMDAS_IsRealOpenedParenthesis_NotOpenedParenthesis_ReturnsTrue()
        {
            string lFormula = "(5+2)";

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            Assert.IsTrue(lPEMDAS.IsRealOpenedParenthesis(0)); // GOOD '('
        }

        [TestMethod]
        public void PEMDAS_IsRealOpenedParenthesis_RealOpenedParenthesis_ReturnsTrue()
        {
            string lFormula = "4*((5/2))";

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            Assert.IsTrue(lPEMDAS.IsRealOpenedParenthesis(2)); // GOOD '('
        }

        [TestMethod]
        public void PEMDAS_IsRealOpenedParenthesis_ClosedParenthesis_ReturnsFalse()
        {
            string lFormula = "4*((5/2))";

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            Assert.IsFalse(lPEMDAS.IsRealOpenedParenthesis(7)); // BAD ')'
        }

        [TestMethod]
        public void PEMDAS_IsRealOpenedParenthesis_ParenthesisPreceedByExponentSymbol_ReturnsFalse()
        {
            string lFormula = "2^(5/3)+4*((5/2))";

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            Assert.IsFalse(lPEMDAS.IsRealOpenedParenthesis(2)); // BAD '^('
        }

        [TestMethod]
        public void PENDAS_ComputeParenthesis_PutFormulaWithParenthesis_ReturnsChunkWithParenthesis()
        {
            string lFormula = "2^(5/3)+4*((5/2))";

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            lPEMDAS.ComputeParenthesis();

            Assert.AreEqual("(5/2)", new string(lPEMDAS.mChunk));
        }

        #endregion

        #region Tests on exponents



        #endregion
    }
}
