using System;
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

    public enum EOperation { Unknown, Multiplication, Division, Addition, Substraction, Square, Exponent }

    public class PEMDAS
    {
        #region Fields
        // Pattern to test if there are no operator remaining in the current formula
        private readonly string lFinishedComputationPattern = @"(?(?<=[(]|[Ee])([÷\/×xX*\^√])|[+\-÷\/×xX*\^√])";

        // Enum to select which part of a chunk
        internal enum EFormulaPart { All = 0, Left = 1, Right = 2 };

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
            // Initialize the chunk formula with the complete formula
            Chunk = new Chunk(new StringBuilder(pFormula), 0, pFormula.Length);
        }

        /// <summary>
        /// Main function to compute formula input and returns out parameter result
        /// </summary>
        /// <param name="pResult"></param>
        public string ComputeFormula()
        {
            Regex lRegex = new Regex(lFinishedComputationPattern);

            Match lMatch = lRegex.Match(Chunk.Formula.ToString());

            while (lMatch.Success)
            {
                // P (Parenthesis) E (Exponents) MD (Multiplcation and division) AS (Addition and substraction): PEMDAS
                ExtractParenthesis();   // P
                ComputeExponent();      // E
                ComputeMultAndDiv();    // MD
                ComputeAddAndSub();     // AS

                DoCompute(out string lResult);

                if (string.IsNullOrEmpty(lResult))
                {
                    throw new NullReferenceException("No result to do comute from current chuck !!!");
                }

                DoReplaceByResult(lResult);

                lMatch = lRegex.Match(Chunk.Formula.ToString());
            }

            return Chunk.SB.ToString();
        }

        #region Extract Parenthesis, Exponent, Multiplication/Division, Addition/Substraction

        /// <summary>
        /// Method to apply regular expression pattern on chunk
        /// Apply the new chunk, the index of the chunk in the formula and the length of the chunk
        /// </summary>
        /// <param name="pPattern"></param>
        private void ArrangeChunkOfFormula(string pPattern)
        {
            Regex regex = new Regex(pPattern);

            Match lMatch = regex.Match(Chunk.SB.ToString());

            if (!lMatch.Success)
            {
                return;
            }

            int lIndex = lMatch.Groups[0].Index;
            int lLength = lMatch.Groups[0].Length;

            Chunk.SB.GetChunk(lIndex, lLength);
            Chunk.StartIndex = lIndex;
            Chunk.Length = lLength;
        }

        #region Parenthesis

        internal void ExtractParenthesis()
        {
            // Regex to select all parenthesis groups
            string lPattern = @"[({\[](?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)((?<Operator>[+\-÷\/×xX*])(?(?=[({\[][-])[({\[]+[-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]+|[√]?\d+[.,]?\d*([Ee][+]\d*)?))+[)}\]]";

            ArrangeChunkOfFormula(lPattern);
        }

        #endregion

        #region Exponent

        internal void ComputeExponent()
        {
            // Regex to select all exponents groups
            string lPattern = @"(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)[\^]+[({\[](?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)[)}\]]";

            ArrangeChunkOfFormula(lPattern);
        }

        #endregion

        #region Multiplication/Division

        internal void ComputeMultAndDiv()
        {
            // Regex to select all multiplication and division groups
            string lPattern = @"(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)[×xX*÷\/]+(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)";

            ArrangeChunkOfFormula(lPattern);
        }

        #endregion

        #region Addition/Substraction 

        internal void ComputeAddAndSub()
        {
            // Regex to select all addition and substraction groups
            string lPattern = @"(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)[+-]+(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)";

            ArrangeChunkOfFormula(lPattern);
        }

        #endregion

        #endregion 

        #region ExtractOperands

        /// <summary>
        /// Extract each part of a simple formula
        /// </summary>
        /// <param name="pLeftOperand"></param>
        /// <param name="pRightOperand"></param>
        /// <param name="pOperator"></param>
        public void ExtractArithmeticsGroups(out double pLeftOperand, out double pRightOperand, out EOperation? pOperator)
        {
            pLeftOperand = default;
            pRightOperand = default;

            string lPattern = @"(?<LeftOperand>(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?))?(?<Operator>[+\-÷\/×xX*\^√])(?<RightOperand>(?(?=[({\[]+[-])[({\[]+[-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]+|[({\[]*[√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]*))";

            Regex regex = new Regex(lPattern);

            Match lMatch = regex.Match(Chunk.SB.ToString());

            string lLeft = lMatch.Groups["LeftOperand"].Value.Replace(",", ".").Replace("(", "").Replace(")", "");
            string lRight = lMatch.Groups["RightOperand"].Value.Replace(",", ".").Replace("(", "").Replace(")", "");

            if (!string.IsNullOrEmpty(lLeft))
            {
                //pLeftOperand = GetDecimalFromString(lLeft);
                pLeftOperand = GetDoubleFromString(lLeft);
            }

            if (!string.IsNullOrEmpty(lRight))
            {
                // pRightOperand = GetDecimalFromString(lRight);
                pRightOperand = GetDoubleFromString(lRight);
            }

            pOperator = WhatOperator(char.Parse(lMatch.Groups["Operator"].Value));
        }

        #endregion

        #region TrimLengthString

        /// <summary>
        /// Remove the exceedent of a string to limit the length for decimal number
        /// Use to avoid the rounded effect in the Decimal.tryParse
        /// </summary>
        /// <param name="pStr"></param>
        /// <returns></returns>
        internal static decimal GetDecimalFromString(string pStr)
        {
            bool lHasExponential = (pStr.Contains("E") || pStr.Contains("e"));

            if (lHasExponential)
            {
                return decimal.Parse(pStr, NumberStyles.Float, CultureInfo.InvariantCulture);
            }
            else if (decimal.TryParse(pStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal lValue))
            {
                return lValue;
            }
            else
            {
                throw new OverflowException($"Le format de la valeur est incorrecte ({pStr})");
            }
        }

        /// <summary>
        /// Remove the exceedent of a string to limit the length for decimal number
        /// Use to avoid the rounded effect in the Decimal.tryParse
        /// </summary>
        /// <param name="pStr"></param>
        /// <returns></returns>
        internal static double GetDoubleFromString(string pStr)
        {
            try
            {
                bool lHasExponential = (pStr.Contains("E") || pStr.Contains("e"));

                if (lHasExponential)
                {
                    return double.Parse(pStr, NumberStyles.Float, null);
                }
                else
                {
                    return double.Parse(pStr, NumberStyles.Any, CultureInfo.InvariantCulture);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region Maths compute operation

        /// <summary>
        /// Compute the chunk formula from a chunk sequence
        /// </summary>
        internal void DoCompute(out string pResult)
        {
            pResult = default;

            ExtractArithmeticsGroups(out double lLeftOperand, out double lRightOperand, out EOperation? lOperator);

            if (double.IsNaN(lLeftOperand) || double.IsNaN(lRightOperand) || lOperator == null)
            {
                throw new Exception("Do compute: operands are not consistents !!!");
            }

            string lResult = string.Empty;

            switch (lOperator)
            {
                case EOperation.Multiplication:
                    lResult = MathOperation.Multiply(lLeftOperand, lRightOperand);
                    break;
                case EOperation.Division:
                    lResult = MathOperation.Divide(lLeftOperand, lRightOperand);
                    break;
                case EOperation.Addition:
                    lResult = MathOperation.Add(lLeftOperand, lRightOperand);
                    break;
                case EOperation.Substraction:
                    lResult = MathOperation.Substract(lLeftOperand, lRightOperand);
                    break;
                case EOperation.Square:
                    lResult = MathOperation.Sqrt(lRightOperand);
                    break;
                case EOperation.Exponent:
                    lResult = MathOperation.Exponent(lLeftOperand, lRightOperand);
                    break;
            }

            pResult = Double.Parse(lResult) < 0 ? $"({lResult})" : lResult;
        }

        private EOperation mOperator;

        /// <summary>
        /// Get the operator from the string
        /// </summary>
        internal EOperation WhatOperator(char pOperator)
        {
            switch (pOperator)
            {
                case '^':
                    mOperator = EOperation.Exponent;
                    break;
                case '×':
                case 'x':
                case 'X':
                case '*':
                    mOperator = EOperation.Multiplication;
                    break;
                case '÷':
                case '/':
                    mOperator = EOperation.Division;
                    break;
                case '+':
                    mOperator = EOperation.Addition;
                    break;
                case '-':
                    mOperator = EOperation.Substraction;
                    break;
                case '√':
                    mOperator = EOperation.Square;
                    break;
                default:
                    mOperator = EOperation.Unknown;
                    break;
            }

            return mOperator;
        }

        /// <summary>
        /// Replace the chunk sequence by the result into the main formula
        /// </summary>
        /// <param name="lResult"></param>
        internal void DoReplaceByResult(string pResult)
        {
            // Check if compute if finish, return if yes
            Regex lRegex = new Regex(lFinishedComputationPattern);

            Match lMatch = lRegex.Match(pResult);

            if (!lMatch.Success)
            {
                Chunk.Formula.Remove(Chunk.StartIndex, Chunk.Length);
                Chunk.Formula.Insert(Chunk.StartIndex, pResult);
                string lTmp = Chunk.Formula.ToString();
                Chunk.SB = new StringBuilder(lTmp);
                return;
            }

            Chunk.SB.Remove(Chunk.StartIndex, Chunk.Length);
            Chunk.SB.Insert(Chunk.StartIndex, pResult);
        }

        #endregion
    }
}

