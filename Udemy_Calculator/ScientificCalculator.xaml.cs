using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for StandardCalculator.xaml
    /// </summary>
    public partial class ScientificCalculator : UserControl
    {
        private string mLastNumber = "0";
        internal string LastNumber
        {
            get
            {
                return mLastNumber.Replace(',', '.');
            }

            private set
            {
                mLastNumber = value;
            }
        }

        internal string ResultLabel
        {
            get
            {
                return UIResultLabel.Content.ToString().Replace(',', '.');
            }

            private set
            {
                UIResultLabel.Content = value;
            }
        }

        private readonly string mSpecialSymbols = "×÷+-";
        private bool mIsResult;
        private readonly History mHistory;
        private PEMDAS mPemdas;

        public ScientificCalculator()
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

            if (ResultLabel == "0" || mSpecialSymbols.Contains(ResultLabel) || mIsResult)
            {
                ResultLabel = lNumber.ToString();
                mIsResult = false;
            }
            else
            {
                ResultLabel = $"{ResultLabel}{lNumber}";
            }

            lInput = lNumber.ToString().Replace(',', '.');
            mHistory.AppendElement(lInput, mIsResult, LastNumber);
        }

        private void UIPointButton_Click(object sender, RoutedEventArgs e)
        {
            string lUIContent = ResultLabel;

            if (!lUIContent.Contains('.'))
            {
                // If preceding is operator or nothing, add 0 before the point
                string lOperators = "÷/×xX*-+√";
                string lAdded = string.Empty;

                if (lOperators.Contains(lUIContent[lUIContent.Length - 1]))
                {
                    lAdded = "0";
                    ResultLabel = $"{ResultLabel}{lAdded}.";
                }
                else
                {
                    ResultLabel = $"{ResultLabel}.";
                }

                mHistory.AppendElement(lAdded + (e.Source as Button).Content.ToString(), mIsResult, LastNumber);
                mIsResult = false;
            }
        }

        private void UIACButton_Click(object sender, RoutedEventArgs e)
        {
            UIResultLabel.Content = "0";
            LastNumber = "0";
            mHistory.AppendElement(string.Empty);
        }

        private void UISignOrUnSignButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(UIResultLabel.Content.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double lResult))
            {
                if (!mIsResult)
                {
                    mHistory.RemoveElement(ResultLabel.Length);
                    ResultLabel = (lResult * (-1)).ToString();
                }
                else
                {
                    mIsResult = false;
                    string lDuplicate = LastNumber;
                    _ = lDuplicate.Replace("(", "").Replace(")", "");

                    double.TryParse(lDuplicate, NumberStyles.Any, CultureInfo.InvariantCulture, out double lNum);

                    ResultLabel = (lNum * (-1)).ToString();
                }

                string lConcateSign = ResultLabel;

                if (lConcateSign.IndexOf('-') != -1)
                {
                    lConcateSign = $"({lConcateSign})";
                }

                mHistory.AppendElement(lConcateSign, mIsResult, LastNumber);
            }
        }

        // 50 + 6% (0,06) = 50,06
        private void UIPercentageButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultLabel, NumberStyles.Any, CultureInfo.InvariantCulture, out double lResult))
            {
                mHistory.RemoveElement(ResultLabel.Length);
                ResultLabel = (lResult * (0.01)).ToString();

                mHistory.AppendElement(ResultLabel, false);
            }
        }

        private void UIOperatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mSpecialSymbols.Contains(ResultLabel.LastOrDefault()))
            {
                mHistory.AppendElement((e.Source as Button).Content.ToString().Replace(',', '.'), mIsResult, LastNumber);

                UIResultLabel.Content = ResultLabel;

                LastNumber = ResultLabel;
                ResultLabel = (e.Source as Button).Content.ToString();
            }
        }

        private void UIEqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mIsResult)
            {
                string lFormula = mHistory.ReturnFormula();
                if (double.TryParse(ResultLabel, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                {
                    // Ici PEMDAS
                    string lResult;
                    mPemdas = new PEMDAS(lFormula);
                    lResult = mPemdas.ComputeFormula();

                    ResultLabel = lResult;
                    mHistory.AppendElement((e.Source as Button).Content.ToString(), mIsResult, LastNumber);

                    mIsResult = true;

                    mHistory.NewElement();

                    LastNumber = ResultLabel;
                    mHistory.AppendElement(lResult.Replace(',', '.'), true);
                    mHistory.NewElement();
                    UIResultLabel.Content = ResultLabel;
                }
            }
        }
    }
}
