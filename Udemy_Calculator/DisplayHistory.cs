using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Udemy_Calculator
{
    public class DisplayHistory
    {
        private Paragraph mParagraph;
        FlowDocument mFlowDocument;
        private float mLineHeight = 1;

        public string FormulaStr { get; private set; }
        public string ResultStr { get; private set; }

        public DisplayHistory()
        {
            mFlowDocument = new FlowDocument();
            AddNewHistory();
        }

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

        public void AppendHistoryFormula(string pText, RichTextBox pUITextBox, bool pIsResult = false, decimal pLastNumber = default)
        {
            if (pIsResult)
            {
                pText = $"{pLastNumber}{pText}";
            }

            Block lCurrentBlock = AppendHistory(pText, ref pUITextBox);
            lCurrentBlock.TextAlignment = TextAlignment.Left;
            lCurrentBlock.Foreground = Brushes.LightGreen;
        }

        public void AppendHistoryResult(string pText, RichTextBox pUITextBox)
        {
            Block lCurrentBlock = AppendHistory(pText, ref pUITextBox);
            lCurrentBlock.TextAlignment = TextAlignment.Right;
            lCurrentBlock.Foreground = Brushes.LightSalmon;
        }

        public void RemoveHistoryFormula(int pUILength)
        {
            var textRange = new TextRange(mParagraph.ContentStart, mParagraph.ContentEnd);

            if (!textRange.IsEmpty)
            {
                string lOutput = textRange.Text.Substring(0, textRange.Text.Count() - pUILength);
                mParagraph.Inlines.Clear();
                mParagraph.Inlines.Add(new Run(lOutput));

                FormulaStr += lOutput;
            }
        }

        public void AddNewHistory()
        {
            mParagraph = new Paragraph();
            FormulaStr = string.Empty;
        }

        internal void CleanHistory(ref RichTextBox pUI)
        {
            pUI.Document.Blocks.Clear();
            mFlowDocument = new FlowDocument();
            AddNewHistory();
            FormulaStr = string.Empty;
        }
    }
}
