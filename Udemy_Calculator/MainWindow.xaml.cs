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
using System.Windows.Media;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private decimal mLastNumber;
        private SelectedOperator mSelectedOperator;
        private string mSpecialSymbols = "×÷+-";
        private bool mIsResult = false;
        private DisplayHistory mDisplayHistory;

        public MainWindow()
        {
            InitializeComponent();

            mDisplayHistory = new DisplayHistory();
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
            }
        }

        private void UIACButton_Click(object sender, RoutedEventArgs e)
        {
            UIResultLabel.Content = "0";
            mDisplayHistory.AddNewHistory();
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
                            UIResultLabel.Content = SimpleMath.Add(mLastNumber, lNewNumber);
                            break;
                        case SelectedOperator.Substraction:
                            UIResultLabel.Content = SimpleMath.Substract(mLastNumber, lNewNumber);
                            break;
                        case SelectedOperator.Multiplication:
                            UIResultLabel.Content = SimpleMath.Multiply(mLastNumber, lNewNumber);
                            break;
                        case SelectedOperator.Division:
                            UIResultLabel.Content = SimpleMath.Divide(mLastNumber, lNewNumber);
                            break;
                    }

                    mIsResult = true;
                    mDisplayHistory.AddNewHistory();
                    decimal.TryParse(UIResultLabel.Content.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out mLastNumber);
                    mDisplayHistory.AppendHistoryResult(UIResultLabel.Content.ToString(), UIHistoryTextBox);
                    mDisplayHistory.AddNewHistory();
                }
            }
        }

        private void UIDetailsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UIHistoryScrollViewer.ScrollToEnd();
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Substraction,
        Multiplication,
        Division
    }
}
