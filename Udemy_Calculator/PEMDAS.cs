using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

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
            TraceLogs.AddTechnical($"ComputeFormula: Begining to compute de formula with Regex pattern ({lFinishedComputationPattern})");
            TraceLogs.AddInfo($"ComputeFormula: onto the formula ({Chunk.Formula})");

            Regex lRegex = new Regex(lFinishedComputationPattern);
            Match lMatch;

            try
            {
                do
                {
                    // P (Parenthesis) E (Exponents) MD (Multiplcation and division) AS (Addition and substraction): PEMDAS
                    ExtractParenthesis();   // P
                    ComputeExponent();      // E
                    ComputeMultAndDiv();    // MD
                    ComputeAddAndSub();     // AS

                    DoCompute(out string lResult);

                    if (string.IsNullOrEmpty(lResult))
                    {
                        string lWarn = "No result to do compute from current chuck !!!";
                        TraceLogs.AddWarning(lWarn);
                        //throw new NullReferenceException(lWarn);
                    }

                    DoReplaceByResult(lResult);
                    lMatch = lRegex.Match(Chunk.Formula.ToString());

                } while (lMatch.Success);

                return Chunk.SB.ToString();
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError("ComputeFormula:\n" + e.Message.ToString());
            }

            return null;
        }

        #region Extract Parenthesis, Exponent, Multiplication/Division, Addition/Substraction

        /// <summary>
        /// Method to apply regular expression pattern on chunk
        /// Apply the new chunk, the index of the chunk in the formula and the length of the chunk
        /// </summary>
        /// <param name="pPattern"></param>
        private void ArrangeChunkOfFormula(string pPattern)
        {
            TraceLogs.AddTechnical($"ArrangeChunkOfFormula: Applying Regex pattern ({pPattern})");
            TraceLogs.AddInfo($"ArrangeChunkOfFormula: on the formula chunk ({Chunk.SB})");

            try
            {
                Regex regex = new Regex(pPattern);

                Match lMatch = regex.Match(Chunk.SB.ToString());

                if (!lMatch.Success)
                {
                    TraceLogs.AddInfo("ArrangeChunkOfFormula: No matching pattern onto the chunk");
                    return;
                }

                int lIndex = lMatch.Groups[0].Index;
                int lLength = lMatch.Groups[0].Length;

                Chunk.SB.GetChunk(lIndex, lLength);
                Chunk.StartIndex = lIndex;
                Chunk.Length = lLength;
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError("ArrangeChunkOfFormula:\n" + e.Message.ToString());
            }
        }

        #region Parenthesis

        internal void ExtractParenthesis()
        {

            // Regex to select all parenthesis groups
            string lPattern = @"[({\[](?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)((?<Operator>[+\-÷\/×xX*])(?(?=[({\[][-])[({\[]+[-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]+|[√]?\d+[.,]?\d*([Ee][+]\d*)?))+[)}\]]";

            TraceLogs.AddTechnical($"ExtractParenthesis: Pattern {lPattern}");

            ArrangeChunkOfFormula(lPattern);
        }

        #endregion

        #region Exponent

        internal void ComputeExponent()
        {
            // Regex to select all exponents groups
            string lPattern = @"(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)[\^]+[({\[](?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)[)}\]]";

            TraceLogs.AddTechnical($"ComputeExponent: Pattern {lPattern}");

            ArrangeChunkOfFormula(lPattern);
        }

        #endregion

        #region Multiplication/Division

        internal void ComputeMultAndDiv()
        {
            // Regex to select all multiplication and division groups
            string lPattern = @"(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)[×xX*÷\/]+(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)";

            TraceLogs.AddTechnical($"ComputeMultAndDiv: Pattern {lPattern}");

            ArrangeChunkOfFormula(lPattern);
        }

        #endregion

        #region Addition/Substraction 

        internal void ComputeAddAndSub()
        {
            // Regex to select all addition and substraction groups
            string lPattern = @"(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)[+-]+(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)";

            TraceLogs.AddTechnical($"ComputeAddAndSub: Pattern {lPattern}");

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
            TraceLogs.AddInfo($"ExtractArithmeticsGroups: Extracting each part of operands from the formula ({Chunk.SB})");

            pLeftOperand = default;
            pRightOperand = default;
            pOperator = default;

            try
            {
                string lPattern = @"(?<LeftOperand>(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?))?(?<Operator>[+\-÷\/×xX*\^√])(?<RightOperand>(?(?=[({\[]+[-])[({\[]+[-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]+|[({\[]*[√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]*))";

                Regex regex = new Regex(lPattern);
                Match lMatch = regex.Match(Chunk.SB.ToString());

                TraceLogs.AddTechnical($"ExtractArithmeticsGroups: Pattern for extractions ({lPattern})");

                string lLeft = lMatch.Groups["LeftOperand"].Value.Replace(",", ".").Replace("(", "").Replace(")", "");
                string lRight = lMatch.Groups["RightOperand"].Value.Replace(",", ".").Replace("(", "").Replace(")", "");

                TraceLogs.AddInfo($"ExtractArithmeticsGroups: Left part: {lLeft} ; Right part: {lRight}");

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
                TraceLogs.AddInfo($"ExtractArithmeticsGroups: Operator: {pOperator}");
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError("ExtractArithmeticsGroups:\n" + e.Message.ToString());
            }
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
            TraceLogs.AddInfo($"GetDecimalFromString: Removing the excedent of the string to limit the length for decimal number");

            decimal lResult = default;

            try
            {
                bool lHasExponential = (pStr.Contains("E") || pStr.Contains("e"));

                if (lHasExponential)
                {
                    return decimal.Parse(pStr, NumberStyles.Float, CultureInfo.InvariantCulture);
                }
                else if (decimal.TryParse(pStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal lValue))
                {
                    lResult = lValue;
                }
                else
                {
                    TraceLogs.AddWarning($"Format of the string is incorrect: ({pStr})");
                    //throw new OverflowException($"Le format de la valeur est incorrecte ({pStr})");
                }
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError("GetDecimalFromString:\n" + e.Message.ToString());
            }

            return lResult;
        }

        /// <summary>
        /// Remove the exceedent of a string to limit the length for decimal number
        /// Use to avoid the rounded effect in the Decimal.tryParse
        /// </summary>
        /// <param name="pStr"></param>
        /// <returns></returns>
        internal static double GetDoubleFromString(string pStr)
        {
            TraceLogs.AddInfo($"GetDoubleFromString: Removing the excedent of the string to limit the length for decimal number");

            double lResult = default;

            try
            {
                bool lHasExponential = (pStr.Contains("E") || pStr.Contains("e"));

                if (lHasExponential)
                {
                    lResult = double.Parse(pStr, NumberStyles.Float, null);
                }
                else
                {
                    lResult = double.Parse(pStr, NumberStyles.Any, CultureInfo.InvariantCulture);
                }
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError("GetDoubleFromString:\n" + e.Message.ToString());
            }

            return lResult;
        }

        #endregion

        #region Maths compute operation

        /// <summary>
        /// Compute the chunk formula from a chunk sequence
        /// </summary>
        internal void DoCompute(out string pResult)
        {
            TraceLogs.AddInfo($"DoCompute: Compute the chunk formula from a chunk {Chunk.SB} sequence");
            pResult = default;

            try
            {
                ExtractArithmeticsGroups(out double lLeftOperand, out double lRightOperand, out EOperation? lOperator);
                TraceLogs.AddInfo($"DoCompute: Compute the chunk formula from a chunk {Chunk.SB} sequence");

                if (double.IsNaN(lLeftOperand) || double.IsNaN(lRightOperand) || lOperator == null)
                {
                    TraceLogs.AddWarning($"Do compute: operands are not consistents (leftOperand: {lLeftOperand}, rightOperand: {lRightOperand}, operator: {lOperator}) !!!");
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
                TraceLogs.AddInfo($"DoCompute: Compute result: {pResult}");
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError("DoCompute:\n" + e.Message.ToString());
            }
        }

        private EOperation mOperator;

        /// <summary>
        /// Get the operator from the string
        /// </summary>
        internal EOperation WhatOperator(char pOperator)
        {
            TraceLogs.AddInfo($"WhatOperator: Get the operator from the string input");

            try
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
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError("WhatOperator:\n" + e.Message.ToString());
            }

            TraceLogs.AddInfo($"WhatOperator: Operator is {mOperator}");

            return mOperator;
        }

        /// <summary>
        /// Replace the chunk sequence by the result into the main formula
        /// </summary>
        /// <param name="lResult"></param>
        internal void DoReplaceByResult(string pResult)
        {
            TraceLogs.AddInfo($"DoReplaceByResult: Replacing the chunk sequence {Chunk.SB} by the result {pResult}");

            try
            {
                // Check if compute if finish, return if yes
                Regex lRegex = new Regex(lFinishedComputationPattern);
                Match lMatch = lRegex.Match(pResult);
                TraceLogs.AddTechnical($"DoReplaceByResult: Checking if compute is finish\n Regex Pattern: {lFinishedComputationPattern}");

                TraceLogs.AddInfo($"DoReplaceByResult: Finding operator to continue: {lMatch.Success}");

                if (!lMatch.Success)
                {
                    TraceLogs.AddInfo($"DoReplaceByResult: Replacing the main formula ({Chunk.Formula}) by the result");

                    Chunk.Formula.Remove(Chunk.StartIndex, Chunk.Length);
                    Chunk.Formula.Insert(Chunk.StartIndex, pResult);
                    string lTmp = Chunk.Formula.ToString();
                    Chunk.SB = new StringBuilder(lTmp);

                    TraceLogs.AddInfo($"DoReplaceByResult: Main formula finaly: ({Chunk.Formula})");
                    return;
                }
            
                TraceLogs.AddInfo($"DoReplaceByResult: Replacing the chunk ({Chunk.SB})");
                Chunk.SB.Remove(Chunk.StartIndex, Chunk.Length);
                Chunk.SB.Insert(Chunk.StartIndex, pResult);
                TraceLogs.AddInfo($"DoReplaceByResult: chunk finaly: ({Chunk.SB})");
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                TraceLogs.AddError("DoReplaceByResult:\n" + e.Message.ToString());
            }
        }

#endregion
    }
}

