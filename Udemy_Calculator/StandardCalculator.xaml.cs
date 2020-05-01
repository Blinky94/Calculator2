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
        private decimal mLastNumber;
        private string mSpecialSymbols = "×÷+-";
        private bool mIsResult = false;
        private History mHistory;

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

            mHistory.AppendElement(lNumber.ToString(), mIsResult, mLastNumber);
        }

        private void UIPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!UIResultLabel.Content.ToString().Replace(',', '.').Contains('.'))
            {
                UIResultLabel.Content = $"{UIResultLabel.Content}.";
                mHistory.AppendElement((e.Source as Button).Content.ToString(), mIsResult, mLastNumber);
                mIsResult = false;
            }
        }

        private void UIACButton_Click(object sender, RoutedEventArgs e)
        {
            UIResultLabel.Content = "0";
            mLastNumber = 0;
            mHistory.AppendElement(string.Empty);
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
                    mHistory.RemoveElement(UIResultLabel.Content.ToString().Length);
                    UIResultLabel.Content = lResult * lTransformCoef;
                }
                else
                {
                    mIsResult = false;
                    UIResultLabel.Content = mLastNumber * lTransformCoef;
                }

                mHistory.AppendElement(UIResultLabel.Content.ToString(), mIsResult, mLastNumber);
            }
        }

        private void UIOperatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mSpecialSymbols.Contains(UIResultLabel.Content.ToString().LastOrDefault()))
            {
                mHistory.AppendElement((e.Source as Button).Content.ToString(), mIsResult, mLastNumber);

                decimal.TryParse(UIResultLabel.Content.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out mLastNumber);
                UIResultLabel.Content = (e.Source as Button).Content.ToString();
            }
        }

        private void UIEqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mIsResult)
            {
                string lFormula = mHistory.ReturnFormula();
                decimal lNewNumber;
                if (decimal.TryParse(UIResultLabel.Content.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out lNewNumber))
                {
                    decimal lResult = default;

                    UIResultLabel.Content = lResult;
                    mHistory.AppendElement((e.Source as Button).Content.ToString(), mIsResult, mLastNumber);

                    mIsResult = true;

                    mHistory.NewElement();
                    decimal.TryParse(UIResultLabel.Content.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out mLastNumber);
                    mHistory.AppendElement(lResult.ToString(), true);
                    mHistory.NewElement();
                }
            }
        }
    }
}
