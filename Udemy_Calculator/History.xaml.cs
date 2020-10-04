using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : UserControl
    {
        private const double mHistoryWidth = 200d;
        private const double mMainWindowWidth = 260d;
        private GridLength mExpandedWidth = new GridLength(1, GridUnitType.Star);
        private readonly DisplayHistory mDisplayHistory;

        public History()
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

        private void UIHistoryExpander_Expanded(object sender, RoutedEventArgs e)
        {
            TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: Expand the history panel");
            Application.Current.MainWindow.Width = mMainWindowWidth + mHistoryWidth;
            UIHistoryExpanderColumn.Width = mExpandedWidth;

            if (HistoryGrid != null)
            {
                HistoryGrid.Visibility = Visibility.Visible;
                UICleaner.Visibility = Visibility.Visible;
            }
        }

        private void UIHistoryExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: Collapse the history panel");
            Application.Current.MainWindow.Width = mMainWindowWidth;
            mExpandedWidth = UIHistoryExpanderColumn.Width;
            UIHistoryExpanderColumn.Width = new GridLength(0);

            if (HistoryGrid != null)
            {
                HistoryGrid.Visibility = Visibility.Collapsed;
                UICleaner.Visibility = Visibility.Collapsed;
            }
        }

        internal void NewElement()
        {
            mDisplayHistory.AddNewHistory();
        }

        internal void AppendElement(string pStr, bool pIsResult = false, string pNum = default, bool pIsDetail = false)
        {
            if (!pIsResult)
            {
                TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: Append {pStr}, is result: {pIsResult}, number: {pNum}");
                mDisplayHistory.AppendHistoryFormula(pStr, UIHistoryTextBox, pIsResult, pNum, pIsDetail);
            }
            else
            {
                if (pNum != default)
                {
                    TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: Append into the formula the last result {pNum}");
                    mDisplayHistory.AppendHistoryFormula(pStr, UIHistoryTextBox, pIsResult, pNum, pIsDetail);
                }
                else
                {
                    TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: Append the result {pStr}");
                    mDisplayHistory.AppendHistoryResult(pStr, UIHistoryTextBox);
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

        private void CheckBoxWithDetails_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBoxWithDetails_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
