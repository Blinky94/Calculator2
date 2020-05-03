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
        public void Add_TwoDouble_ReturnsResult()
        {
            // Arrange
            double p1 = 55;
            double p2 = 13.5;

            // Act
            double lResult = MathOperation.Add(p1, p2);

            // Assert
            Assert.AreEqual(68.5, lResult);
        }

        [TestMethod]
        public void Add_InfinityNumber_ReturnsException()
        {
            // Arrange
            double p1 = double.PositiveInfinity;
            double p2 = 1;

            try
            {
                // Act
                MathOperation.Add(p1, p2);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual("The Add operation failed !!! ", e.Message);
            }
        }

        [TestMethod]
        public void Add_NaN_ReturnsException()
        {
            // Arrange
            double p1 = 16;
            var p2 = double.NaN;

            try
            {
                // Act
                MathOperation.Add(p1, p2);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual("The Add operation failed !!! ", e.Message);
            }
        }

        #endregion

        #region Substract

        [TestMethod]
        public void Substract_TwoDouble_ReturnsResult()
        {
            // Arrange
            double p1 = 50;
            double p2 = 15.5;

            // Act
            double lResult = MathOperation.Substract(p1, p2);

            // Assert
            Assert.AreEqual(34.5, lResult);
        }

        [TestMethod]
        public void Substract_InfinityNumber_ReturnsException()
        {
            // Arrange
            double p1 = 5;
            double p2 = double.NegativeInfinity;

            try
            {
                // Act
                MathOperation.Substract(p1, p2);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual("The Substract operation failed !!! ", e.Message);
            }
        }

        [TestMethod]
        public void Substract_NaN_ReturnsException()
        {
            // Arrange
            double p1 = 12;
            var p2 = double.NaN;

            try
            {
                // Act
                MathOperation.Substract(p1, p2);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual("The Substract operation failed !!! ", e.Message);
            }
        }

        #endregion

        #region Multiply

        [TestMethod]
        public void Multiply_TwoDouble_ReturnsResult()
        {
            // Arrange
            double p1 = 10;
            double p2 = 2;

            // Act
            double lResult = MathOperation.Multiply(p1, p2);

            // Assert
            Assert.AreEqual(20, lResult);
        }

        [TestMethod]
        public void Multiply_InfinityNumber_ReturnsException()
        {
            // Arrange
            double p1 = 5;
            double p2 = double.PositiveInfinity;

            try
            {
                // Act
                MathOperation.Multiply(p1, p2);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual("The Multiply operation failed !!! ", e.Message);
            }
        }

        [TestMethod]
        public void Multiply_NaN_ReturnsException()
        {
            // Arrange
            double p1 = 12;
            var p2 = double.NaN;

            try
            {
                // Act
                MathOperation.Multiply(p1, p2);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual("The Multiply operation failed !!! ", e.Message);
            }
        }

        #endregion

        #region Divide

        [TestMethod]
        public void Divide_TwoDouble_ReturnsResult()
        {
            // Arrange
            double p1 = 10;
            double p2 = 2;

            // Act
            double lResult = MathOperation.Divide(p1, p2);

            // Assert
            Assert.AreEqual(5, lResult);
        }

        [TestMethod]
        public void Divide_ByZero_ReturnsDivideByZeroException()
        {
            // Arrange
            double p1 = 10;
            double p2 = 0;

            try
            {
                // Act
                MathOperation.Divide(p1, p2);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual("The Divide operation failed !!! ", e.Message);
            }
        }

        [TestMethod]
        public void Divide_ByInfinity_ReturnsException()
        {
            // Arrange
            double p1 = 10;
            double p2 = double.PositiveInfinity;

            try
            {
                // Act
                MathOperation.Divide(p1, p2);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual("The Divide operation failed !!! ", e.Message);
            }
        }

        [TestMethod]
        public void Divide_ByNaN_ReturnsException()
        {
            // Arrange
            double p1 = 10;
            double p2 = double.NaN;

            try
            {
                // Act
                MathOperation.Divide(p1, p2);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual("The Divide operation failed !!! ", e.Message);
            }
        }

        #endregion

        #region Exponent

        [TestMethod]
        public void Exponent_WithTwoNumber_ReturnsResult()
        {
            // Arrange
            double p1 = 8;
            double p2 = 5;

            // Act
            double lResult = MathOperation.Exponent(p1, p2);

            // Assert
            Assert.AreEqual(32768, lResult);
        }

        [TestMethod]
        public void Exponent_PositiveInfinity_ReturnsException()
        {
            // Arrange
            double p1 = 8;
            double p2 = double.PositiveInfinity;

            try
            {
                // Act
                MathOperation.Exponent(p1, p2);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual("The Exponent operation failed !!! ", e.Message);
            }
        }

        [TestMethod]
        public void Exponent_NaN_ReturnsException()
        {
            // Arrange
            double p1 = 8;
            double p2 = double.NaN;

            try
            {
                // Act
                MathOperation.Exponent(p1, p2);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual("The Exponent operation failed !!! ", e.Message);
            }
        }

        #endregion

        #region Sqrt

        [TestMethod]
        public void Sqrt_WithNumber_ReturnsResult()
        {
            // Arrange
            double p = 16;

            // Act
            double lResult = MathOperation.Sqrt(p);

            // Assert
            Assert.AreEqual(4, lResult);
        }

        [TestMethod]
        public void Sqrt_PositiveInfinity_ReturnsException()
        {
            // Arrange
            double p = double.PositiveInfinity;

            try
            {
                // Act
                MathOperation.Sqrt(p);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual("The Sqrt operation failed !!! ", e.Message);
            }
        }

        [TestMethod]
        public void Sqrt_NaN_ReturnsException()
        {
            // Arrange
            double p = double.NaN;

            try
            {
                // Act
                MathOperation.Sqrt(p);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual("The Sqrt operation failed !!! ", e.Message);
            }
        }

        #endregion
    }
}
