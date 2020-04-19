using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private decimal mLastNumber;
        private SelectedOperator mSelectedOperator = SelectedOperator.None;
        private string mSpecialSymbols = "×÷+-";
        private bool mIsResult = false;
        private DisplayHistory mDisplayHistory;
        private static double mHistoryWidth = 200d;
        private const double mMainWindowWidth = 360d;
        private const double mMainWindowHeight = 390d;
        private GridLength mExpandedWidth = new GridLength(1, GridUnitType.Star);
        private DispatcherTimer mTimer;
        private double mPanelWidth;
        private bool mHidden;

        public MainWindow()
        {
            InitializeComponent();
            mDisplayHistory = new DisplayHistory();
            mTimer = new DispatcherTimer();
            mTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            mTimer.Tick += Timer_Tick;
            mPanelWidth = MenuSidePanel.Width;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (mHidden)
            {
                MenuSidePanel.Width += 1;
                if (MenuSidePanel.Width >= mPanelWidth)
                {
                    mTimer.Stop();
                    mHidden = false;
                }
            }
            else
            {
                MenuSidePanel.Width -= 1;
                if (MenuSidePanel.Width <= 35)
                {
                    mTimer.Stop();
                    mHidden = true;
                }
            }
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

        private void UINumberButton_Click(object sender, RoutedEventArgs e)
        {
            double lNumber;
            double.TryParse((e.Source as Button).Content.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out lNumber);

            if (UIResultLabel.Content.ToString() == "0" || mSpecialSymbols.Contains(UIResultLabel.Content.ToString()) || mIsResult)
            {
                UIResultLabel.Content = lNumber;
                mIsResult = false;
            }
            else
            {
                UIResultLabel.Content = $"{UIResultLabel.Content}{lNumber}";
            }

            mDisplayHistory.AppendHistoryFormula(lNumber.ToString(), UIHistoryTextBox, mIsResult, mLastNumber);
        }

        private void UIPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!UIResultLabel.Content.ToString().Replace(',', '.').Contains('.'))
            {
                UIResultLabel.Content = $"{UIResultLabel.Content}.";
                mDisplayHistory.AppendHistoryFormula((e.Source as Button).Content.ToString(), UIHistoryTextBox, mIsResult, mLastNumber);
                mIsResult = false;
            }
        }

        private void UIACButton_Click(object sender, RoutedEventArgs e)
        {
            UIResultLabel.Content = "0";
            mDisplayHistory.AddNewHistory();
            mLastNumber = 0;
            mDisplayHistory.AppendHistoryFormula(string.Empty, UIHistoryTextBox);
        }

        private decimal TransformCoef(string pButtonPressed)
        {
            switch (pButtonPressed)
            {
                case "+/1":
                    return -1;

                case "%":
                    return 0.01m;
            }

            return default;
        }

        private void UIMultiplyCoefButton_Click(object sender, RoutedEventArgs e)
        {
            decimal lTransformCoef = TransformCoef((e.Source as Button).Content.ToString());
            decimal lResult;

            if (decimal.TryParse(UIResultLabel.Content.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out lResult))
            {
                if (!mIsResult)
                {
                    mDisplayHistory.RemoveHistoryFormula(UIResultLabel.Content.ToString().Length);
                    UIResultLabel.Content = lResult * lTransformCoef;
                }
                else
                {
                    mIsResult = false;
                    UIResultLabel.Content = mLastNumber * lTransformCoef;
                }

                mDisplayHistory.AppendHistoryFormula(UIResultLabel.Content.ToString(), UIHistoryTextBox, mIsResult, mLastNumber);
            }
        }

        private void UIOperatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mSpecialSymbols.Contains(UIResultLabel.Content.ToString().LastOrDefault()))
            {
                mDisplayHistory.AppendHistoryFormula((e.Source as Button).Content.ToString(), UIHistoryTextBox, mIsResult, mLastNumber);

                decimal.TryParse(UIResultLabel.Content.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out mLastNumber);
                UIResultLabel.Content = (e.Source as Button).Content.ToString();

                if (sender == UIPlusButton)
                {
                    mSelectedOperator = SelectedOperator.Addition;
                }
                else if (sender == UIMinusButton)
                {
                    mSelectedOperator = SelectedOperator.Substraction;
                }
                else if (sender == UIMultiplyButton)
                {
                    mSelectedOperator = SelectedOperator.Multiplication;
                }
                else if (sender == UIDivideButton)
                {
                    mSelectedOperator = SelectedOperator.Division;
                }
            }
        }

        private void UIEqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mIsResult)
            {
                decimal lNewNumber;
                if (decimal.TryParse(UIResultLabel.Content.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out lNewNumber))
                {
                    switch (mSelectedOperator)
                    {
                        case SelectedOperator.Addition:
                            UIResultLabel.Content = MathCompute.Add(mLastNumber, lNewNumber);
                            break;
                        case SelectedOperator.Substraction:
                            UIResultLabel.Content = MathCompute.Substract(mLastNumber, lNewNumber);
                            break;
                        case SelectedOperator.Multiplication:
                            UIResultLabel.Content = MathCompute.Multiply(mLastNumber, lNewNumber);
                            break;
                        case SelectedOperator.Division:
                            UIResultLabel.Content = MathCompute.Divide(mLastNumber, lNewNumber);
                            break;
                    }

                    mDisplayHistory.AppendHistoryFormula((e.Source as Button).Content.ToString(), UIHistoryTextBox, mIsResult, mLastNumber);
                    mIsResult = true;
                    mDisplayHistory.AddNewHistory();
                    decimal.TryParse(UIResultLabel.Content.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out mLastNumber);
                    mDisplayHistory.AppendHistoryResult(UIResultLabel.Content.ToString(), UIHistoryTextBox);
                    mDisplayHistory.AddNewHistory();
                }
            }
        }

        private void UIHistoryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UIHistoryScrollViewer.ScrollToEnd();
        }

        private void UICleaner_Click(object sender, RoutedEventArgs e)
        {
            mDisplayHistory.CleanHistory(ref UIHistoryTextBox);
        }

        private void MenuSideButton_Click(object sender, RoutedEventArgs e)
        {
            mTimer.Start();
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Substraction,
        Multiplication,
        Division,
        None
    }
}
