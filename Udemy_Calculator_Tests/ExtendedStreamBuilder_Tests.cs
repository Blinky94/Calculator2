using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Udemy_Calculator;
using System.Linq;

namespace Udemy_Calculator_Tests
{
    /// <summary>
    /// Summary description for ExtendedStreamBuilder_Tests
    /// </summary>
    [TestClass]
    public class ExtendedStreamBuilder_Tests
    {
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        #region ContainsAny

        [TestMethod]
        public void ContainsAny_StringBuilderContainsAnyofChar_ReturnsTrue()
        {
            StringBuilder lSb = new StringBuilder("5*7+2^(3)");

            Assert.IsTrue(lSb.ContainsAny(new char[] { '*', '^' }));
        }

        [TestMethod]
        public void ContainsAny_StringBuilderNOTContainsAnyofChar_ReturnsFalse()
        {
            StringBuilder lSb = new StringBuilder("5*7+2^(3)");

            Assert.IsFalse(lSb.ContainsAny(new char[] { '/', 'y' }));
        }

        #endregion

        #region Contains

        [TestMethod]
        public void Contains_StringBuilderContainsChar_ReturnsTrue()
        {
            StringBuilder lSb = new StringBuilder("5*7+2^(3)");

            Assert.IsTrue(lSb.Contains('^'));
        }

        [TestMethod]
        public void Contains_StringBuilderNOTContainsChar_ReturnsFalse()
        {
            StringBuilder lSb = new StringBuilder("5*7+2^(3)");

            Assert.IsFalse(lSb.Contains('y'));
        }

        #endregion

        #region IndexOf

        // public static int IndexOf(this StringBuilder pSb, char pChar)

        [TestMethod]
        public void IndexOf_StringBuilderContainsOneChar_ReturnsIndexOfThisChar()
        {
            StringBuilder lSb = new StringBuilder("5*7+2^(3)");

            Assert.AreEqual(5, lSb.IndexOf('^'));
        }

        [TestMethod]
        public void IndexOf_StringBuilderContainsManySameChar_ReturnsIndexOfTheFirstChar()
        {
            StringBuilder lSb = new StringBuilder("5*7+2^(3)+5^(6^(2/3))");

            Assert.AreEqual(5, lSb.IndexOf('^'));
        }

        [TestMethod]
        public void IndexOf_StringBuilderNOTContainsChar_ReturnsNothing()
        {
            StringBuilder lSb = new StringBuilder("5*7+2^(3)+5^(6^(2/3))");

            Assert.AreEqual(-1, lSb.IndexOf('y'));
        }

        #endregion

        #region IndexOfAnyChar

        // public static int[] IndexOfAny(this StringBuilder pSb, char pChar)
        [TestMethod]
        public void IndexOfAnyChar_StringBuilderContainsChar_ReturnsTabIndex()
        {
            StringBuilder lSb = new StringBuilder("5*7+2^(3)+5^(6^(2/3))");

            int[] lResult = lSb.IndexOfAnyChar('^');
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] { 5, 11, 14 }, lResult));
        }

        #endregion

        #region IndexOfAnyString

        [TestMethod]
        public void IndexOfAnyString_StringBuilderContainsString_ReturnsTabIndex()
        {
            StringBuilder lSb = new StringBuilder("5*7+2^(3)+5^(6^(2/3))");

            int[] lResult = lSb.IndexOfAnyString("^(");
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] { 5, 11, 14 }, lResult));
        }

        [TestMethod]
        public void IndexOfAnyString_StringBuilderNOTContainsString_ReturnsNothing()
        {
            StringBuilder lSb = new StringBuilder("5*7+2^(3)+5^(6^(2/3))");

            int[] lResult = lSb.IndexOfAnyString("*(");
            Assert.IsTrue(lResult.Count() == 0);
        }

        #endregion

        #region GetChunk

        [TestMethod]
        public void GetChunk_EnterIndexAndLengthOfAnExistingSequence_ReturnsTheSequence()
        {
            StringBuilder lSb = new StringBuilder("5*7+2^(3)+5^(6^(2/3))");

            StringBuilder lSubSb = lSb.GetChunk(4, 5);

            Assert.AreEqual("2^(3)", lSubSb.ToString());
        }

        #endregion
    }
}
