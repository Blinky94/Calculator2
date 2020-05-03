using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Udemy_Calculator;

namespace Udemy_Calculator_Tests
{

    [TestClass]
    public class PEMDAS_Tests
    {

        #region Test on Constructor PEMDAS

        [TestMethod]
        public void Constructor_ComputeFormula_ReturnsStreamBuilder()
        {
            Assert.Inconclusive();
            string lFormula = "4*((5/2)";

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            Assert.AreEqual(lFormula, lPEMDAS.mChunk.ToString());
        }

        #endregion

        #region Test on parenthesis

        [TestMethod]
        public void ParenthesisEquivalence_BadNumberParenthesisOpenedAndClosed_ReturnsNotCompleted()
        {
            string lFormula = "4*((5/2)";

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            bool lboolParenthesis = lPEMDAS.ParenthesisAreEquivalent(lFormula);
            Assert.IsFalse(lboolParenthesis);
        }

        [TestMethod]
        public void ParenthesisEquivalence_GoodNumberParenthesisOpenedAndClosed_ReturnsCompleted()
        {
            string lFormula = "4*((5/2))";

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            bool lboolParenthesis = lPEMDAS.ParenthesisAreEquivalent(lFormula);

            Assert.IsTrue(lboolParenthesis);
        }

        [TestMethod]
        public void IsRealOpenedParenthesis_NotOpenedParenthesis_ReturnsTrue()
        {
            string lFormula = "(5+2)";

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            lPEMDAS.mChunk = new Chunk(new StringBuilder(lFormula), 0, lFormula.Length);

            Assert.IsTrue(lPEMDAS.IsRealOpenedParenthesis(lPEMDAS.mChunk)); // GOOD '('
        }

        [TestMethod]
        public void IsRealOpenedParenthesis_RealOpenedParenthesis_ReturnsTrue()
        {
            string lFormula = "4*((5/2))";

            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.mChunk = new Chunk(new StringBuilder(lFormula), 2, lFormula.Length);

            Assert.IsTrue(lPEMDAS.IsRealOpenedParenthesis(lPEMDAS.mChunk)); // GOOD '('
        }

        [TestMethod]
        public void IsRealOpenedParenthesis_ClosedParenthesis_ReturnsFalse()
        {
            string lFormula = "4*((5/2))";

            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.mChunk = new Chunk(new StringBuilder(lFormula), 7, lFormula.Length);

            Assert.IsFalse(lPEMDAS.IsRealOpenedParenthesis(lPEMDAS.mChunk)); // BAD ')'
        }

        [TestMethod]
        public void IsRealOpenedParenthesis_ParenthesisPreceedByExponentSymbol_ReturnsFalse()
        {
            string lFormula = "2^(5/3)+4*((5/2))";

            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.mChunk = new Chunk(new StringBuilder(lFormula), 2, lFormula.Length);

            Assert.IsFalse(lPEMDAS.IsRealOpenedParenthesis(lPEMDAS.mChunk)); // BAD '^('
        }

        [TestMethod]
        public void ComputeParenthesis_PutFormulaWithParenthesis_ReturnsChunkWithTheSmallerParenthesis()
        {
            string lFormula = "2^(5/3)+4*((5/2))";

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            lPEMDAS.mChunk = new Chunk(new StringBuilder(lFormula), 0, lFormula.Length);

            lPEMDAS.ComputeParenthesis();

            Assert.AreEqual("(5/2)", lPEMDAS.mChunk.SB.ToString());
        }      

        #endregion

        #region Tests on exponents

        #region EraseLeftElementsFromExponent

        [TestMethod]
        public void EraseLeftElementsFromExponent_CaseLeftDecimalUnderParenthesis_ReturnsLeftChunk()
        {
            string lFormula = "(51^(5))";
            int lIndex = 3;

            StringBuilder lSb = new StringBuilder(lFormula);

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            lPEMDAS.EraseLeftElementsFromExponent(ref lSb, ref lIndex);

            Assert.AreEqual("51^(5))", lSb.ToString());
        }

        [TestMethod]
        public void EraseLeftElementsFromExponent_CaseLeftDecimalWithNoParenthesis_ReturnsLeftChunk()
        {
            string lFormula = "51^(5)";
            int lIndex = 2;

            StringBuilder lSb = new StringBuilder(lFormula);

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            lPEMDAS.EraseLeftElementsFromExponent(ref lSb, ref lIndex);

            Assert.AreEqual("51^(5)", lSb.ToString());
        }

        [TestMethod]
        public void EraseLeftElementsFromExponent_CaseLeftDecimalWithOperatorBefore_ReturnsLeftChunk()
        {
            string lFormula = "5+6^(2)";
            int lIndex = 3;

            StringBuilder lSb = new StringBuilder(lFormula);

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            lPEMDAS.EraseLeftElementsFromExponent(ref lSb, ref lIndex);

            Assert.AreEqual("6^(2)", lSb.ToString());
        }

