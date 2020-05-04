using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_Calculator
{
    /// <summary>
    /// Chunk of formula
    /// </summary>
    public class Chunk
    {
        private StringBuilder mFormula;
        public StringBuilder Formula
        {
            get { return mFormula; }
            private set { mFormula = value; }
        }

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
            string lFormula = pSb.ToString();
            mFormula = new StringBuilder(lFormula);

            SB = pSb;
            StartIndex = pStartIndex;
            Length = pLength;
        }
    }

    public enum Operand { Multiplication, Division, Addition, Substraction, Square, Exponent }

    public class PEMDAS
    {
        #region Fields

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
            mTabOperators = new char[] { '(', ')', '^', '*', '/', '+', '-' };
            // Initialize the chunk formula with the complete formula
            mChunk = new Chunk(new StringBuilder(pFormula), 0, pFormula.Length);
        }

        /// <summary>
        /// Main function to compute formula input and returns out parameter result
        /// </summary>
        /// <param name="pResult"></param>
        public string ComputeFormula()
        {
            while (mChunk.SB.ContainsAny(mTabOperators))
            {
                ComputeParenthesis();
                ComputeExponent();
                ComputeMultiplicationOrDivision();
                ComputeAdditionOrSubstraction();
                DoCompute();
                DoReplaceWithResult();
            }

            return mChunk.SB.ToString();
        }

        #region Parenthesis

        private Stack<int> mStack = new Stack<int>();

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
            // If no parenthesis, out
            if (mChunk.SB.IndexOf('(') == -1)
            {
                return;
            }

            mStack.Clear();

            if (ParenthesisAreEquivalent(mChunk.SB))
            {
                for (int i = 0; i < mChunk.SB.Length; i++)
                {
                    mChunk.StartIndex = i;
                    if (IsRealOpenedParenthesis(mChunk)) // If '(' not preceed exponent symbol
                    {
                        if (!mStack.Contains(i)) // index not already in
                        {
                            mStack.Push(i); // Add index
                        }
                    }
                    else if (mChunk.SB[i] == ')')
                    {
                        if (mStack.Count() > 0)
                        {
                            int lStartIndex = mStack.Pop(); // Pop index

                            // Get new chunk of formula from the '(' to the ')'
                            mChunk.SB.GetChunk(lStartIndex, i - lStartIndex + 1);
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
        public bool ParenthesisAreEquivalent(StringBuilder pFormula)
        {
            int lOpenedParenthesis = pFormula.ToString().Count(c => c == '(');
            int lClosedParenthesis = pFormula.ToString().Count(c => c == ')');

            return lOpenedParenthesis == lClosedParenthesis;
        }

        #endregion

        #region Exponent

        internal void ComputeExponent()
        {
            // If no exponent, out
            if (mChunk.SB.IndexOf('^') == -1)
            {
                return;
            }

            if (mChunk.Length > 0)
            {
                while (mChunk.SB.ToString().Count(p => p == '^') > 1)
                {
                    StringBuilder lSb = mChunk.SB;
                    ExtractExponentChunk(ref lSb);

                    int lOpened = lSb.IndexOf('(');
                    int lClosed = lSb.ToString().LastIndexOf(')');

                    lSb.GetChunk(lOpened, lClosed - lOpened + 1);
                    mChunk.StartIndex = mChunk.Formula.IndexOf(lSb[0]);
                    mChunk.Length = lSb.Length;
                }

                // Add operand type exponent
                mOperand = Operand.Exponent;
            }
        }

        /// <summary>
        /// Get the exponent chunk from the main formula
        /// </summary>
        /// <param name="pSb"></param>
        internal void ExtractExponentChunk(ref StringBuilder pSb)
        {
            int lIndexExp = pSb.IndexOf('^');
            DeleteLeftSequence(ref pSb, ref lIndexExp);
            DeleteRightSequenceWithParenthesis(ref pSb, lIndexExp);
        }

        /// <summary>
        /// Delete everything after the '(' && ')' exponent, modify the startIndex parameter following the begining of the current exponent value
        /// </summary>
        /// <param name="pSbChunk"></param>
        /// <param name="pStartIndex"></param>
        internal void DeleteLeftSequence(ref StringBuilder pSbChunk, ref int pStartIndex)
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
        /// Delete everything after char 
        /// </summary>
        /// <param name="pSbChunk"></param>
        /// <param name="pStartIndex"></param>
        /// <returns>The ended index</returns>
        internal void DeleteRightSequence(ref StringBuilder pSbChunk, int pStartIndex)
        {
            pStartIndex++;
            // Get RIGHT
            for (int i = pStartIndex; i < pSbChunk.Length; i++)
            {
                if (mTabOperators.Contains(pSbChunk[i]))
                {
                    pSbChunk.Remove(i, pSbChunk.Length - i);
                    break;
                }
            }
        }

        /// <summary>
        /// Delete everything after the last exponent parenthesis
        /// </summary>
        /// <param name="pSbChunk"></param>
        /// <param name="pStartIndex"></param>
        /// <returns>The ended index</returns>
        internal void DeleteRightSequenceWithParenthesis(ref StringBuilder pSbChunk, int pStartIndex)
        {
            Stack<int> lStackOpenedIndexParenthesis = new Stack<int>();

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
                        lStackOpenedIndexParenthesis.Pop();
                        if (lStackOpenedIndexParenthesis.Count() == 0)
                        {
                            i++;
                            pSbChunk.Remove(i, pSbChunk.Length - i);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get index from the exponent symbol ('^'), from left to right
        /// </summary>
        /// <param name="pSbChunk"></param>
        internal void GetChunkOfExponent(ref StringBuilder pSbChunk)
        {
            int lStartIndex = pSbChunk.IndexOf('^');
            DeleteLeftSequence(ref pSbChunk, ref lStartIndex);
            DeleteRightSequenceWithParenthesis(ref pSbChunk, lStartIndex);
        }

        #endregion

        #region Multiplication & Division

        internal void ComputeMultiplicationOrDivision()
        {
            // If no multiply or divide symbol, out
            if (!mChunk.SB.ContainsAny(new char[] { '*', '/' }))
            {
                return;
            }

            if (mChunk.Length > 0)
            {
                // Get first '*' or '/'
                int lIndex = mChunk.SB.IndexOf('*');

                if (lIndex != -1)
                {
                    int lIndexOfDiv = mChunk.SB.IndexOf('/');

                    if (lIndexOfDiv != -1 && lIndexOfDiv < lIndex)
                    {
                        lIndex = lIndexOfDiv;
                    }
                }
                else
                {
                    int lIndexOfDiv = mChunk.SB.IndexOf('/');
                    if (lIndexOfDiv != -1)
                    {
                        lIndex = lIndexOfDiv;
                    }
                }

                if (lIndex != -1)
                {
                    // Delete left and from formula
                    StringBuilder lSb = mChunk.SB;
                    DeleteLeftSequence(ref lSb, ref lIndex);
                    DeleteRightSequence(ref lSb, lIndex);

                    // Ajouter l'indexStart
                    // Ajouter le Length
                }
            }
        }

        #endregion

        #region Addition & Substraction

        private void ComputeAdditionOrSubstraction()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Maths compute operation

        private void DoReplaceWithResult()
        {
            throw new NotImplementedException();
        }

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
                    lResult = MathOperation.Sqrt(a);
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

