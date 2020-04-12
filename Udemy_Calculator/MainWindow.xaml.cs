using System;
using System.Data;
using System.Globalization;
using System.Linq;
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
        public MainWindow()
        {
            InitializeComponent();
            AddNewDetails();
        }

        private double mLastNumber;
        private SelectedOperator mSelectedOperator;
        private string mSpecialSymbols = "×÷+-";
        private Paragraph mParagraph;
        private bool mIsResult = false;

        private void AddNewDetails()
        {
            mParagraph = new Paragraph();
        }

        private Block AppendDetails(string pText)
        {
            pText = pText.Replace(',', '.');
            mParagraph.Inlines.Add(new Run(pText));
            UIDetailsFlowDoc.Blocks.Add(mParagraph);
            UIdetailsTextBox.Document = UIDetailsFlowDoc;

            return UIdetailsTextBox.Document.Blocks.LastOrDefault();
        }

        private void AppendDetailsResult(string pText)
        {
            Block lCurrentBlock = AppendDetails(pText);
            lCurrentBlock.TextAlignment = TextAlignment.Right;
            lCurrentBlock.Foreground = Brushes.LightSalmon;
        }

        private void AppendDetailsFormula(string pText)
        {
            if (mIsResult)
            {
                pText = $"{mLastNumber}{pText}";
            }

            Block lCurrentBlock = AppendDetails(pText);
            lCurrentBlock.TextAlignment = TextAlignment.Left;
            lCurrentBlock.Foreground = Brushes.LightGreen;
        }

        private void ReplaceDetailsFormula(string pText)
        {           
            if (mIsResult)
            {
                pText = $"{mLastNumber}{pText}";
            }

            Block lCurrentBlock = AppendDetails(pText);
            lCurrentBlock.TextAlignment = TextAlignment.Left;
            lCurrentBlock.Foreground = Brushes.LightGreen;
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

            AppendDetailsFormula(lNumber.ToString());
        }

        private void UIPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!UIResultLabel.Content.ToString().Contains('.'))
            {
                UIResultLabel.Content = $"{UIResultLabel.Content}.";
                AppendDetailsFormula((e.Source as Button).Content.ToString());
            }
        }

        private void UIACButton_Click(object sender, RoutedEventArgs e)
        {
            UIResultLabel.Content = "0";
            AddNewDetails();
        }

        private double TransformCoef(string pButtonPressed)
        {
            switch (pButtonPressed)
            {
                case "+/1":
                    return -1;

                case "%":
                    return 0.01;
            }

            return default;
        }

        private void UIMultiplyCoefButton_Click(object sender, RoutedEventArgs e)
        {
            double lTransformCoef = TransformCoef((e.Source as Button).Content.ToString());
            double lResult;

            if (double.TryParse(UIResultLabel.Content.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out lResult))
            {
                UIResultLabel.Content = lResult * lTransformCoef;
                ReplaceDetailsFormula(UIResultLabel.Content.ToString());
            }
        }

        private void UIOperatorButton_Click(object sender, RoutedEventArgs e)
        {
            AppendDetailsFormula((e.Source as Button).Content.ToString());

            double.TryParse(UIResultLabel.Content.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out mLastNumber);
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

        private void UIEqualButton_Click(object sender, RoutedEventArgs e)
        {
            double lNewNumber;
            if (double.TryParse(UIResultLabel.Content.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out lNewNumber))
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
                AddNewDetails();
                double.TryParse(UIResultLabel.Content.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out mLastNumber);
                AppendDetailsResult(UIResultLabel.Content.ToString());
                AddNewDetails();
            }
        }

        private void UIDetailsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UIDetailsScrollViewer.ScrollToEnd();
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
