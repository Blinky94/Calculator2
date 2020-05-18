using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
        private string[] mSpecials;
        // Regex to split in 3 groups a simple formula (0 => all the formula, 1 => left part before [+-÷×], 2 => right part after [+-÷×])
        private string mRegexSplitGroup = @"^([\(]?[-]*\d*[.,]*[\d*]*[\)]*)([+-×÷])([\(]*[-]*\d*[.,]*[\d*]*[\)]?)$";
        // Enum to select which part of a chunk
        internal enum eFormulaPart { All = 0, Left = 1, Right = 2 };

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
            mOperators = new char[] { '+', '-', '×', '÷', '^', '√' };
            mComa = new char[] { '.', ',' };
            mSpecials = new string[] { "E+" };

            // Initialize the chunk formula with the complete formula
            Chunk = new Chunk(new StringBuilder(pFormula), 0, pFormula.Length);
        }

        /// <summary>
        /// Check if the compute formula is finish or not by containing any operator symbols or parenthesis
        /// </summary>
        /// <returns></returns>
        internal bool FormulaContainsOperatorsYet(string pChunk)
        {
            var lArray = mOperators.Concat(mParenthesis).ToArray();

            foreach (var pDigit in pChunk)
            {
                if (lArray.Contains(pDigit))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Main function to compute formula input and returns out parameter result
        /// </summary>
        /// <param name="pResult"></param>
        public string ComputeFormula()
        {
            while (!decimal.TryParse(Chunk.Formula.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal _))
            {
                string vvv = Chunk.SB.ToString();
                ComputeParenthesis();
                ComputeExponent();
                ComputeOperand(new char[] { '×', '÷' });
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
                return pCkunk.SB[pCkunk.StartIndex] == '('
                    && (pCkunk.SB[pCkunk.StartIndex - 1] != '^')
                    && (pCkunk.SB[pCkunk.StartIndex + 1] != '-');
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
            DeleteFromExponentOverParenthesis(ref pSb, lIndexExp);
        }

        /// <summary>
        /// Delete everything after the '(' && ')' exponent, modify the startIndex parameter following the begining of the current exponent value
        /// </summary>
        /// <param name="pSbChunk"></param>
        /// <param name="pStartIndex"></param>
        internal int DeleteLeftSequence(ref StringBuilder pSbChunk, ref int pStartIndex)
        {
            int lIndex = 0;

            // Voir pour verifier si la séquence contient E+, si oui, ne pas supprimer
            // GET LEFT side of the chunk formula
            char[] lLstOperators = mParenthesis.Concat(mOperators).ToArray();

            for (int i = pStartIndex - 1; i >= 0; i--)
            {
                if (lLstOperators.Contains(pSbChunk[i]))
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
            int lIndex = 0;
            pStartIndex++;

            // Voir pour verifier si la séquence contient E+, si oui, ne pas supprimer
            // Get RIGHT side of the chunk formula
            char[] lLstOperators = mParenthesis.Concat(mOperators).ToArray();

            for (int i = pStartIndex; i < pSbChunk.Length; i++)
            {
                if (lLstOperators.Contains(pSbChunk[i]))
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
        internal void DeleteFromExponentOverParenthesis(ref StringBuilder pSbChunk, int pStartIndex)
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
                    // Delete left and right from formula
                    StringBuilder lSb = Chunk.SB;
                    int lBeginIndex = DeleteLeftSequence(ref lSb, ref lIndex);
                    int lEndIndex = DeleteRightSequence(ref lSb, lIndex);

                    // Add indexStart to the Chunk
                    Chunk.StartIndex = lBeginIndex;

                    // Add Length to the Chunk
                    Chunk.Length = lEndIndex - lBeginIndex;
                }
            }
        }

        #endregion

        #region Maths compute operation

        /// <summary>
        /// Extract a sequence number from a chunk formula starting with index left (index = 0) or right (index != 0)
        /// </summary>
        /// <param name="pStartIndex"></param>
        /// <param name="pStr"></param>
        internal void GetChunkPart(out string pStr, eFormulaPart pFPart)
        {
            var lRegex = new Regex(mRegexSplitGroup);
            Match match = lRegex.Match(Chunk.SB.ToString());

            string lGroup = match.Groups[(int)pFPart].Value;

            // Reajust the group without parentesis if inequals count are detected (ex : (40 OR 50))
            if (!ParenthesisAreEquivalent(new StringBuilder(lGroup)))
            {
                lGroup = lGroup.Trim(mParenthesis);
            }

            pStr = lGroup;
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
            GetChunkPart(out string lA, eFormulaPart.Left);
            GetChunkPart(out string lB, eFormulaPart.Right);

            lA = TrimLengthString(lA);
            lB = TrimLengthString(lB);
            lA = lA.Replace(',', '.');
            lB = lB.Replace(',', '.');

            NumberStyles lStyle = NumberStyles.Number | NumberStyles.AllowParentheses | NumberStyles.AllowTrailingSign | NumberStyles.AllowLeadingSign;

            a = decimal.Parse(lA, lStyle, CultureInfo.InvariantCulture);
            b = decimal.Parse(lB, lStyle, CultureInfo.InvariantCulture);
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
                case '×':
                    Operator = Operator.Multiplication;
                    break;
                case '÷':
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
            // pResult = pResult.ToString().Replace('.', ',');
            // Check if compute if finish, return if yes
            if (Chunk.SB.CountChar(mOperators) == 1)
            {
                Chunk.Formula.Remove(Chunk.StartIndex, Chunk.Length);
                Chunk.Formula.Insert(Chunk.StartIndex, pResult);
                string lTmp = Chunk.Formula.ToString();
                Chunk.SB = new StringBuilder(lTmp);
                return;
            }

            Chunk.SB.Remove(Chunk.StartIndex, Chunk.Length);
            Chunk.SB.Insert(Chunk.StartIndex, pResult.ToString());
        }

        #endregion
    }
}

