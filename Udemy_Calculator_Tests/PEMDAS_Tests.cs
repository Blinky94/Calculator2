using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Udemy_Calculator;

namespace Udemy_Calculator_Tests
{

    [TestClass]
    public class PEMDAS_Tests
    {
        #region Test on parenthesis

        [TestMethod]
        public void ParenthesisEquivalence_BadNumberParenthesisOpenedAndClosed_ReturnsNotCompleted()
        {
            // Arrange
            StringBuilder lFormula = new StringBuilder("4*((5/2)");

            // Act
            PEMDAS lPEMDAS = new PEMDAS(lFormula.ToString());
            bool lboolParenthesis = lPEMDAS.ParenthesisAreEquivalent(lFormula);

            // Assert
            Assert.IsFalse(lboolParenthesis);
        }

        [TestMethod]
        public void ParenthesisEquivalence_GoodNumberParenthesisOpenedAndClosed_ReturnsCompleted()
        {
            // Arrange
            StringBuilder lFormula = new StringBuilder("4*((5/2))");

            // Act
            PEMDAS lPEMDAS = new PEMDAS(lFormula.ToString());
            bool lboolParenthesis = lPEMDAS.ParenthesisAreEquivalent(lFormula);

            // Assert
            Assert.IsTrue(lboolParenthesis);
        }

        [TestMethod]
        public void IsRealOpenedParenthesis_NotOpenedParenthesis_ReturnsTrue()
        {
            // Arrange
            string lFormula = "(5+2)";

            // Act
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.mChunk = new Chunk(new StringBuilder(lFormula), 0, lFormula.Length);

            // Assert
            Assert.IsTrue(lPEMDAS.IsRealOpenedParenthesis(lPEMDAS.mChunk)); // GOOD '('
        }

        [TestMethod]
        public void IsRealOpenedParenthesis_RealOpenedParenthesis_ReturnsTrue()
        {
            // Arrange
            string lFormula = "4*((5/2))";

            // Act
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.mChunk = new Chunk(new StringBuilder(lFormula), 2, lFormula.Length);

            // Assert
            Assert.IsTrue(lPEMDAS.IsRealOpenedParenthesis(lPEMDAS.mChunk)); // GOOD '('
        }

        [TestMethod]
        public void IsRealOpenedParenthesis_ClosedParenthesis_ReturnsFalse()
        {
            // Arrange
            string lFormula = "4*((5/2))";

            // Act
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.mChunk = new Chunk(new StringBuilder(lFormula), 7, lFormula.Length);

            // Assert
            Assert.IsFalse(lPEMDAS.IsRealOpenedParenthesis(lPEMDAS.mChunk)); // BAD ')'
        }

        [TestMethod]
        public void IsRealOpenedParenthesis_ParenthesisPreceedByExponentSymbol_ReturnsFalse()
        {
            // Arrange
            string lFormula = "2^(5/3)+4*((5/2))";

            // Act
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.mChunk = new Chunk(new StringBuilder(lFormula), 2, lFormula.Length);

            // Assert
            Assert.IsFalse(lPEMDAS.IsRealOpenedParenthesis(lPEMDAS.mChunk)); // BAD '^('
        }

        [TestMethod]
        public void ComputeParenthesis_PutFormulaWithParenthesis_ReturnsChunkWithTheSmallerParenthesis()
        {
            // Arrange
            string lFormula = "2^(5/3)+4*((5/2))";

            // Act
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.mChunk = new Chunk(new StringBuilder(lFormula), 0, lFormula.Length);
            lPEMDAS.ComputeParenthesis();

            // Assert
            Assert.AreEqual("(5/2)", lPEMDAS.mChunk.SB.ToString());
        }

        #endregion

        #region Tests on exponents

        #region ComputeExponent

        [TestMethod]
        public void ComputeExponent_WithFormula_ReturnsLastChunk()
        {
            PEMDAS lPemdas = new PEMDAS("5+7^(3^(5/2))");

            lPemdas.ComputeExponent();

            Assert.AreEqual(4, lPemdas.mChunk.StartIndex);
            Assert.AreEqual("(3^(5/2))", lPemdas.mChunk.SB.ToString());
            Assert.AreEqual(9, lPemdas.mChunk.Length);
        }

        #endregion

        #region ExtractExponentChunk

        [TestMethod]
        public void ExtractExponentChunk_WithCompleteFormula_ReturnsChunk()
        {
            PEMDAS lPemdas = new PEMDAS("5+7^(3^(5/2))");

            StringBuilder lSb = lPemdas.mChunk.SB;

            lPemdas.ExtractExponentChunk(ref lSb);

            Assert.AreEqual("7^(3^(5/2))", lSb.ToString());
        }

        #endregion

        #region EraseLeftElementsFromExponent

        [TestMethod]
        public void EraseLeftElementsFromExponent_CaseLeftDecimalUnderParenthesis_ReturnsLeftChunk()
        {
            // Arrange
            string lFormula = "(51^(5))";
            int lIndex = 3;

            // Act
            StringBuilder lSb = new StringBuilder(lFormula);
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.DeleteLeftSequence(ref lSb, ref lIndex);

            // Assert
            Assert.AreEqual("51^(5))", lSb.ToString());
        }

