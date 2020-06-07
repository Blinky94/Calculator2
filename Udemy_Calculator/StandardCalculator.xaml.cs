using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for StandardCalculator.xaml
    /// </summary>
    public partial class StandardCalculator : UserControl
    {
        private double mLastNumber;
        private string mSpecialSymbols = "×÷+-";
        private bool mIsResult = false;
        private History mHistory;
        private PEMDAS mPemdas;

        public StandardCalculator()
        {
            InitializeComponent();

            mHistory = new History();
            mHistory.SetValue(Grid.RowProperty, 1);
            mHistory.SetValue(Grid.ColumnProperty, 1);
            mHistory.SetValue(Grid.ColumnSpanProperty, 6);
            mHistory.SetValue(Grid.RowSpanProperty, 7);
            mHistory.SetValue(Grid.MarginProperty, new Thickness(0, -25, 0, 0));
            UIStandardCalculator.Children.Add(mHistory);
        }

        private void UINumberButton_Click(object sender, RoutedEventArgs e)
        {
            string lInput = (e.Source as Button).Content.ToString().Replace(',', '.');

            double.TryParse(lInput, NumberStyles.Any, CultureInfo.InvariantCulture, out double lNumber);

            if (UIResultLabel.Content.ToString() == "0" || mSpecialSymbols.Contains(UIResultLabel.Content.ToString()) || mIsResult)
            {
                UIResultLabel.Content = lNumber;
                mIsResult = false;
            }
            else
            {
                UIResultLabel.Content = $"{UIResultLabel.Content}{lNumber}";
            }

            lInput = lNumber.ToString().Replace(',', '.');
            mHistory.AppendElement(lInput, mIsResult, mLastNumber.ToString().Replace(',', '.'));
        }

        private void UIPointButton_Click(object sender, RoutedEventArgs e)
        {
            UIResultLabel.Content = UIResultLabel.Content.ToString().Replace(',', '.');

            if (!UIResultLabel.Content.ToString().Contains('.'))
            {
                UIResultLabel.Content = $"{UIResultLabel.Content}.";
                mHistory.AppendElement((e.Source as Button).Content.ToString(), mIsResult, mLastNumber.ToString().Replace(',', '.'));
                mIsResult = false;
            }
        }

        private void UIACButton_Click(object sender, RoutedEventArgs e)
        {
            UIResultLabel.Content = "0";
            mLastNumber = 0;
            mHistory.AppendElement(string.Empty);
        }

        private void UISignOrUnSignButton_Click(object sender, RoutedEventArgs e)
        {
            UIResultLabel.Content = UIResultLabel.Content.ToString().Replace(',', '.');

            if (double.TryParse(UIResultLabel.Content.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double lResult))
            {
                if (!mIsResult)
                {
                    mHistory.RemoveElement(UIResultLabel.Content.ToString().Length);
                    UIResultLabel.Content = (lResult * (-1)).ToString().Replace(',', '.');
                }
                else
                {
                    mIsResult = false;
                    UIResultLabel.Content = (mLastNumber * (-1)).ToString().Replace(',', '.');
                }

                string lConcateSign = UIResultLabel.Content.ToString();

                if (lConcateSign.IndexOf('-') != -1)
                {
                    lConcateSign = $"({lConcateSign})";
                }

                mHistory.AppendElement(lConcateSign, mIsResult, mLastNumber.ToString().Replace(',', '.'));
            }
        }

        private void UIPercentageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIOperatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mSpecialSymbols.Contains(UIResultLabel.Content.ToString().LastOrDefault()))
            {
                mHistory.AppendElement((e.Source as Button).Content.ToString().Replace(',', '.'), mIsResult, mLastNumber.ToString().Replace(',', '.'));

                UIResultLabel.Content = UIResultLabel.Content.ToString().Replace(',', '.');

                double.TryParse(UIResultLabel.Content.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out mLastNumber);
                UIResultLabel.Content = (e.Source as Button).Content.ToString().Replace(',', '.');
            }
        }

        private void UIEqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mIsResult)
            {
                string lFormula = mHistory.ReturnFormula();

                string lResultContent = UIResultLabel.Content.ToString();
                lResultContent = lResultContent.Replace(',', '.');

                if (double.TryParse(lResultContent, NumberStyles.Any, CultureInfo.InvariantCulture, out double lNewNumber))
                {
                    // Ici PEMDAS
                    string lResult;
                    mPemdas = new PEMDAS(lFormula);
                    lResult = mPemdas.ComputeFormula();

                    UIResultLabel.Content = lResult;
                    mHistory.AppendElement((e.Source as Button).Content.ToString(), mIsResult, mLastNumber.ToString().Replace(',', '.'));

                    mIsResult = true;

                    mHistory.NewElement();
                    UIResultLabel.Content = UIResultLabel.Content.ToString().Replace(',', '.');

                    double.TryParse(UIResultLabel.Content.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out mLastNumber);
                    mHistory.AppendElement(lResult.ToString().Replace(',', '.'), true);
                    mHistory.NewElement();
                }
            }
        }
    }
}
