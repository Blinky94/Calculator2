﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            mParenthesis = new char[] { '(', ')', '{', '}', '[', ']' };
            mOperators = new char[] { '+', '-', '×', 'x', 'X', '*', '/', '÷', '^', '√' };
            mComa = new char[] { '.', ',' };
            mSpecials = new string[] { "E+", "e+" };

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
                ComputeParenthesis();
                ComputeExponent();
                ComputeMultAndDiv();
                ComputeAddAndSub();
                DoCompute(out decimal lResult);
                DoReplaceByResult(lResult);
            }

            return Chunk.SB.ToString();
        }

        #region Parenthesis

        internal void ComputeParenthesis()
        {
            // Regex to select all parenthesis groups
            string lPattern = @"[({\[](?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)((?<Operator>[+\-÷\/×xX*])(?(?=[({\[][-])[({\[]+[-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]+|[√]?\d+[.,]?\d*([Ee][+]\d*)?))+[)}\]]";


        }

        #endregion

        #region Exponent

        internal void ComputeExponent()
        {
            // Regex to select all exponents groups
            string lPattern = @"(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)[\^]+[({\[](?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)[)}\]]";



        }

        #endregion

        #region Multiplication/Division

        internal void ComputeMultAndDiv()
        {
            // Regex to select all multiplication and division groups
            string lPattern = @"(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)[×xX*÷\/]+(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)";


        }

        #endregion

        #region Addition/Substraction 

        internal void ComputeAddAndSub()
        {
            // Regex to select all addition and substraction groups
            string lPattern = @"(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)[+-]+(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?)";



        }

        #endregion

        #region ExtractOperands

        /// <summary>
        /// Extract each part of a simple formula
        /// </summary>
        /// <param name="pLeftOperand"></param>
        /// <param name="pRightOperand"></param>
        /// <param name="pOperator"></param>
        public void ExtractArithmeticsGroups(out decimal pLeftOperand, out decimal pRightOperand, out char pOperator)
        {
            string lPattern = @"(?<LeftOperand>(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?))(?<Operator>[+\-÷\/×xX*])(?<RightOperand>(?(?=[({\[][-])[({\[][-][√]?\d+[.,]?\d*([Ee][+]\d*)?[)}\]]|[√]?\d+[.,]?\d*([Ee][+]\d*)?))";

            Regex regex = new Regex(lPattern);

            Match lMatch = regex.Match(Chunk.SB.ToString());

            decimal.TryParse(lMatch.Groups["LeftOperand"].Value.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out pLeftOperand);
            char.TryParse(lMatch.Groups["Operator"].Value, out pOperator);
            decimal.TryParse(lMatch.Groups["RightOperand"].Value.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out pRightOperand);
        }

        #endregion

        #region TrimLengthString

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

        #endregion

        #region Maths compute operation

        /// <summary>
        /// Compute the chunk formula from a chunk sequence
        /// </summary>
        internal void DoCompute(out decimal pResult)
        {
            pResult = default;

            // Extraction units from formula
            decimal a = 0;
            decimal b = 0;

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
            string lOperator = string.Empty;

            switch (lOperator)
            {
                case "^":
                case "Exp":
                    Operator = Operator.Exponent;
                    break;
                case "×":
                case "x":
                case "X":
                case "*":
                    Operator = Operator.Multiplication;
                    break;
                case "÷":
                case "/":
                    Operator = Operator.Division;
                    break;
                case "+":
                    Operator = Operator.Addition;
                    break;
                case "-":
                    Operator = Operator.Substraction;
                    break;
                case "√":
                case "Sqrt":
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

