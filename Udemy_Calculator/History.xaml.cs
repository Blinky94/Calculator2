using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : UserControl
    {
        private static double mHistoryWidth = 200d;
        private const double mMainWindowWidth = 260d;
        private GridLength mExpandedWidth = new GridLength(1, GridUnitType.Star);
        private DisplayHistory mDisplayHistory;

        public History()
        {
            InitializeComponent();
            mDisplayHistory = new DisplayHistory();
        }

        public string ReturnFormula()
        {
            return mDisplayHistory.FormulaStr;
        }

        private void UICleaner_Click(object sender, RoutedEventArgs e)
        {
            mDisplayHistory.CleanHistory(ref UIHistoryTextBox);
        }

        private void UIHistoryExpander_Expanded(object sender, RoutedEventArgs e)
        {
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
            Application.Current.MainWindow.Width = mMainWindowWidth;
            mExpandedWidth = UIHistoryExpanderColumn.Width;
            UIHistoryExpanderColumn.Width = GridLength.Auto;

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

        internal void AppendElement(string pStr, bool pIsResult = false, string pNum = default)
        {
            if (!pIsResult)
            {
                mDisplayHistory.AppendHistoryFormula(pStr, UIHistoryTextBox, pIsResult, pNum);
            }
            else
            {
                if (pNum != default)
                {
                    mDisplayHistory.AppendHistoryFormula(pStr, UIHistoryTextBox, pIsResult, pNum);
                }
                else
                {
                    mDisplayHistory.AppendHistoryResult(pStr, UIHistoryTextBox);
                }
            }
        }

        internal void RemoveElement(int pLength)
        {
            mDisplayHistory.RemoveHistoryFormula(pLength);
        }

        private void UIHistoryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UIHistoryScrollViewer.ScrollToEnd();
        }
    }
}
