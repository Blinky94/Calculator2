using System;
using System.Globalization;
using System.Windows;
using System.Windows.Documents;

namespace Udemy_Calculator
{
    public class DisplayHistory
    {
        private readonly HistoryControl mHistoryControl;

        /// <summary>
        /// Return the formula string
        /// </summary>
        public string FormulaStr { get; private set; }

        /// <summary>
        /// Return the chunk string
        /// </summary>
        public string ChunkStr { get; private set; }

        /// <summary>
        /// Return the result string
        /// </summary>
        public string ResultStr { get; private set; }

        Paragraph mFormulaParagraph;
        Paragraph mChunkParagraph;
        Paragraph mResultParagraph;

        public DisplayHistory(HistoryControl pHistoryControl)
        {
            // Control recuperation for operations on it
            mHistoryControl = pHistoryControl;

            mFormulaParagraph = new Paragraph();
            mChunkParagraph = new Paragraph();
            mResultParagraph = new Paragraph();
        }

        /// <summary>
        /// Append history
        /// </summary>
        /// <param name="pText"></param>
        /// <param name="pParagraph"></param>
        private void AppendHistory(string pText, Paragraph pParagraph)
        {
            pText = pText.Replace(',', '.');
            pParagraph.Inlines.Add(new Run(pText));
            mHistoryControl.UIHistoryFlowDocument.Blocks.Add(pParagraph);

            if (pParagraph.Name == "UIHistoryFormulaParagraph")
            {
                FormulaStr += pText;
                return;
            }
            if (pParagraph.Name == "UIHistoryChunkParagraph")
            {
                ChunkStr += pText;
                return;
            }
            if (pParagraph.Name == "UIHistoryResultParagraph")
            {
                ResultStr += pText;
                return;
            }
        }

        /// <summary>
        /// Appying parenthesis on the current text if necessary
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        private string ApplyingParentheses(string pText)
        {
            double.TryParse(pText, NumberStyles.Any, CultureInfo.InvariantCulture, out double lTextSigned);

            if (Math.Sign(lTextSigned) == -1)
            {
                RemoveHistoryFormula(pText.Length - 1);
                pText = $"({pText})";
            }

            return pText;
        }

        /// <summary>
        /// Append chunk to the history panel
        /// </summary>
        /// <param name="pText"></param>
        public void AppendHistoryChunk(string pText)
        {
            mChunkParagraph.Style = Application.Current.FindResource("HistoryChunkStyle") as Style;
            mChunkParagraph.Name = "UIHistoryChunkParagraph";
            AppendHistory(ApplyingParentheses(pText), mChunkParagraph);
        }

        /// <summary>
        /// Append the formula to the history panel
        /// </summary>
        /// <param name="pText"></param>
        public void AppendHistoryFormula(string pText, string lLastResult = "")
        {
            mFormulaParagraph.Style = Application.Current.FindResource("HistoryFormulaStyle") as Style;
            mFormulaParagraph.Name = "UIHistoryFormulaParagraph";

            if (!string.IsNullOrEmpty(lLastResult))
            {
                pText = $"{lLastResult}{pText}";
            }

            AppendHistory(pText, mFormulaParagraph);
        }

        /// <summary>
        /// Append the result to the history panel
        /// </summary>
        /// <param name="pText"></param>
        public void AppendHistoryResult(string pText)
        {
            mResultParagraph.Style = Application.Current.FindResource("HistoryResultStyle") as Style;
            mResultParagraph.Name = "UIHistoryResultParagraph";
            AppendHistory(ApplyingParentheses(pText), mResultParagraph);
        }

        /// <summary>
        /// Remove a part of the formula depending of the length to remove
        /// </summary>
        /// <param name="pUILength"></param>
        public void RemoveHistoryFormula(int pUILength)
        {
            var textRange = new TextRange(mFormulaParagraph.ContentStart, mFormulaParagraph.ContentEnd);

            if (!textRange.IsEmpty)
            {
                string lOutput = textRange.Text.Substring(0, textRange.Text.Length - pUILength);
                mFormulaParagraph.Inlines.Clear();
                mFormulaParagraph.Inlines.Add(new Run(lOutput));

                FormulaStr = lOutput;
            }
        }

        /// <summary>
        /// Add a new paragraph, depending of his type (Formula, Chunk, Result)
        /// </summary>
        public void AddNewHistoryParagraph(ParagraphType pType)
        {
            switch (pType)
            {
                case ParagraphType.Formula:
                    FormulaStr = string.Empty;
                    mFormulaParagraph = new Paragraph();
                    break;

                case ParagraphType.Chunk:
                    ChunkStr = string.Empty;
                    mChunkParagraph = new Paragraph();
                    break;

                case ParagraphType.Result:
                    ResultStr = string.Empty;
                    mResultParagraph = new Paragraph();
                    break;
            }
        }

        /// <summary>
        /// Erase everything in the history panel
        /// </summary>
        /// <param name="pUI"></param>
        internal void CleanHistory()
        {
            mHistoryControl.UIHistoryTextBox.Document.Blocks.Clear();
        }
    }
}
