using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Udemy_Calculator;

namespace Udemy_Calculator_Tests
{
    /// <summary>
    /// Summary description for MathOperation_Tests
    /// </summary>
    [TestClass]
    public class MathOperation_Tests
    {
        #region Add

        [TestMethod]
        public void Add_Twodecimal_ReturnsResult()
        {
            // Arrange
            decimal p1 = 55;
            decimal p2 = 13.5m;

            // Act
            decimal lResult = MathOperation.Add(p1, p2);

            // Assert
            Assert.AreEqual(68.5m, lResult);
        }

        #endregion

        #region Substract

        [TestMethod]
        public void Substract_Twodecimal_ReturnsResult()
        {
            // Arrange
            decimal p1 = 50;
            decimal p2 = 15.5m;

            // Act
            decimal lResult = MathOperation.Substract(p1, p2);

            // Assert
            Assert.AreEqual(34.5m, lResult);
        }

        #endregion

        #region Multiply

        [TestMethod]
        public void Multiply_Twodecimal_ReturnsResult()
        {
            // Arrange
            decimal p1 = 10;
            decimal p2 = 2;

            // Act
            decimal lResult = MathOperation.Multiply(p1, p2);

            // Assert
            Assert.AreEqual(20, lResult);
        }

        #endregion

        #region Divide

        [TestMethod]
        public void Divide_Twodecimal_ReturnsResult()
        {
            // Arrange
            decimal p1 = 10;
            decimal p2 = 2;

            // Act
            decimal lResult = MathOperation.Divide(p1, p2);

            // Assert
            Assert.AreEqual(5, lResult);
        }

        [TestMethod]
        public void Divide_ByZero_ReturnsDivideByZeroException()
        {
            // Arrange
            decimal p1 = 10;
            decimal p2 = 0;

            try
            {
                // Act
                MathOperation.Divide(p1, p2);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual("The division by zero is forbidden !!!", e.Message);
            }
        }

        #endregion

        #region Exponent

        [TestMethod]
        public void Exponent_WithTwoNumber_ReturnsResult()
        {
            // Arrange
            decimal p1 = 8;
            decimal p2 = 5;

            // Act
            decimal lResult = MathOperation.Exponent(p1, p2);

            // Assert
            Assert.AreEqual(32768, lResult);
        }

        #endregion

        #region Sqrt

        [TestMethod]
        public void Sqrt_WithNumber_ReturnsResult()
        {
            // Arrange
            decimal p = 16;

            // Act
            decimal lResult = MathOperation.Sqrt(p);

            // Assert
            Assert.AreEqual(4, lResult);
        }

        #endregion
    }
}
