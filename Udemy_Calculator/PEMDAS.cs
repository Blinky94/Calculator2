﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

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

    public enum Operator { Multiplication, Division, Addition, Substraction, Square, Exponent }

    public class PEMDAS
    {
        #region Fields

        private char[] mParenthesis;
        private char[] mOperators;
        private char[] mComa;

        /// <summary>
        /// Enum of operators (multiply, divide, add, substract...)
        /// </summary>
        public Operator Operator { get; set; }

        /// <summary>
        /// Chunk of a formula
        /// </summary>
        public Chunk Chunk { get; set; }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pFormula"></param>
        public PEMDAS(string pFormula)
        {
            mParenthesis = new char[] { '(', ')' };
            mOperators = new char[] { '+', '-', '*', '/', '^', '√' };
            mComa = new char[] { '.', ',' };

            // Initialize the chunk formula with the complete formula
            Chunk = new Chunk(new StringBuilder(pFormula), 0, pFormula.Length);
        }

        /// <summary>
        /// Main function to compute formula input and returns out parameter result
        /// </summary>
        /// <param name="pResult"></param>
        public string ComputeFormula()
        {
            while (Chunk.SB.ContainsAny(mOperators.Concat(mParenthesis).ToArray()))
            {
                ComputeParenthesis();
                ComputeExponent();
                ComputeOperand(new char[] { '*', '/' });
                ComputeOperand(new char[] { '+', '-' });
                DoCompute(out decimal lResult);
                DoReplaceByResult(lResult);
            }

            return Chunk.SB.ToString();
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
            if (Chunk.SB.IndexOf('(') == -1)
            {
                return;
            }

            mStack.Clear();

            if (ParenthesisAreEquivalent(Chunk.SB))
            {
                for (int i = 0; i < Chunk.SB.Length; i++)
                {
                    Chunk.StartIndex = i;
                    if (IsRealOpenedParenthesis(Chunk)) // If '(' not preceed exponent symbol
                    {
                        if (!mStack.Contains(i)) // index not already in
                        {
                            mStack.Push(i); // Add index
                        }
                    }
                    else if (Chunk.SB[i] == ')')
                    {
                        if (mStack.Count() > 0)
                        {
                            int lStartIndex = mStack.Pop(); // Pop index

                            // Get new chunk of formula from the '(' to the ')'
                            Chunk.SB.GetChunk(lStartIndex, i - lStartIndex + 1);
                            Chunk.StartIndex = lStartIndex;
                            Chunk.Length = i - lStartIndex++;
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
            if (Chunk.SB.IndexOf('^') == -1)
            {
                return;
            }

            if (Chunk.Length > 0)
            {
                while (Chunk.SB.ToString().Count(p => p == '^') > 1)
                {
                    StringBuilder lSb = Chunk.SB;
                    ExtractExponentChunk(ref lSb);

                    int lOpened = lSb.IndexOf('(');
                    int lClosed = lSb.ToString().LastIndexOf(')');

                    lSb.GetChunk(lOpened, lClosed - lOpened + 1);
                    Chunk.StartIndex = Chunk.Formula.IndexOf(lSb[0]);
                    Chunk.Length = lSb.Length;
                }

                // Add operand type exponent
                Operator = Operator.Exponent;
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
        internal int DeleteLeftSequence(ref StringBuilder pSbChunk, ref int pStartIndex)
        {
            int lIndex = -1;
            // Get LEFT start index number from ^
            for (int i = pStartIndex - 1; i >= 0; i--)
            {
                if (!char.IsNumber(pSbChunk[i]))
                {
                    lIndex = i + 1;
                    pSbChunk.Remove(0, i + 1);
                    pStartIndex -= i;
                    break;
                }
            }

            return lIndex;
        }

        /// <summary>
        /// Delete everything after char 
        /// </summary>
        /// <param name="pSbChunk"></param>
        /// <param name="pStartIndex"></param>
        /// <returns>The ended index</returns>
        internal int DeleteRightSequence(ref StringBuilder pSbChunk, int pStartIndex)
        {
            int lIndex = -1;
            pStartIndex++;
            // Get RIGHT
            for (int i = pStartIndex; i < pSbChunk.Length; i++)
            {
                if (mParenthesis.Concat(mOperators).ToArray().Contains(pSbChunk[i]))
                {
                    lIndex = i + 1;
                    pSbChunk.Remove(i, pSbChunk.Length - i);
                    break;
                }
            }

            return lIndex;
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

        #endregion

        #region Addition, Substraction, multiplication, division

        internal void ComputeOperand(char[] pLstOperands)
        {
            if (!Chunk.SB.ContainsAny(pLstOperands))
            {
                return;
            }

            if (Chunk.Length > 0)
            {
                int lIndex = Chunk.SB.IndexOf(pLstOperands[0]);

                if (lIndex != -1)
                {
                    int lIndexOfDiv = Chunk.SB.IndexOf(pLstOperands[1]);

                    if (lIndexOfDiv != -1 && lIndexOfDiv < lIndex)
                    {
                        lIndex = lIndexOfDiv;
                    }
                }
                else
                {
                    int lIndexOfDiv = Chunk.SB.IndexOf(pLstOperands[1]);
                    if (lIndexOfDiv != -1)
                    {
                        lIndex = lIndexOfDiv;
                    }
                }

                if (lIndex != -1)
                {
                    // Delete left and from formula
                    StringBuilder lSb = Chunk.SB;
                    int lBeginIndex = DeleteLeftSequence(ref lSb, ref lIndex);
                    int lEndIndex = DeleteRightSequence(ref lSb, lIndex);

                    // Ajouter l'indexStart
                    Chunk.StartIndex = lBeginIndex;

                    // Ajouter le Length
                    Chunk.Length = lEndIndex - lBeginIndex;
                }
            }
        }

        #endregion

        #region Maths compute operation

        /// <summary>
        /// Extract a sequence number from a chunk formula starting with index
        /// </summary>
        /// <param name="pStartIndex"></param>
        /// <param name="pStr"></param>
        internal void GetNumberFromChunk(ref string pStr, int pStartIndex = 0)
        {
            for (int i = pStartIndex; i < Chunk.SB.Length; i++)
            {
                if (Char.IsDigit(Chunk.SB[i]) || Chunk.SB[i].ToString().IndexOfAny(mComa) != -1)
                {
                    pStr += Chunk.SB[i];
                }
                else if (Chunk.SB[i].ToString().IndexOfAny(mOperators) != -1)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Return the index of an operator (+,-,*,/) from a sequence of the chunk formula
        /// </summary>
        /// <returns></returns>
        internal int GetIndexOperator()
        {
            return Chunk.SB.ToString().IndexOfAny(mOperators);
        }

        /// <summary>
        /// Remove the exceedent of a string to limit the length for decimal number
        /// Use to avoid the rounded effect in the Decimal.tryParse
        /// </summary>
        /// <param name="pStr"></param>
        /// <returns></returns>
        internal string TrimLengthString(string pStr)
        {
            int lMaxi = Decimal.MaxValue.ToString().Length;
            if (pStr.Length > lMaxi)
            {
                pStr = pStr.Remove(lMaxi);
            }
            return pStr;
        }

        /// <summary>
        /// Extract operands to compute
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        internal void ExtractOperands(out decimal a, out decimal b)
        {
            string lA = string.Empty;
            string lB = string.Empty;

            GetNumberFromChunk(ref lA);

            int lIndex = GetIndexOperator();
            lIndex++;
            GetNumberFromChunk(ref lB, lIndex);

            lA = TrimLengthString(lA);
            lB = TrimLengthString(lB);

            a = decimal.Parse(lA.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture);
            b = decimal.Parse(lB.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Compute the chunk formula from a chunk sequence
        /// </summary>
        internal void DoCompute(out decimal pResult)
        {
            pResult = default;

            // Extraction units from formula
            ExtractOperands(out decimal a, out decimal b);

            // Extract the operator
            GetOperator();

            switch (Operator)
            {
                case Operator.Multiplication:
                    pResult = MathOperation.Multiply(a, b);
                    break;
                case Operator.Division:
                    pResult = MathOperation.Divide(a, b);
                    break;
                case Operator.Addition:
                    pResult = MathOperation.Add(a, b);
                    break;
                case Operator.Substraction:
                    pResult = MathOperation.Substract(a, b);
                    break;
                case Operator.Square:
                    pResult = MathOperation.Sqrt(a);
                    break;
                case Operator.Exponent:
                    pResult = MathOperation.Exponent(a, b);
                    break;
            }
        }

        /// <summary>
        /// Extract the operator from the Chunk
        /// </summary>
        internal void GetOperator()
        {
            int lIndex = Chunk.SB.ToString().IndexOfAny(mOperators);
            char lOperator = Chunk.SB[lIndex];

            switch (lOperator)
            {
                case '^':
                    Operator = Operator.Exponent;
                    break;
                case '*':
                    Operator = Operator.Multiplication;
                    break;
                case '/':
                    Operator = Operator.Division;
                    break;
                case '+':
                    Operator = Operator.Addition;
                    break;
                case '-':
                    Operator = Operator.Substraction;
                    break;
                case '√':
                    Operator = Operator.Square;
                    break;
            }
        }

        /// <summary>
        /// Replace the chunk sequence by the result into the main formula
        /// </summary>
        /// <param name="lResult"></param>
        internal void DoReplaceByResult(decimal pResult)
        {

        }

        #endregion
    }
}

