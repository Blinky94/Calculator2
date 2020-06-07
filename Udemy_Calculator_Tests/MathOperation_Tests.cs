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

        [TestMethod]
        public void Add_Twodouble_ReturnsResult()
        {
            // Arrange
            double p1 = 55d;
            double p2 = 13.5d;

            // Act
            string lResult = MathOperation.Add(p1, p2);

            // Assert
            Assert.AreEqual("68,5", lResult);
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

        [TestMethod]
        public void Substract_Twodouble_ReturnsResult()
        {
            // Arrange
            double p1 = 50d;
            double p2 = 15.5d;

            // Act
            string lResult = MathOperation.Substract(p1, p2);

            // Assert
            Assert.AreEqual("34,5", lResult);
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

        [TestMethod]
        public void Multiply_Twodouble_ReturnsResult()
        {
            // Arrange
            double p1 = 10;
            double p2 = 2;

            // Act
            string lResult = MathOperation.Multiply(p1, p2);

            // Assert
            Assert.AreEqual("20", lResult);
        }

        #endregion

        #region Divide

        [TestMethod]
        public void Divide_Twodecimal_ReturnsResult()
        {
            // Arrange
            decimal p1 = 10m;
            decimal p2 = 2m;

            // Act
            decimal lResult = MathOperation.Divide(p1, p2);

            // Assert
            Assert.AreEqual(5m, lResult);
        }

        [TestMethod]
        public void Divide_Twodouble_ReturnsResult()
        {
            // Arrange
            double p1 = 10d;
            double p2 = 2d;

            // Act
            string lResult = MathOperation.Divide(p1, p2);

            // Assert
            Assert.AreEqual("5", lResult);
        }

        [TestMethod]
        public void Divide_ByZeroDecimal_ReturnsDivideByZeroException()
        {
            // Arrange
            decimal p1 = 10m;
            decimal p2 = 0m;

            try
            {
                // Act
                MathOperation.Divide(p1, p2);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual("Tentative de division par zéro.", e.Message);
            }
        }

        [TestMethod]
        public void Divide_ByZeroDouble_ReturnsDivideByZeroException()
        {
            // Arrange
            double p1 = 10d;
            double p2 = 0d;

            try
            {
                // Act
                MathOperation.Divide(p1, p2);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual("Tentative de division par zéro.", e.Message);
            }
        }

        #endregion

        #region Exponent

        [TestMethod]
        public void Exponent_WithTwoDecimalNumber_ReturnsResult()
        {
            // Arrange
            decimal p1 = 8;
            decimal p2 = 5;

            // Act
            decimal lResult = MathOperation.Exponent(p1, p2);

            // Assert
            Assert.AreEqual(32768, lResult);
        }

        [TestMethod]
        public void Exponent_WithTwoDoubleNumber_ReturnsResult()
        {
            // Arrange
            double p1 = 8;
            double p2 = 5;

            // Act
            string lResult = MathOperation.Exponent(p1, p2);

            // Assert
            Assert.AreEqual("32768", lResult);
        }

        #endregion

        #region Sqrt

        [TestMethod]
        public void Sqrt_WithDecimalNumber_ReturnsResult()
        {
            // Arrange
            decimal p = 16;

            // Act
            decimal lResult = MathOperation.Sqrt(p);

            // Assert
            Assert.AreEqual(4, lResult);
        }

        [TestMethod]
        public void Sqrt_WithDoubleNumber_ReturnsResult()
        {
            // Arrange
            double p = 16;

            // Act
            string lResult = MathOperation.Sqrt(p);

            // Assert
            Assert.AreEqual("4", lResult);
        }

        #endregion
    }
}