        [TestMethod]
        public void EraseLeftElementsFromExponent_CaseLeftDecimalWithNoParenthesis_ReturnsLeftChunk()
        {
            // Arrange
            string lFormula = "51^(5)";
            int lIndex = 2;

            // Act
            StringBuilder lSb = new StringBuilder(lFormula);
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.DeleteLeftSequence(ref lSb, ref lIndex);

            // Assert
            Assert.AreEqual("51^(5)", lSb.ToString());
        }

        [TestMethod]
        public void EraseLeftElementsFromExponent_CaseLeftDecimalWithOperatorBefore_ReturnsLeftChunk()
        {
            // Arrange
            string lFormula = "5+6^(2)";
            int lIndex = 3;

            // Act
            StringBuilder lSb = new StringBuilder(lFormula);
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.DeleteLeftSequence(ref lSb, ref lIndex);

            // Assert
            Assert.AreEqual("6^(2)", lSb.ToString());
        }

        [TestMethod]
        public void EraseLeftElementsFromExponent_CaseLeftDecimalWithMultipleOperatorsBefore_ReturnsLeftChunk()
        {
            // Arrange
            string lFormula = "5*(2+(5^(5/3)))";
            int lIndex = 7;

            // Act
            StringBuilder lSb = new StringBuilder(lFormula);
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.DeleteLeftSequence(ref lSb, ref lIndex);

            // Assert
            Assert.AreEqual("5^(5/3)))", lSb.ToString());
        }

        [TestMethod]
        public void EraseLeftElementsFromExponent_CaseLeftDecimalWithOtherExponents_ReturnsLeftChunk()
        {
            // Arrange
            string lFormula = "2^(5^(3/2))";
            int lIndex = 1;

            // Act
            StringBuilder lSb = new StringBuilder(lFormula);
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.DeleteLeftSequence(ref lSb, ref lIndex);

            // Assert
            Assert.AreEqual("2^(5^(3/2))", lSb.ToString());
        }

        #endregion

        #region EraseRightElementsFromExponent

        [TestMethod]
        public void EraseRightElementsFromExponent_CaseRightDecimalUnderParenthesis_ReturnsRightChunk()
        {
            // Arrange
            string lFormula = "(51^(5))+5/3";
            int lIndex = 3;

            // Act
            StringBuilder lSb = new StringBuilder(lFormula);
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.DeleteRightSequenceWithParenthesis(ref lSb, lIndex);

            // Assert
            Assert.AreEqual("(51^(5)", lSb.ToString());
        }

        [TestMethod]
        public void EraseRightElementsFromExponent_CaseRightDecimalWithNoParenthesis_ReturnsRightChunk()
        {
            // Arrange
            string lFormula = "51^(5)";
            int lIndex = 2;

            // Act
            StringBuilder lSb = new StringBuilder(lFormula);
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.DeleteRightSequenceWithParenthesis(ref lSb, lIndex);

            // Assert
            Assert.AreEqual("51^(5)", lSb.ToString());
        }

        [TestMethod]
        public void EraseRightElementsFromExponent_CaseRightDecimalWithOperatorAfter_ReturnsRightChunk()
        {
            // Arrange
            string lFormula = "5+6^(2)";
            int lIndex = 3;

            // Act
            StringBuilder lSb = new StringBuilder(lFormula);
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.DeleteRightSequenceWithParenthesis(ref lSb, lIndex);

            // Assert
            Assert.AreEqual("5+6^(2)", lSb.ToString());
        }

        [TestMethod]
        public void EraseRightElementsFromExponent_CaseRightDecimalWithMultipleOperatorsAfter_ReturnsRightChunk()
        {
            // Arrange
            string lFormula = "5*(2+(5^(5/3)))*7";
            int lIndex = 7;

            // Act
            StringBuilder lSb = new StringBuilder(lFormula);
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.DeleteRightSequenceWithParenthesis(ref lSb, lIndex);

            // Assert
            Assert.AreEqual("5*(2+(5^(5/3)", lSb.ToString());
        }

        [TestMethod]
        public void EraseRightElementsFromExponent_CaseRightDecimalWithOtherExponents_ReturnsRightChunk()
        {
            // Arrange
            string lFormula = "2^(5^(3/2))+5^(2)";
            int lIndex = 1;

            // Act
            StringBuilder lSb = new StringBuilder(lFormula);
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.DeleteRightSequenceWithParenthesis(ref lSb, lIndex);

            // Assert
            Assert.AreEqual("2^(5^(3/2))", lSb.ToString());
        }

        #endregion

        #region GetChunkOfExponent

        [TestMethod]
        public void GetChunkOfExponent_SimpleOne_ReturnsChunkOfExponent()
        {
            // Arrange
            string lFormula = "2^(5)";

            // Act
            StringBuilder lSb = new StringBuilder(lFormula);
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.GetChunkOfExponent(ref lSb);

            // Assert
            Assert.AreEqual(lFormula, lSb.ToString());
        }

