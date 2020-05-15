using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            StringBuilder lFormula = new StringBuilder("4×((5÷2)");

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
            StringBuilder lFormula = new StringBuilder("4×((5÷2))");

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
            lPEMDAS.Chunk = new Chunk(new StringBuilder(lFormula), 0, lFormula.Length);

            // Assert
            Assert.IsTrue(lPEMDAS.IsRealOpenedParenthesis(lPEMDAS.Chunk)); // GOOD '('
        }

        [TestMethod]
        public void IsRealOpenedParenthesis_RealOpenedParenthesis_ReturnsTrue()
        {
            // Arrange
            string lFormula = "4×((5÷2))";

            // Act
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.Chunk = new Chunk(new StringBuilder(lFormula), 2, lFormula.Length);

            // Assert
            Assert.IsTrue(lPEMDAS.IsRealOpenedParenthesis(lPEMDAS.Chunk)); // GOOD '('
        }

        [TestMethod]
        public void IsRealOpenedParenthesis_ClosedParenthesis_ReturnsFalse()
        {
            // Arrange
            string lFormula = "4×((5÷2))";

            // Act
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.Chunk = new Chunk(new StringBuilder(lFormula), 7, lFormula.Length);

            // Assert
            Assert.IsFalse(lPEMDAS.IsRealOpenedParenthesis(lPEMDAS.Chunk)); // BAD ')'
        }

        [TestMethod]
        public void IsRealOpenedParenthesis_ParenthesisPreceedByExponentSymbol_ReturnsFalse()
        {
            // Arrange
            string lFormula = "2^(5÷3)+4×((5÷2))";

            // Act
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.Chunk = new Chunk(new StringBuilder(lFormula), 2, lFormula.Length);

            // Assert
            Assert.IsFalse(lPEMDAS.IsRealOpenedParenthesis(lPEMDAS.Chunk)); // BAD '^('
        }

        [TestMethod]
        public void ComputeParenthesis_PutFormulaWithParenthesis_ReturnsChunkWithTheSmallerParenthesis()
        {
            // Arrange
            string lFormula = "2^(5÷3)+4×((5÷2))";

            // Act
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.Chunk = new Chunk(new StringBuilder(lFormula), 0, lFormula.Length);
            lPEMDAS.ComputeParenthesis();

            // Assert
            Assert.AreEqual("(5÷2)", lPEMDAS.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeParenthesis_PutFormulaWithParenthesis_ReturnsStartIndexAndLength()
        {
            // Arrange
            string lFormula = "2^(5÷3)+4×((5÷2))";

            // Act
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.Chunk = new Chunk(new StringBuilder(lFormula), 0, lFormula.Length);
            lPEMDAS.ComputeParenthesis();

            // Assert
            Assert.AreEqual(11, lPEMDAS.Chunk.StartIndex);
            Assert.AreEqual(5, lPEMDAS.Chunk.Length);
        }

        #endregion

        #region Tests on exponents

        #region ComputeExponent

        [TestMethod]
        public void ComputeExponent_WithFormula_ReturnsLastChunk()
        {
            PEMDAS lPemdas = new PEMDAS("5+7^(3^(5÷2))");

            lPemdas.ComputeExponent();

            Assert.AreEqual(4, lPemdas.Chunk.StartIndex);
            Assert.AreEqual("(3^(5÷2))", lPemdas.Chunk.SB.ToString());
            Assert.AreEqual(9, lPemdas.Chunk.Length);
        }

        #endregion

        #region ExtractExponentChunk

        [TestMethod]
        public void ExtractExponentChunk_WithCompleteFormula_ReturnsChunk()
        {
            PEMDAS lPemdas = new PEMDAS("5+7^(3^(5÷2))");

            StringBuilder lSb = lPemdas.Chunk.SB;

            lPemdas.ExtractExponentChunk(ref lSb);

            Assert.AreEqual("7^(3^(5÷2))", lSb.ToString());
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
            string lFormula = "5×(2+(5^(5÷3)))";
            int lIndex = 7;

            // Act
            StringBuilder lSb = new StringBuilder(lFormula);
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.DeleteLeftSequence(ref lSb, ref lIndex);

            // Assert
            Assert.AreEqual("5^(5÷3)))", lSb.ToString());
        }

        [TestMethod]
        public void EraseLeftElementsFromExponent_CaseLeftDecimalWithOtherExponents_ReturnsLeftChunk()
        {
            // Arrange
            string lFormula = "2^(5^(3÷2))";
            int lIndex = 1;

            // Act
            StringBuilder lSb = new StringBuilder(lFormula);
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.DeleteLeftSequence(ref lSb, ref lIndex);

            // Assert
            Assert.AreEqual("2^(5^(3÷2))", lSb.ToString());
        }

        #endregion

        #region EraseRightElementsFromExponent

        [TestMethod]
        public void EraseRightElementsFromExponent_CaseRightDecimalUnderParenthesis_ReturnsRightChunk()
        {
            // Arrange
            string lFormula = "(51^(5))+5÷3";
            int lIndex = 3;

            // Act
            StringBuilder lSb = new StringBuilder(lFormula);
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.DeleteFromExponentOverParenthesis(ref lSb, lIndex);

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
            lPEMDAS.DeleteFromExponentOverParenthesis(ref lSb, lIndex);

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
            lPEMDAS.DeleteFromExponentOverParenthesis(ref lSb, lIndex);

            // Assert
            Assert.AreEqual("5+6^(2)", lSb.ToString());
        }

        [TestMethod]
        public void EraseRightElementsFromExponent_CaseRightDecimalWithMultipleOperatorsAfter_ReturnsRightChunk()
        {
            // Arrange
            string lFormula = "5×(2+(5^(5÷3)))×7";
            int lIndex = 7;

            // Act
            StringBuilder lSb = new StringBuilder(lFormula);
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.DeleteFromExponentOverParenthesis(ref lSb, lIndex);

            // Assert
            Assert.AreEqual("5×(2+(5^(5÷3)", lSb.ToString());
        }

        [TestMethod]
        public void EraseRightElementsFromExponent_CaseRightDecimalWithOtherExponents_ReturnsRightChunk()
        {
            // Arrange
            string lFormula = "2^(5^(3÷2))+5^(2)";
            int lIndex = 1;

            // Act
            StringBuilder lSb = new StringBuilder(lFormula);
            PEMDAS lPEMDAS = new PEMDAS(lFormula);
            lPEMDAS.DeleteFromExponentOverParenthesis(ref lSb, lIndex);

            // Assert
            Assert.AreEqual("2^(5^(3÷2))", lSb.ToString());
        }

        #endregion

        #endregion

        #region Tests on Multiplication or Division

        #region Delete Sequences

        [TestMethod]
        public void DeleteRightSequence_WithFormula_ReturnsLeftSequence()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8+(5×3÷2)");

            // Act
            StringBuilder lSb = lPemdas.Chunk.SB;
            lPemdas.DeleteRightSequence(ref lSb, 4);

            // Assert
            Assert.AreEqual("8+(5×3", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void DeleteRightSequence_WithFormula2_ReturnsLeftSequence()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8+(5×3.5÷2)");

            // Act
            StringBuilder lSb = lPemdas.Chunk.SB;
            lPemdas.DeleteRightSequence(ref lSb, 4);

            // Assert
            Assert.AreEqual("8+(5×3.5", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void DeleteLeftSequence_WithFormula_ReturnsLeftSequence()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8+(5×3÷2)");

            // Act
            StringBuilder lSb = lPemdas.Chunk.SB;
            int lIndex = 4;
            lPemdas.DeleteLeftSequence(ref lSb, ref lIndex);

            // Assert
            Assert.AreEqual("5×3÷2)", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void DeleteLeftSequence_WithFormula2_ReturnsLeftSequence()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8+(5.62×3.5÷2)");

            // Act
            StringBuilder lSb = lPemdas.Chunk.SB;
            int lIndex = 4;
            lPemdas.DeleteLeftSequence(ref lSb, ref lIndex);

            // Assert
            Assert.AreEqual("5.62×3.5÷2)", lPemdas.Chunk.SB.ToString());
        }


        #endregion

        #region Multiplication

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithMultiply_ReturnsMultiplicationChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8×5+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '×', '÷' });

            // Assert
            Assert.AreEqual("8×5", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithMultiply2_ReturnsMultiplicationChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6+2-8×5+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '×', '÷' });

            // Assert
            Assert.AreEqual("8×5", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithMultiplyFirstAndDivision_ReturnsMultiplicationChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8×5+3÷2+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '×', '÷' });

            // Assert
            Assert.AreEqual("8×5", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithMultiplyFirstAndDivision2_ReturnsMultiplicationChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6+2-8×5+3÷2+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '×', '÷' });

            // Assert
            Assert.AreEqual("8×5", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithMultiplicationFirstAndDivisionAndParenthesis_ReturnsMultiplicationChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6+2-(3×2÷5)+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '×', '÷' });

            // Assert
            Assert.AreEqual("3×2", lPemdas.Chunk.SB.ToString());
        }


        [TestMethod]
        public void ComputeMultiplicationOrDivision_MultiplicationWithSigned_ReturnsMultiplicationChunk()
        {
            Assert.Inconclusive();
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6+2-(-3×2÷5)+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '×', '÷' });

            // Assert
            Assert.AreEqual("-3×2", lPemdas.Chunk.SB.ToString());
        }

        #endregion

        #region Division

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithDivide_ReturnsDivideChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8÷5+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '×', '÷' });

            // Assert
            Assert.AreEqual("8÷5", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithDivide2_ReturnsDivideChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6+2-8÷5+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '×', '÷' });

            // Assert
            Assert.AreEqual("8÷5", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithDivideFirstAndMultiplication_ReturnsDivisionChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8÷5+3×2+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '×', '÷' });

            // Assert
            Assert.AreEqual("8÷5", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithDivideFirstAndMultiplication2_ReturnsDivisionChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6+2-8÷5+3×2+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '×', '÷' });

            // Assert
            Assert.AreEqual("8÷5", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_WithDivisionFirstAndMultiplicationAndParenthesis_ReturnsDivisionChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6+2-(3÷2×5)+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '×', '÷' });

            // Assert
            Assert.AreEqual("3÷2", lPemdas.Chunk.SB.ToString());
        }

        #endregion

        #region Index And Length

        [TestMethod]
        public void ComputeMultiplicationOrDivision_PutDivisionInParenthesis_ReturnsStartIndexAndLengthDivisionChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6+2-(3÷2×5)+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '×', '÷' });

            // Assert
            Assert.AreEqual(5, lPemdas.Chunk.StartIndex);
            Assert.AreEqual(3, lPemdas.Chunk.Length);
        }

        [TestMethod]
        public void ComputeMultiplicationOrDivision_PutMultiplicationInParenthesis_ReturnsStartIndexAndLengthMultiplicationChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6+2-(3×2÷5)+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '×', '÷' });

            // Assert
            Assert.AreEqual(5, lPemdas.Chunk.StartIndex);
            Assert.AreEqual(3, lPemdas.Chunk.Length);
        }

        #endregion

        #endregion

        #region Tests on Addition or Substraction  

        [TestMethod]
        public void ComputeAdditionOrSubstraction_WithAddition_ReturnsAdditionChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8+5×(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '+', '-' });

            // Assert
            Assert.AreEqual("8+5", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeAdditionOrSubstraction_WithAddition2_ReturnsAdditionChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6×2÷8+5+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '+', '-' });

            // Assert
            Assert.AreEqual("8+5", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeAdditionOrSubstraction_WithSubstraction_ReturnsSubstractChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8-5+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '+', '-' });

            // Assert
            Assert.AreEqual("8-5", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeAdditionOrSubstraction_WithSubstract2_ReturnsSubstractChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6×2÷8-5+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '+', '-' });

            // Assert
            Assert.AreEqual("8-5", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeAdditionOrSubstraction_WithAdditionFirstAndSubstraction_ReturnsAdditionChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8+5-3÷2+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '+', '-' });

            // Assert
            Assert.AreEqual("8+5", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeAdditionOrSubstraction_WithAdditionAndSubstraction2_ReturnsAdditionChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6×2÷8+5-3÷2+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '+', '-' });

            // Assert
            Assert.AreEqual("8+5", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeAdditionOrSubstraction_WithSubstractionFirstAndAddition_ReturnsSubstractionChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("8-5×3+2×(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '+', '-' });

            // Assert
            Assert.AreEqual("8-5", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeAdditionOrSubstraction_WithSubstractionFirstAndAddition2_ReturnsSubstractionChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6×2-8+5+3×2+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '+', '-' });

            // Assert
            Assert.AreEqual("2-8", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeAdditionOrSubstraction_WithAdditionFirstAndSubstractionAndParenthesis_ReturnsAdditionChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6×2÷(3+2-5)+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '+', '-' });

            // Assert
            Assert.AreEqual("3+2", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeAdditionOrSubstraction_WithSubstractionFirstAndAdditionAndParenthesis_ReturnsSubstractionChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6×2÷(3-2+5)+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '+', '-' });

            // Assert
            Assert.AreEqual("3-2", lPemdas.Chunk.SB.ToString());
        }

        [TestMethod]
        public void ComputeAdditionOrSubstraction_PutSubstractionInParenthesis_ReturnsStartIndexAndLengthSubstractionChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6×2÷(3-2+5)+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '+', '-' });

            // Assert
            Assert.AreEqual(5, lPemdas.Chunk.StartIndex);
            Assert.AreEqual(3, lPemdas.Chunk.Length);
        }

        [TestMethod]
        public void ComputeAdditionOrSubstraction_PutAdditionInParenthesis_ReturnsStartIndexAndLengthAdditionChunk()
        {
            // Arrange
            PEMDAS lPemdas = new PEMDAS("6×2÷(3+2-5)+(5^(10))");

            // Act
            lPemdas.ComputeOperand(new char[] { '+', '-' });

            // Assert
            Assert.AreEqual(5, lPemdas.Chunk.StartIndex);
            Assert.AreEqual(3, lPemdas.Chunk.Length);
        }

        #endregion

        #region Test ExtractOperands

        [TestMethod]
        public void ExtractOperands_SetSimpleMultiplicationFormula_ReturnsOperands()
        {
            PEMDAS lPemdas = new PEMDAS("(40.56×15)");

            lPemdas.ExtractOperands(out decimal a, out decimal b);

            Assert.AreEqual(40.56m, a);
            Assert.AreEqual(15, b);
        }

        [TestMethod]
        public void ExtractOperands_SetSimpleMultiplicationFormula2_ReturnsOperands()
        {
            PEMDAS lPemdas = new PEMDAS("(40,56×15)");

            lPemdas.ExtractOperands(out decimal a, out decimal b);

            Assert.AreEqual(40.56m, a);
            Assert.AreEqual(15, b);
        }

        [TestMethod]
        public void ExtractOperands_SetSimpleDivisionFormula_ReturnsOperands()
        {
            PEMDAS lPemdas = new PEMDAS("(40÷15)");

            lPemdas.ExtractOperands(out decimal a, out decimal b);

            Assert.AreEqual(40, a);
            Assert.AreEqual(15, b);
        }

        #endregion

        #region  GetNumberFromChunk

        [TestMethod]
        public void GetNumberFromChunk_SetSimpleFormula_ReturnsLeftOperand()
        {
            PEMDAS lPemdas = new PEMDAS("16.5+56");

            string lChunk = string.Empty;

            lPemdas.GetNumberFromChunk(ref lChunk);

            Assert.AreEqual("16.5", lChunk);
        }

        [TestMethod]
        public void GetNumberFromChunk_SetSimpleFormula2_ReturnsRightOperand()
        {
            PEMDAS lPemdas = new PEMDAS("16.5+56");

            string lChunk = string.Empty;

            lPemdas.GetNumberFromChunk(ref lChunk, 5);

            Assert.AreEqual("56", lChunk);
        }

        #endregion

        #region  GetIndexOperator

        [TestMethod]
        public void GetIndexOperator_WithAddition_ReturnsIndex()
        {
            PEMDAS lPemdas = new PEMDAS("16.5+56");

            string lChunk = string.Empty;

            int lIndex = lPemdas.GetIndexOperator();

            Assert.AreEqual(4, lIndex);
        }

        [TestMethod]
        public void GetIndexOperator_WithSubstraction_ReturnsIndex()
        {
            PEMDAS lPemdas = new PEMDAS("16.5-56");

            string lChunk = string.Empty;

            int lIndex = lPemdas.GetIndexOperator();

            Assert.AreEqual(4, lIndex);
        }

        [TestMethod]
        public void GetIndexOperator_WithMultiplication_ReturnsIndex()
        {
            PEMDAS lPemdas = new PEMDAS("16.5×56");

            string lChunk = string.Empty;

            int lIndex = lPemdas.GetIndexOperator();

            Assert.AreEqual(4, lIndex);
        }

        [TestMethod]
        public void GetIndexOperator_WitDivision_ReturnsIndex()
        {
            PEMDAS lPemdas = new PEMDAS("16.5÷56");

            string lChunk = string.Empty;

            int lIndex = lPemdas.GetIndexOperator();

            Assert.AreEqual(4, lIndex);
        }

        #endregion

        #region DoCompute

        #region Addition

        [TestMethod]
        public void DoCompute_WithAddition_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("15+16");
            lPemdas.Operator = Operator.Addition;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("31", lResult.ToString());
        }

        [TestMethod]
        public void DoCompute_WithAddition2_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("15.67+16.20");
            lPemdas.Operator = Operator.Addition;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("31,87", lResult.ToString());
        }

        [TestMethod]
        public void DoCompute_AdditionWithNegativeNumberBeforeOperator_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("─15+16");
            lPemdas.Operator = Operator.Addition;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("1", lResult.ToString());
        }

        /// <summary>
        /// Negative sign alt + 196 (Extended ASCII)
        /// </summary>
        [TestMethod]
        public void DoCompute_AdditionWithNegativeNumberAfterOperator_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("15+─16");
            lPemdas.Operator = Operator.Addition;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("-1", lResult.ToString());
        }

        [TestMethod]
        public void DoCompute_WithAddition3_ReturnsException()
        {
            try
            {
                PEMDAS lPemdas = new PEMDAS("0+999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999");
                lPemdas.Operator = Operator.Addition;

                lPemdas.DoCompute(out decimal lResult);
            }
            catch (Exception e)
            {
                Assert.AreEqual("La valeur était trop grande ou trop petite pour un Decimal.", e.Message);
                return;
            }

            Assert.Fail("Exception was raised but not catched");
        }

        #endregion

        #region Substraction

        [TestMethod]
        public void DoCompute_WithSubstraction_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("15-16");
            lPemdas.Operator = Operator.Substraction;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("-1", lResult.ToString());
        }

        [TestMethod]
        public void DoCompute_WithSubstraction2_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("15.67-16.20");
            lPemdas.Operator = Operator.Substraction;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("-0,53", lResult.ToString());
        }

        [TestMethod]
        public void DoCompute_WithSubstraction3_ReturnsException()
        {
            try
            {
                PEMDAS lPemdas = new PEMDAS("0-999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999");
                lPemdas.Operator = Operator.Substraction;

                lPemdas.DoCompute(out decimal lResult);
            }
            catch (Exception e)
            {
                Assert.AreEqual("La valeur était trop grande ou trop petite pour un Decimal.", e.Message);
                return;
            }

            Assert.Fail("Exception was raised but not catched");
        }

        [TestMethod]
        public void DoCompute_SubstactionWithNegativeNumberBeforeOperator_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("─15-16");
            lPemdas.Operator = Operator.Addition;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("-31", lResult.ToString());
        }

        [TestMethod]
        public void DoCompute_SubstactionWithNegativeNumberAfterOperator_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("─15-─16");
            lPemdas.Operator = Operator.Addition;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("1", lResult.ToString());
        }

        #endregion

        #region Multiplication

        [TestMethod]
        public void DoCompute_WithMultiplication_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("15×16");
            lPemdas.Operator = Operator.Multiplication;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("240", lResult.ToString());
        }

        [TestMethod]
        public void DoCompute_WithMultiplication2_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("15.67×16.20");
            lPemdas.Operator = Operator.Multiplication;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("253,8540", lResult.ToString());
        }

        [TestMethod]
        public void DoCompute_WithMultiplication3_ReturnsException()
        {
            try
            {
                PEMDAS lPemdas = new PEMDAS("156700000000000000000000000000000000000000000000000000000000000000×1600000000000000000000000000000000000000000000000000000000000000020");
                lPemdas.Operator = Operator.Multiplication;

                lPemdas.DoCompute(out decimal lResult);
            }
            catch (Exception e)
            {
                Assert.AreEqual("La valeur était trop grande ou trop petite pour un Decimal.", e.Message);
                return;
            }

            Assert.Fail("Exception was raised but not catched");
        }

        /// <summary>
        /// Multiplication sign alt + 158
        /// </summary>
        [TestMethod]
        public void DoCompute_MultiplicationWithNegativeNumberBeforeOperator_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("─15×16");
            lPemdas.Operator = Operator.Addition;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("-240", lResult.ToString());
        }

        [TestMethod]
        public void DoCompute_MultiplicationWithNegativeNumberAfterOperator_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("─15×─16");
            lPemdas.Operator = Operator.Addition;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("240", lResult.ToString());
        }

        #endregion

        #region Division

        [TestMethod]
        public void DoCompute_WithDivision_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("15÷16");
            lPemdas.Operator = Operator.Division;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("0,9375", lResult.ToString());
        }

        [TestMethod]
        public void DoCompute_WithDivision2_ReturnsException()
        {
            PEMDAS lPemdas = new PEMDAS("15.67÷16.20");
            lPemdas.Operator = Operator.Division;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("0,9672839506172839506172839506", lResult.ToString());
        }

        [TestMethod]
        public void DoCompute_WithDivision3_ReturnsException()
        {
            try
            {
                PEMDAS lPemdas = new PEMDAS("15.67÷0");
                lPemdas.Operator = Operator.Division;

                lPemdas.DoCompute(out decimal lResult);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Tentative de division par zéro.", e.Message);
                return;
            }

            Assert.Fail("Exception was raised but not catched");
        }

        [TestMethod]
        public void DoCompute_WithDivision4_ReturnsException()
        {
            PEMDAS lPemdas = new PEMDAS("1÷0.99999999999999999999999999999999999999999999999999999999999999999999999999999");
            lPemdas.Operator = Operator.Division;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("1,000000000000000000000000001", lResult.ToString());
        }

        /// <summary>
        /// Division alt + 246
        /// </summary>
        [TestMethod]
        public void DoCompute_DivisionWithNegativeNumberBeforeOperator_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("─15÷16");
            lPemdas.Operator = Operator.Addition;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("-0,9375", lResult.ToString());
        }

        [TestMethod]
        public void DoCompute_DivisionWithNegativeNumberAfterOperator_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("15÷─16");
            lPemdas.Operator = Operator.Addition;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("-0,9375", lResult.ToString());
        }

        #endregion

        #region Exponent

        [TestMethod]
        public void DoCompute_WithExponent_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("15^2");
            lPemdas.Operator = Operator.Exponent;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("225", lResult.ToString());
        }

        [TestMethod]
        public void DoCompute_WithExponent2_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("15^16");
            lPemdas.Operator = Operator.Exponent;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("6568408355712890000", lResult.ToString());
        }

        [TestMethod]
        public void DoCompute_WithExponent3_ReturnsException()
        {
            try
            {
                PEMDAS lPemdas = new PEMDAS("150^1000");
                lPemdas.Operator = Operator.Exponent;

                lPemdas.DoCompute(out decimal lResult);
            }
            catch (Exception e)
            {
                Assert.AreEqual("La valeur était trop grande ou trop petite pour un Decimal.", e.Message);
                return;
            }

            Assert.Fail("Exception was raised but not catched");
        }

        [TestMethod]
        public void DoCompute_ExponentWithNegativeNumberBeforeOperator_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("─5^2");
            lPemdas.Operator = Operator.Addition;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("25", lResult.ToString());
        }

        [TestMethod]
        public void DoCompute_ExponentWithNegativeNumberAfterOperator_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("5^─2");
            lPemdas.Operator = Operator.Addition;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("0,04", lResult.ToString());
        }

        #endregion

        #region Square

        [TestMethod]
        public void DoCompute_WithRootSquare_ReturnsResult()
        {
            Assert.Inconclusive();
            PEMDAS lPemdas = new PEMDAS("√81");
            lPemdas.Operator = Operator.Square;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("225", lResult.ToString());
        }

        [TestMethod]
        public void DoCompute_WithRootSquare2_ReturnsResult()
        {
            Assert.Inconclusive();
            PEMDAS lPemdas = new PEMDAS("√15");
            lPemdas.Operator = Operator.Square;

            lPemdas.DoCompute(out decimal lResult);

            Assert.AreEqual("6568408355712890000", lResult.ToString());
        }

        [TestMethod]
        public void DoCompute_WithRootSquare3_ReturnsException()
        {
            try
            {
                PEMDAS lPemdas = new PEMDAS("√─1");
                lPemdas.Operator = Operator.Square;

                lPemdas.DoCompute(out decimal lResult);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Le format de la chaîne d'entrée est incorrect.", e.Message);
                return;
            }

            Assert.Fail("Exception was raised but not catched");
        }

        #endregion

        #endregion

        #region  TrimLengthString

        [TestMethod]
        public void TrimLengthString_WithNormalString_ReturnsString()
        {
            string lStr = "99999999999999999999999999999";
            PEMDAS lPemdas = new PEMDAS(lStr);

            lStr = lPemdas.TrimLengthString(lStr);

            // 29 digits max
            Assert.AreEqual("99999999999999999999999999999", lStr);
        }

        [TestMethod]
        public void TrimLengthString_WithStringTooLong_ReturnsTrimedString()
        {
            string lStr = "99999999999999999999999999999111111";
            PEMDAS lPemdas = new PEMDAS(lStr);

            lStr = lPemdas.TrimLengthString(lStr);

            // 29 digits max
            Assert.AreEqual("99999999999999999999999999999", lStr);
        }

        [TestMethod]
        public void TrimLengthString_WithComaNormalString_ReturnsString()
        {
            string lStr = "1.999999999999999999999999999";
            PEMDAS lPemdas = new PEMDAS(lStr);

            lStr = lPemdas.TrimLengthString(lStr);

            // 29 digits max
            Assert.AreEqual("1.999999999999999999999999999", lStr);
        }

        [TestMethod]
        public void TrimLengthString_WithComaTooLongString_ReturnsTrimedString()
        {
            string lStr = "1.9999999999999999999999999991111111";
            PEMDAS lPemdas = new PEMDAS(lStr);

            lStr = lPemdas.TrimLengthString(lStr);

            // 29 digits max
            Assert.AreEqual("1.999999999999999999999999999", lStr);
        }

        #endregion

        #region  GetOperator

        [TestMethod]
        public void GetOperator_ChunkWithExponent_ReturnsExponent()
        {
            string lStr = "5^12";
            PEMDAS lPemdas = new PEMDAS(lStr);

            lPemdas.GetOperator();

            Assert.AreEqual(Operator.Exponent, lPemdas.Operator);
        }

        [TestMethod]
        public void GetOperator_ChunkWithMultiplication_ReturnsMultiplication()
        {
            string lStr = "5×12";
            PEMDAS lPemdas = new PEMDAS(lStr);

            lPemdas.GetOperator();

            Assert.AreEqual(Operator.Multiplication, lPemdas.Operator);
        }

        [TestMethod]
        public void GetOperator_ChunkWithDivision_ReturnsDivision()
        {
            string lStr = "5÷12";
            PEMDAS lPemdas = new PEMDAS(lStr);

            lPemdas.GetOperator();

            Assert.AreEqual(Operator.Division, lPemdas.Operator);
        }

        [TestMethod]
        public void GetOperator_ChunkWithAddition_ReturnsAddition()
        {
            string lStr = "5+12";
            PEMDAS lPemdas = new PEMDAS(lStr);

            lPemdas.GetOperator();

            Assert.AreEqual(Operator.Addition, lPemdas.Operator);
        }

        [TestMethod]
        public void GetOperator_ChunkWithSubstraction_ReturnsSubstraction()
        {
            string lStr = "5-12";
            PEMDAS lPemdas = new PEMDAS(lStr);

            lPemdas.GetOperator();

            Assert.AreEqual(Operator.Substraction, lPemdas.Operator);
        }

        [TestMethod]
        public void GetOperator_ChunkWithRootSquare_ReturnsSquare()
        {
            string lStr = "√12";
            PEMDAS lPemdas = new PEMDAS(lStr);

            lPemdas.GetOperator();

            Assert.AreEqual(Operator.Square, lPemdas.Operator);
        }

        #endregion

        #region DoReplaceByResult

        [TestMethod]
        public void DoReplaceByResult_SimplyOperation_ReturnsNewFormulaWithResult()
        {
            PEMDAS lPemdas = new PEMDAS("5×7");

            lPemdas.DoReplaceByResult(35m);

            Assert.AreEqual("35", lPemdas.Chunk.SB.ToString());
        }

        #endregion

        #region ComputeFormula

        #region Addition

        [TestMethod]
        public void ComputeFormula_SimpleAdditionFormula_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("5+7");
            var lResult = lPemdas.ComputeFormula();

            Assert.AreEqual("12", lResult);
        }

        [TestMethod]
        public void ComputeFormula_MaxIntegerAdditionFormula_ReturnsResult()
        {
            string lBig1 = Int64.MaxValue.ToString();
            string lBig2 = Int64.MaxValue.ToString();
            PEMDAS lPemdas = new PEMDAS($"{lBig1}+{lBig2}");
            var lResult = lPemdas.ComputeFormula();

            Assert.AreEqual("18446744073709551614", lResult);
        }

        [TestMethod]
        public void ComputeFormula_MaxDecimalAdditionFormula_ReturnsException()
        {
            try
            {
                string lBig = Decimal.MaxValue.ToString();

                PEMDAS lPemdas = new PEMDAS($"{lBig}+{lBig}");
                var lResult = lPemdas.ComputeFormula();

            }
            catch (Exception e)
            {
                Assert.AreEqual("La valeur était trop grande ou trop petite pour un Decimal.", e.Message);
                return;
            }

            Assert.Fail("Exception was raised but not catched");
        }

        [TestMethod]
        public void ComputeFormula_SimpleAdditionFormulaWithComa_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("5.5+7.15");
            var lResult = lPemdas.ComputeFormula();

            Assert.AreEqual("12,65", lResult);
        }

        [TestMethod]
        public void ComputeFormula_SimpleFloatAdditionFormula_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("5.591112+7.158165555");
            var lResult = lPemdas.ComputeFormula();

            Assert.AreEqual("12,749277555", lResult);
        }

        [TestMethod]
        public void ComputeFormula_MaxFloatAdditionFormula_ReturnsResult()
        {
            Assert.Inconclusive();
            // Problème avec la traduction de la notation scientifique dans la chaine de caracteres E+...
            try
            {
                string lBig = float.MaxValue.ToString();

                PEMDAS lPemdas = new PEMDAS($"{lBig}+{lBig}");
                var lResult = lPemdas.ComputeFormula();
            }
            catch (Exception e)
            {
                Assert.AreEqual("La valeur était trop grande ou trop petite pour un Decimal.", e.Message);
                return;
            }

            Assert.Fail("Exception was raised but not catched");
        }

        #endregion

        #region Substraction

        [TestMethod]
        public void ComputeFormula_SimpleSubstractionFormula_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("7-5");
            var lResult = lPemdas.ComputeFormula();

            Assert.AreEqual("2", lResult);
        }

        [TestMethod]
        public void ComputeFormula_SimpleFloatSubstraction_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("7.51-5.6333333");
            var lResult = lPemdas.ComputeFormula();

            Assert.AreEqual("1,8766667", lResult);
        }

        [TestMethod]
        public void ComputeFormula_MaxFloatSubstractionFormula_ReturnsResult()
        {
            Assert.Inconclusive();
            // Problème avec la traduction de la notation scientifique dans la chaine de caracteres E+...

            string lBig = float.MaxValue.ToString();

            PEMDAS lPemdas = new PEMDAS($"{lBig}-{lBig}");
            var lResult = lPemdas.ComputeFormula();

            Assert.AreEqual("0", lResult);
        }

        #endregion

        #region Division

        [TestMethod]
        public void ComputeFormula_SimpleDivisionFormula_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("8÷2");
            var lResult = lPemdas.ComputeFormula();

            Assert.AreEqual("4", lResult);
        }

        [TestMethod]
        public void ComputeFormula_DivisionByZeroFormula_ReturnsException()
        {
            try
            {
                PEMDAS lPemdas = new PEMDAS("8÷0");
                var lResult = lPemdas.ComputeFormula();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Tentative de division par zéro.", e.Message);
                return;
            }

            Assert.Fail("Exception was raised but not catched");
        }

        [TestMethod]
        public void ComputeFormula_FloatDivisionFormula_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("8.56÷37.2110");
            var lResult = lPemdas.ComputeFormula();

            Assert.AreEqual("0,2300395044476095778130122813", lResult.ToString());
        }

        [TestMethod]
        public void ComputeFormula_DivisionByDecimalMaxValueFormula_ReturnsException()
        {
            Assert.Inconclusive();
            string lBig = Decimal.MaxValue.ToString();

            PEMDAS lPemdas = new PEMDAS($"1÷{lBig}");
            var lResult = lPemdas.ComputeFormula();

            Assert.AreEqual("1,2621774483536342087562403992417e-28", lResult);
        }

        #endregion

        #region Multiplication

        [TestMethod]
        public void ComputeFormula_SimpleMultiplicationFormula_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("7×5");
            var lResult = lPemdas.ComputeFormula();

            Assert.AreEqual("35", lResult);
        }

        [TestMethod]
        public void ComputeFormula_MultiplicationFormula_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("7×5");
            var lResult = lPemdas.ComputeFormula();

            Assert.AreEqual("35", lResult);
        }

        #endregion

        #region Exponent

        [TestMethod]
        public void ComputeFormula_SimpleExponentFormula_ReturnsResult()
        {
            PEMDAS lPemdas = new PEMDAS("8^2");
            var lResult = lPemdas.ComputeFormula();

            Assert.AreEqual("64", lResult);
        }

        #endregion

        #region Root Square

        [TestMethod]
        public void ComputeFormula_SimpleRootSquareFormula_ReturnsResult()
        {
            Assert.Inconclusive();
            PEMDAS lPemdas = new PEMDAS("√64");
            var lResult = lPemdas.ComputeFormula();

            Assert.AreEqual("8", lResult);
        }

        #endregion

        #endregion
    }
}
