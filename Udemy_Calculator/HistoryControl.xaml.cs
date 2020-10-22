using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class HistoryControl : UserControl
    {
        private readonly DisplayHistory mDisplayHistory;

        public HistoryControl()
        {
            InitializeComponent();
            mDisplayHistory = new DisplayHistory();
        }

        public string ReturnFormula()
        {
            TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: returning the formula: {mDisplayHistory.FormulaStr}");
            return mDisplayHistory.FormulaStr;
        }

        private void UICleaner_Click(object sender, RoutedEventArgs e)
        {
            TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: Cleaning history");
            mDisplayHistory.CleanHistory(ref UIHistoryTextBox);
        }

        internal void NewElement()
        {
            mDisplayHistory.AddNewHistory();
        }

        internal void AppendElement(string pStr, bool pIsResult = false, string pNum = default, bool pIsDetail = false)
        {
            Style lStyle = pIsDetail ? (Style)FindResource("HistoryChunkStyle") : (Style)FindResource("HistoryFormulaStyle");

            if (!pIsResult)
            {
                TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: Append {pStr}, is result: {pIsResult}, number: {pNum}");

               
                mDisplayHistory.AppendHistoryFormula(pStr, UIHistoryTextBox, lStyle, pNum, pIsResult);
            }
            else
            {
                if (pNum != default)
                {
                    TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: Append into the formula the last result {pNum}");
                    mDisplayHistory.AppendHistoryFormula(pStr, UIHistoryTextBox, lStyle, pNum, pIsResult);
                }
                else
                {
                    TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: Append the result {pStr}");
                    mDisplayHistory.AppendHistoryResult(pStr, UIHistoryTextBox, (Style)FindResource("HistoryResultStyle"));
                }
            }
        }

        internal void RemoveElement(int pLength)
        {
            TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: removing element from length: {pLength}");
            mDisplayHistory.RemoveHistoryFormula(pLength);
        }

        private void UIHistoryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UIHistoryScrollViewer.ScrollToEnd();
        }

        private void UISave_Click(object sender, RoutedEventArgs e)
        {
            TraceLogs.AddInfo($"{GlobalUsage.GetCurrentMethodName}: Saving history logs");
            GlobalUsage.SaveToFile(UIHistoryTextBox);
        }
    }
}
