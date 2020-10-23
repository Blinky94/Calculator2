using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Udemy_Calculator
{
    public class DisplayHistory
    {
        private Paragraph mParagraph;
        FlowDocument mFlowDocument;
        private readonly float mLineHeight = 1;

        public string FormulaStr { get; private set; }
        public string ResultStr { get; private set; }

        public DisplayHistory()
        {
            mFlowDocument = new FlowDocument();
            AddNewHistory();
        }

        /// <summary>
        /// Append history
        /// </summary>
        /// <param name="pText"></param>
        /// <param name="pUITextBox"></param>
        /// <returns></returns>
        private Block AppendHistory(string pText, ref RichTextBox pUITextBox)
        {
            pText = pText.Replace(',', '.');
            mParagraph.Inlines.Add(new Run(pText));
            mFlowDocument.LineHeight = mLineHeight;
            mFlowDocument.Blocks.Add(mParagraph);
            pUITextBox.Document = mFlowDocument;

            FormulaStr += pText;

            return pUITextBox.Document.Blocks.LastOrDefault();
        }

        /// <summary>
        /// Append the formula to the history panel
        /// </summary>
        /// <param name="pText"></param>
        /// <param name="pUITextBox"></param>
        /// <param name="pIsResult"></param>
        /// <param name="pLastNumber"></param>
        /// <param name="pIsDetail"></param>
        public void AppendHistoryFormula(string pText, RichTextBox pUITextBox, Style pStyle, string pLastNumber = default, bool pIsResult = false)
        {
            double.TryParse(pText, NumberStyles.Any, CultureInfo.InvariantCulture, out double lTextSigned);

            if (Math.Sign(lTextSigned) == -1)
            {
                RemoveHistoryFormula(pText.Length - 1);
                pText = $"({pText})";
            }

            if (pIsResult)
            {
                pText = $"{pLastNumber}{pText}";
            }

            Block lCurrentBlock = AppendHistory(pText, ref pUITextBox);
            lCurrentBlock.Style = pStyle;         
        }

        /// <summary>
        /// Append the result to the history panel
        /// </summary>
        /// <param name="pText"></param>
        /// <param name="pUITextBox"></param>
        public void AppendHistoryResult(string pText, RichTextBox pUITextBox, Style pStyle)
        {
            Block lCurrentBlock = AppendHistory(pText, ref pUITextBox);
            lCurrentBlock.Style = pStyle;
        }

        /// <summary>
        /// Remove a part of the formula depending of the length to remove
        /// </summary>
        /// <param name="pUILength"></param>
        public void RemoveHistoryFormula(int pUILength)
        {
            var textRange = new TextRange(mParagraph.ContentStart, mParagraph.ContentEnd);

            if (!textRange.IsEmpty)
            {
                string lOutput = textRange.Text.Substring(0, textRange.Text.Length - pUILength);
                mParagraph.Inlines.Clear();
                mParagraph.Inlines.Add(new Run(lOutput));

                FormulaStr = lOutput;
            }
        }

        /// <summary>
        /// Add a new blank history
        /// </summary>
        public void AddNewHistory()
        {
            mParagraph = new Paragraph();
            FormulaStr = string.Empty;
        }

        /// <summary>
        /// Erase everything in the history panel
        /// </summary>
        /// <param name="pUI"></param>
        internal void CleanHistory(ref RichTextBox pUI)
        {
            pUI.Document.Blocks.Clear();
            mFlowDocument = new FlowDocument();
            AddNewHistory();
            FormulaStr = string.Empty;
        }
    }
}
