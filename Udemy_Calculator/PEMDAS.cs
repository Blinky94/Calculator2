﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_Calculator
{
    /// <summary>
    /// Chunk of fomula
    /// </summary>
    public static class Chunk
    {
        public static StringBuilder SB { get; set; }
        public static int StartIndex { get; set; }
        public static int Length { get; set; }
    }

    public enum Operand { Multiplication, Division, Addition, Substraction, Square, Exponent }

    public class PEMDAS
    {
        #region Fields

        private string mFormula;
        internal StringBuilder mSb;
        private char[] mTabOperators;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pFormula"></param>
        public PEMDAS(string pFormula)
        {
            mFormula = pFormula;
            mTabOperators = new char[] { '(', ')', '^', '*', '/', '+', '-' };
            mSb = new StringBuilder(mFormula);           
        }

        /// <summary>
        /// Main function to compute formula input and returns out parameter result
        /// </summary>
        /// <param name="pResult"></param>
        public void ComputeFormula(out string pResult)
        {
            while (mSb.Contains(mTabOperators))
            {
                ComputeParenthesis();
                ComputeExponent();
                ComputeMultiplicationOrDivision();
                ComputeAdditionOrSubstraction();
            }

            pResult = mSb.ToString();
        }

        #region Parenthesis

        private Stack<int> mStackOfOpenedParenthesisIndex = new Stack<int>();

        internal bool IsRealOpenedParenthesis(int pIndex, string pStr)
        {
            if (pIndex > 0)
            {
                return pStr[pIndex] == '(' && (pStr[pIndex - 1] != '^');
            }
            else
            {
                return pStr[pIndex] == '(';
            }
        }

        internal char[] mChunkParenthesis;
        internal char[] mChunkExponent;

        /// <summary>
        /// Main method to compute all parenthesis in a formula
        /// </summary>
        internal void ComputeParenthesis()
        {
            string lCurrentChunk;

            if (mChunkExponent != null)
            {
                lCurrentChunk = mChunkExponent.ToString();
            }
            else
            {
                lCurrentChunk = mFormula;
            }

            if (ParenthesisAreEquivalent(lCurrentChunk))
            {
                for (int i = 0; i < mSb.Length; i++)
                {
                    if (IsRealOpenedParenthesis(i, lCurrentChunk)) // If '(' not preceed exponent symbol
                    {
                        if (!mStackOfOpenedParenthesisIndex.Contains(i)) // index not already in
                        {
                            mStackOfOpenedParenthesisIndex.Push(i); // Add index
                        }
                    }
                    else if (mSb[i] == ')')
                    {
                        if (mStackOfOpenedParenthesisIndex.Count() > 0)
                        {
                            int lOpIndex = mStackOfOpenedParenthesisIndex.Pop(); // Pop index
                            lOpIndex++; // Adjust the index number from 1 to ...

                            mChunkParenthesis = new char[i - lOpIndex];
                            mSb.CopyTo(lOpIndex, mChunkParenthesis, 0, i - lOpIndex);
                        }
                    }
                }
            }
            else
            {
                throw new ArithmeticException("The number of opened parenthesis and closed parenthesis are not the same !!!");
            }
        }

        /// <summary>
        /// Compare the number of '(' and ')'
        /// </summary>
        public bool ParenthesisAreEquivalent(string pFormula)
        {
            int lOpenedParenthesis = pFormula.Count(c => c == '(');
            int lClosedParenthesis = pFormula.Count(c => c == ')');

            return lOpenedParenthesis == lClosedParenthesis;
        }

        #endregion

        #region Exponent

        internal void ComputeExponent()
        {
            // compute chunk formula if not empty
            if (mChunkParenthesis.Count() > 0)
            {
                StringBuilder lSbChunkOfExponent = new StringBuilder(new string(mChunkParenthesis)); // Convert chunk array to streambuilder object

                // Number of parenthesis are equivalent ex : (5+6^(2)) or 6^(2)
                if (ParenthesisAreEquivalent(lSbChunkOfExponent.ToString()))
                {
                    // get chunk of exponent
                    GetChunkOfExponent(ref lSbChunkOfExponent);

                    // if parenthesis contains () or ^, or */, or +-, compute

                    // compute Math.pow(num, exponent)

                    // Replace result in mFomula                
                }
                else
                {
                    throw new ArithmeticException("The number of opened parenthesis and closed parenthesis are not the same !!!");
                }
            }
        }

        /// <summary>
        /// Delete everything after the '(' && ')' exponent, modify the startIndex parameter following the begining of the current exponent value
        /// </summary>
        /// <param name="pSbChunk"></param>
        /// <param name="pStartIndex"></param>
        internal void EraseLeftElementsFromExponent(ref StringBuilder pSbChunk, ref int pStartIndex)
        {
            // Get LEFT start index number from ^
            for (int i = pStartIndex - 1; i >= 0; i--)
            {
                if (!char.IsNumber(pSbChunk[i]))
                {
                    pSbChunk.Remove(0, i + 1);
                    pStartIndex -= i;
                    break;
                }
            }
        }

        /// <summary>
        /// Delete everything before the exponent decimal
        /// </summary>
        /// <param name="pSbChunk"></param>
        /// <param name="pStartIndex"></param>
        /// <returns>The ended index</returns>
        internal int EraseRightElementsFromExponent(ref StringBuilder pSbChunk, int pStartIndex)
        {
            Stack<int> lStackOpenedIndexParenthesis = new Stack<int>();
            int lClosedIndex = 0;

            // Get RIGHT from '(' to ')' index from ^
            for (int i = pStartIndex; i < pSbChunk.Length; i++)
            {
                if (pSbChunk[i] == '(')
                {
                    lStackOpenedIndexParenthesis.Push(i);
                }
                else if (pSbChunk[i] == ')')
                {
                    if (lStackOpenedIndexParenthesis.Count() > 0)
                    {
                        lClosedIndex = lStackOpenedIndexParenthesis.Pop();
                        if (lStackOpenedIndexParenthesis.Count() == 0)
                        {
                            pSbChunk.Remove(i++, pSbChunk.Length - i++);
                            mChunkExponent = pSbChunk.ToString().ToCharArray();
                            break;
                        }
                    }
                }
            }

            return lClosedIndex;
        }

        internal int IndexOfFirstSymbol(StringBuilder pSbChunk, char pSymb)
        {
            for (int i = 0; i < pSbChunk.Length; i++)
            {
                if (pSbChunk[i] == pSymb)
                {
                    return i;
                }
            }

            return default;
        }

        /// <summary>
        /// Get index from the exponent symbol ('^'), from left to right
        /// </summary>
        /// <param name="pSbChunk"></param>
        internal void GetChunkOfExponent(ref StringBuilder pSbChunk)
        {
            int lStartIndex = IndexOfFirstSymbol(pSbChunk, '^');
            EraseLeftElementsFromExponent(ref pSbChunk, ref lStartIndex);
            int lClosedIndex = EraseRightElementsFromExponent(ref pSbChunk, lStartIndex);
        }

        #endregion

        #region Multiplication & Division

        private void ComputeMultiplicationOrDivision()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Addition & Substraction

        private void ComputeAdditionOrSubstraction()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Maths compute operation

        internal double DoCompute()
        {
            double lResult = default;
            double a = 0;
            double b = 0;

            switch (mOperand)
            {
                case Operand.Multiplication:
                    lResult = MathOperation.Multiply(a, b);
                    break;
                case Operand.Division:
                    lResult = MathOperation.Divide(a, b);
                    break;
                case Operand.Addition:
                    lResult = MathOperation.Add(a, b);
                    break;
                case Operand.Substraction:
                    lResult = MathOperation.Substract(a, b);
                    break;
                case Operand.Square:
                    lResult = MathOperation.Square(a);
                    break;
                case Operand.Exponent:
                    lResult = MathOperation.Exponent(a, b);
                    break;
            }

            return lResult;
        }

        #endregion
    }
}

