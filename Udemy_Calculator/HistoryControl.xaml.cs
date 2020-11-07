﻿using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator
{
    /// <summary>
    /// Enum for the kind of paragraph
    /// </summary>
    public enum ParagraphType
    {
        /// <summary>
        /// Type of formula
        /// </summary>
        Formula,
        /// <summary>
        /// Type of chunk
        /// </summary>
        Chunk,
        /// <summary>
        /// Type of result
        /// </summary>
        Result
    }

    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class HistoryControl : UserControl
    {
        private readonly DisplayHistory mDisplayHistory;

        public HistoryControl()
        {
            InitializeComponent();
            mDisplayHistory = new DisplayHistory(this);
            Cleaner_GeneralButtonControl.OnGeneralButtonClicked += new RoutedEventHandler(UICleaner_Click);
            Save_GeneralButtonControl.OnGeneralButtonClicked += new RoutedEventHandler(UISave_Click);
        }

        /// <summary>
        /// Return the formula
        /// </summary>
        /// <returns></returns>
        public string ReturnFormula()
        {
            TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: returning the formula: {mDisplayHistory.FormulaStr}");
            return mDisplayHistory.FormulaStr;
        }

        /// <summary>
        /// Return the last operand in the formula (Nomber, signed number, with coma, square, parenthesis...)
        /// </summary>
        /// <returns></returns>
        public string ReturnLastOperandInTheFormula()
        {
            TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: returning last numerical part of the formula: {mDisplayHistory.FormulaStr}");

            Regex regex = new Regex(GlobalUsage.Regexp_SeparateElementsInOperation);
            var lMatches = regex.Matches(mDisplayHistory.FormulaStr);

            if (lMatches.Count > 0)
            {
                string ffff = lMatches[lMatches.Count - 1].ToString();
                Match lMatch = regex.Match(ffff);
                if (lMatch.Success)
                {
                    string lll = lMatch.Groups["RightOperand"].Value;

                    return lll;
                }
            }

            TraceLogs.AddWarning($"{GlobalUsage.GetCurrentMethodName}: No operand in the current formula: {mDisplayHistory.FormulaStr}");

            return string.Empty;
        }

        /// <summary>
        /// Insert a new paragraph to the History panel
        /// </summary>
        internal void InsertNewParagraph(ParagraphType pTypeParagraph)
        {
            mDisplayHistory.AddNewHistoryParagraph(pTypeParagraph);
        }

        /// <summary>
        /// Adding a new element to the paragraph
        /// </summary>
        /// <param name="pStr"></param>
        /// <param name="pIsResult"></param>
        /// <param name="pNum"></param>
        /// <param name="pIsDetail"></param>
        internal void AppendElement(string pStr, bool pIsResult = false, string pNum = default, bool pIsDetail = false)
        {
            if (!pIsResult)
            {
                if (pIsDetail)
                {
                    // Chunk detail calculus
                    TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: Append chunk element {pStr}");
                    mDisplayHistory.AppendHistoryChunk(pStr);
                }
                else
                {
                    // Formula calculus
                    TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: Append formula element {pStr}");
                    mDisplayHistory.AppendHistoryFormula(pStr);
                }
            }
            else
            {
                if (pNum != default)
                {
                    // Formula calculus added to the last result
                    TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: Append formula element {pStr}");
                    mDisplayHistory.AppendHistoryFormula(pStr, pNum);
                }
                else
                {
                    // Last result of calculus
                    TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: Append the result {pStr}");
                    mDisplayHistory.AppendHistoryResult(pStr);
                }
            }
        }

        /// <summary>
        /// Remove element with length to remove from string on display history
        /// </summary>
        /// <param name="pLength"></param>
        internal void RemoveElement(int pLength)
        {
            TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: Removing element from length: {pLength}");
            mDisplayHistory.RemoveHistoryFormula(pLength);
        }

        private void UIHistoryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UIHistoryScrollViewer.ScrollToEnd();
        }

        private void UICleaner_Click(object sender, RoutedEventArgs e)
        {
            TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: Cleaning history");
            mDisplayHistory.CleanHistory();
        }

        private void UISave_Click(object sender, RoutedEventArgs e)
        {
            TraceLogs.AddInfo($"{GlobalUsage.GetCurrentMethodName}: Saving history logs");
            GlobalUsage.SaveToFile(UIHistoryTextBox);
        }
    }
}
