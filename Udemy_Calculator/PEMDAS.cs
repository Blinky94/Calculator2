using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_Calculator
{
    /// <summary>
    /// Chunk of fomula
    /// </summary>
    public class Chunk
    {
        public StringBuilder SB { get; set; }
        public int StartIndex { get; set; }

        private int mLength;
        public int Length
        {
            get { return mLength; }
            set { mLength = SB.Length; }
        }

        public Chunk(StringBuilder pSb, int pStartIndex, int pLength)
        {
            SB = pSb;
            StartIndex = pStartIndex;
            Length = pLength;
        }
    }

    public enum Operand { Multiplication, Division, Addition, Substraction, Square, Exponent }

    public class PEMDAS
    {
        #region Fields

        private string mFormula;
        private char[] mTabOperators;
        private Operand mOperand;
        internal Chunk mChunk;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pFormula"></param>
        public PEMDAS(string pFormula)
        {
            mFormula = pFormula;
            mTabOperators = new char[] { '(', ')', '^', '*', '/', '+', '-' };
        }

        /// <summary>
        /// Main function to compute formula input and returns out parameter result
        /// </summary>
        /// <param name="pResult"></param>
        public void ComputeFormula(out string pResult)
        {
            // Initialize the chunk formula with the complete formula
            mChunk = new Chunk(new StringBuilder(mFormula), 0, mFormula.Length);
            StringBuilder lSb = new StringBuilder(mFormula);

            while (lSb.ContainsAny(mTabOperators))
            {
                ComputeParenthesis();
                ComputeExponent();
                ComputeMultiplicationOrDivision();
                ComputeAdditionOrSubstraction();
            }

            pResult = lSb.ToString();
        }

        #region Parenthesis

        private Stack<int> mStackOfOpenedParenthesisIndex = new Stack<int>();

        internal bool IsRealOpenedParenthesis(Chunk pCkunk)
        {
            if (pCkunk.StartIndex > 0)
            {
                return pCkunk.SB[pCkunk.StartIndex] == '(' && (pCkunk.SB[pCkunk.StartIndex - 1] != '^');
            }
            else
            {
                return pCkunk.SB[pCkunk.StartIndex] == '(';
            }
        }

        /// <summary>
        /// Main method to compute all parenthesis in a formula
        /// </summary>
        internal void ComputeParenthesis()
        {
            if (ParenthesisAreEquivalent(mChunk.SB.ToString()))
            {
                for (int i = 0; i < mChunk.SB.Length; i++)
                {
                    mChunk.StartIndex = i;
                    if (IsRealOpenedParenthesis(mChunk)) // If '(' not preceed exponent symbol
                    {
                        if (!mStackOfOpenedParenthesisIndex.Contains(i)) // index not already in
                        {
                            mStackOfOpenedParenthesisIndex.Push(i); // Add index
                        }
                    }
                    else if (mChunk.SB[i] == ')')
                    {
                        if (mStackOfOpenedParenthesisIndex.Count() > 0)
                        {
                            int lStartIndex = mStackOfOpenedParenthesisIndex.Pop(); // Pop index

                            // Get new chunk of formula from the '(' to the ')'
                            mChunk.SB.GetChunk(lStartIndex, i - lStartIndex);
                            mChunk.StartIndex = lStartIndex;
                            mChunk.Length = i - lStartIndex++;
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
            if (mChunk.Length > 0 && mChunk.SB.Contains('^'))
            {


                // Add operand type exponent
                mOperand = Operand.Exponent;
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
                            // mChunkExponent = pSbChunk.ToString().ToCharArray();
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