        [TestMethod]
        public void EraseLeftElementsFromExponent_CaseLeftDecimalWithMultipleOperatorsBefore_ReturnsLeftChunk()
        {
            string lFormula = "5*(2+(5^(5/3)))";
            int lIndex = 7;

            StringBuilder lSb = new StringBuilder(lFormula);

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            lPEMDAS.EraseLeftElementsFromExponent(ref lSb, ref lIndex);

            Assert.AreEqual("5^(5/3)))", lSb.ToString());
        }

        [TestMethod]
        public void EraseLeftElementsFromExponent_CaseLeftDecimalWithOtherExponents_ReturnsLeftChunk()
        {
            string lFormula = "2^(5^(3/2))";
            int lIndex = 1;

            StringBuilder lSb = new StringBuilder(lFormula);

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            lPEMDAS.EraseLeftElementsFromExponent(ref lSb, ref lIndex);

            Assert.AreEqual("2^(5^(3/2))", lSb.ToString());
        }

        #endregion

        #region EraseRightElementsFromExponent

        [TestMethod]
        public void EraseRightElementsFromExponent_CaseRightDecimalUnderParenthesis_ReturnsRightChunk()
        {
            string lFormula = "(51^(5))";
            int lIndex = 3;

            StringBuilder lSb = new StringBuilder(lFormula);

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            lPEMDAS.EraseRightElementsFromExponent(ref lSb, lIndex);

            Assert.AreEqual("(51^(5)", lSb.ToString());
        }

        [TestMethod]
        public void EraseRightElementsFromExponent_CaseRightDecimalWithNoParenthesis_ReturnsRightChunk()
        {
            string lFormula = "51^(5)";
            int lIndex = 2;

            StringBuilder lSb = new StringBuilder(lFormula);

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            lPEMDAS.EraseRightElementsFromExponent(ref lSb, lIndex);

            Assert.AreEqual("51^(5)", lSb.ToString());
        }

        [TestMethod]
        public void EraseRightElementsFromExponent_CaseRightDecimalWithOperatorAfter_ReturnsRightChunk()
        {
            string lFormula = "5+6^(2)";
            int lIndex = 3;

            StringBuilder lSb = new StringBuilder(lFormula);

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            lPEMDAS.EraseRightElementsFromExponent(ref lSb, lIndex);

            Assert.AreEqual("5+6^(2)", lSb.ToString());
        }

        [TestMethod]
        public void EraseRightElementsFromExponent_CaseRightDecimalWithMultipleOperatorsAfter_ReturnsRightChunk()
        {
            string lFormula = "5*(2+(5^(5/3)))";
            int lIndex = 7;

            StringBuilder lSb = new StringBuilder(lFormula);

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            lPEMDAS.EraseRightElementsFromExponent(ref lSb, lIndex);

            Assert.AreEqual("5*(2+(5^(5/3)", lSb.ToString());
        }

        [TestMethod]
        public void EraseRightElementsFromExponent_CaseRightDecimalWithOtherExponents_ReturnsRightChunk()
        {
            string lFormula = "2^(5^(3/2))+5^(2)";
            int lIndex = 1;

            StringBuilder lSb = new StringBuilder(lFormula);

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            lPEMDAS.EraseRightElementsFromExponent(ref lSb, lIndex);

            Assert.AreEqual("2^(5^(3/2))", lSb.ToString());
        }

        #endregion

        #region IndexOfFirstSymbol

        [TestMethod]
        public void IndexOfFirstSymbol_OneExponent_ReturnsIndex()
        {
            string lFormula = "2^(5)";

            StringBuilder lSb = new StringBuilder(lFormula);

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            int lResult = lPEMDAS.IndexOfFirstSymbol(lSb, '^');

            Assert.AreEqual(1, lResult);
        }

        [TestMethod]
        public void IndexOfFirstSymbol_MultipleExponents_ReturnsIndex()
        {
            string lFormula = "2^(5^(3/2))+5^(2)";

            StringBuilder lSb = new StringBuilder(lFormula);

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            int lResult = lPEMDAS.IndexOfFirstSymbol(lSb, '^');

            Assert.AreEqual(1, lResult);
        }

        [TestMethod]
        public void IndexOfFirstSymbol_NoExponents_ReturnsIndex()
        {
            string lFormula = "2+3*(5/2)";

            StringBuilder lSb = new StringBuilder(lFormula);

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            int lResult = lPEMDAS.IndexOfFirstSymbol(lSb, '^');

            Assert.AreEqual(0, lResult);
        }

        #endregion

        #region GetChunkOfExponent

        [TestMethod]
        public void GetChunkOfExponent_SimpleOne_ReturnsChunkOfExponent()
        {
            string lFormula = "2^(5)";

            StringBuilder lSb = new StringBuilder(lFormula);

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            lPEMDAS.GetChunkOfExponent(ref lSb);

            Assert.AreEqual(lFormula, lSb.ToString());
        }

        [TestMethod]
        public void GetChunkOfExponent_MultipleExponents_ReturnsFirst()
        {
            string lFormula = "2^(5^(3/2))+5^(2)";

            StringBuilder lSb = new StringBuilder(lFormula);

            PEMDAS lPEMDAS = new PEMDAS(lFormula);

            lPEMDAS.GetChunkOfExponent(ref lSb);

            Assert.AreEqual("2^(5^(3/2))", lSb.ToString());
        }

        #endregion

        #endregion
    }
}
