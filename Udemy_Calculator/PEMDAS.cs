using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_Calculator
{
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

            }

            pResult = mSb.ToString();
        }

        #region Parenthesis

        private Stack<int> mStackOfOpenedParenthesisIndex = new Stack<int>();

        internal bool IsRealOpenedParenthesis(int pIndex)
        {
            if (pIndex > 0)
            {
                return mSb[pIndex] == '(' && (mSb[pIndex - 1] != '^');
            }
            else
            {
                return mSb[pIndex] == '(';
            }
        }

        internal char[] mChunk;

        /// <summary>
        /// Main method to compute all parenthesis in a formula
        /// </summary>
        internal void ComputeParenthesis()
        {
            if (ParenthesisAreEquivalent(mFormula))
            {
                for (int i = 0; i < mSb.Length; i++)
                {
                    if (IsRealOpenedParenthesis(i)) // If '(' not preceed exponent symbol
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

                            mChunk = new char[i - lOpIndex];
                            mSb.CopyTo(lOpIndex, mChunk, 0, i - lOpIndex);
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
            if (mChunk.Count() > 0)
            {
                StringBuilder lSbChunk = new StringBuilder(new string(mChunk)); // Convert chunk array to streambuilder object

                // Number of parenthesis are equivalent ex : (5+6^(2)) or 6^(2)
                if (ParenthesisAreEquivalent(lSbChunk.ToString()))
                {
                    // get chunk of exponent
                    GetChunkOfExponent(ref lSbChunk);

                    // if parenthesis contains () or ^, or */, or +-, compute

                    // compute Math.pow(num, exponent)

                    // Replace result in mFomula

                    for (int i = 0; i < lSbChunk.Length; i++)
                    {
                        if (IsRealOpenedParenthesis(i)) // If '(' not preceed exponent symbol
                        {
                            if (!mStackOfOpenedParenthesisIndex.Contains(i)) // index not already in
                            {
                                mStackOfOpenedParenthesisIndex.Push(i); // Add index
                            }
                        }
                        else if (lSbChunk[i] == ')')
                        {
                            if (mStackOfOpenedParenthesisIndex.Count() > 0)
                            {
                                int lOpIndex = mStackOfOpenedParenthesisIndex.Pop(); // Pop index
                                lOpIndex++; // Adjust the index number from 1 to ...

                                mChunk = new char[i - lOpIndex];
                                lSbChunk.CopyTo(lOpIndex, mChunk, 0, i - lOpIndex);
                            }
                        }
                    }
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
    }

    #endregion

    #region Multiplication



    #endregion

    #region Division



    #endregion

    #region Addition



    #endregion

    #region Substraction



    #endregion
}

