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
            if (ParenthesisAreEquivalent())
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
        public bool ParenthesisAreEquivalent()
        {
            int lOpenedParenthesis = mFormula.Count(c => c == '(');
            int lClosedParenthesis = mFormula.Count(c => c == ')');

            return lOpenedParenthesis == lClosedParenthesis;
        }

        #endregion

        #region Exponent

        internal void ComputeExponent()
        {
            // compute chunk if not empty
            if (mChunk.Count() != 0)
            {
                StringBuilder lSbChunk = new StringBuilder(new string(mChunk)); // Convert chunk array to streambuilder object

                for (int i = 0; i < lSbChunk.Length; i++)
                {
                    if(lSbChunk[i] == '^')
                    {

                    }
                }
            }
            else // compute formula
            {

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
}