        [TestMethod]
        public void GetChunkOfExponent_MultipleExponents_ReturnsFirst()
        {
            // Arrange
            string lFormula = "2^(5^(3/2))+5^(2)";

            // Act
            StringBuilder lSb = new StringBuilder(lFormula);
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.GetChunkOfExponent(ref lSb);

            // Assert
            Assert.AreEqual("2^(5^(3/2))", lSb.ToString());
        }

        #endregion

        #endregion

        #region Tests on Multiplication or Division

        // internal void DeleteRightSequence(ref StringBuilder pSbChunk, int pStartIndex)

        [TestMethod]
        public void DeleteRightSequence_WithFormula_ReturnsLeftSequence()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8+(5*3/2)");

            // Act
            StringBuilder lSb = lPemdas.mChunk.SB;
            lPemdas.DeleteRightSequence(ref lSb, 4);

            // Assert
            Assert.AreEqual("8+(5*3", lPemdas.mChunk.SB.ToString());
        }

        [TestMethod]
        public void DeleteRightSequence_WithFormula2_ReturnsLeftSequence()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8+(5*3.5/2)");

            // Act
            StringBuilder lSb = lPemdas.mChunk.SB;
            lPemdas.DeleteRightSequence(ref lSb, 4);

            // Assert
            Assert.AreEqual("8+(5*3.5", lPemdas.mChunk.SB.ToString());
        }

        [TestMethod]
        public void DeleteLeftSequence_WithFormula_ReturnsLeftSequence()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8+(5*3/2)");

            // Act
            StringBuilder lSb = lPemdas.mChunk.SB;
            int lIndex = 4;
            lPemdas.DeleteLeftSequence(ref lSb, ref lIndex);

            // Assert
            Assert.AreEqual("5*3/2)", lPemdas.mChunk.SB.ToString());
        }

        [TestMethod]
        public void DeleteLeftSequence_WithFormula2_ReturnsLeftSequence()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8+(5.62*3.5/2)");

            // Act
            StringBuilder lSb = lPemdas.mChunk.SB;
            int lIndex = 4;
            lPemdas.DeleteLeftSequence(ref lSb, ref lIndex);

            // Assert
            Assert.AreEqual("5.62*3.5/2)", lPemdas.mChunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithMultiply_ReturnsMultiplicationChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8*5+(5^(10))");

            // Act
            lPemdas.ComputeMultiplicationOrDivision();

            // Assert
            Assert.AreEqual("8*5", lPemdas.mChunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithMultiply2_ReturnsMultiplicationChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6+2-8*5+(5^(10))");

            // Act
            lPemdas.ComputeMultiplicationOrDivision();

            // Assert
            Assert.AreEqual("8*5", lPemdas.mChunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithDivide_ReturnsDivideChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8/5+(5^(10))");

            // Act
            lPemdas.ComputeMultiplicationOrDivision();

            // Assert
            Assert.AreEqual("8/5", lPemdas.mChunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithDivide2_ReturnsDivideChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6+2-8/5+(5^(10))");

            // Act
            lPemdas.ComputeMultiplicationOrDivision();

            // Assert
            Assert.AreEqual("8/5", lPemdas.mChunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithMultiplyFirstAndDivision_ReturnsMultiplicationChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8*5+3/2+(5^(10))");

            // Act
            lPemdas.ComputeMultiplicationOrDivision();

            // Assert
            Assert.AreEqual("8*5", lPemdas.mChunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithMultiplyFirstAndDivision2_ReturnsMultiplicationChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6+2-8*5+3/2+(5^(10))");

            // Act
            lPemdas.ComputeMultiplicationOrDivision();

            // Assert
            Assert.AreEqual("8*5", lPemdas.mChunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithDivideFirstAndMultiplication_ReturnsDivisionChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8/5+3*2+(5^(10))");

            // Act
            lPemdas.ComputeMultiplicationOrDivision();

            // Assert
            Assert.AreEqual("8/5", lPemdas.mChunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithDivideFirstAndMultiplication2_ReturnsDivisionChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6+2-8/5+3*2+(5^(10))");

            // Act
            lPemdas.ComputeMultiplicationOrDivision();

            // Assert
            Assert.AreEqual("8/5", lPemdas.mChunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithMultiplicationFirstAndDivisionAndParenthesis_ReturnsMultiplicationChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6+2-(3*2/5)+(5^(10))");

            // Act
            lPemdas.ComputeMultiplicationOrDivision();

            // Assert
            Assert.AreEqual("3*2", lPemdas.mChunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithDivisionFirstAndMultiplicationAndParenthesis_ReturnsDivisionChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6+2-(3/2*5)+(5^(10))");

            // Act
            lPemdas.ComputeMultiplicationOrDivision();

            // Assert
            Assert.AreEqual("3/2", lPemdas.mChunk.SB.ToString());
        }

        #endregion

        #region Tests on Addition or Substraction



        #endregion
    }
}
