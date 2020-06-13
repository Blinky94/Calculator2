using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : UserControl
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

        private string mSpecialSymbols = "×÷+-";
        private bool mIsResult = false;

        public Calculator()
        {
            InitializeComponent();
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
            }
        }

        private void UIACButton_Click(object sender, RoutedEventArgs e)
        {
            UIResultLabel.Content = "0";
            LastNumber = "0";
        }

        private void UISignOrUnSignButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(UIResultLabel.Content.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double lResult))
            {
                if (!mIsResult)
                {
                    ResultLabel = (lResult * (-1)).ToString();
                }
                else
                {
                    mIsResult = false;
                    string lDuplicate = LastNumber;
                    lDuplicate.Replace("(", "").Replace(")", "");

                    double.TryParse(lDuplicate, NumberStyles.Any, CultureInfo.InvariantCulture, out double lNum);

                    ResultLabel = (lNum * (-1)).ToString();
                }

                string lConcateSign = ResultLabel;

                if (lConcateSign.IndexOf('-') != -1)
                {
                    lConcateSign = $"({lConcateSign})";
                }
            }
        }

        // 50 + 6% (0,06) = 50,06
        private void UIPercentageButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultLabel, NumberStyles.Any, CultureInfo.InvariantCulture, out double lResult))
            {
                ResultLabel = (lResult * (0.01)).ToString();
            }
        }

        private void UIOperatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mSpecialSymbols.Contains(ResultLabel.LastOrDefault()))
            {
                UIResultLabel.Content = ResultLabel;

                LastNumber = ResultLabel;

                ResultLabel = (e.Source as Button).Content.GetType() != typeof(System.Windows.Controls.Image) ? (e.Source as Button).Content.ToString() : (e.Source as Button).Uid.ToString();
            }
        }

        public virtual void UIEqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mIsResult)
            {
                if (double.TryParse(ResultLabel, NumberStyles.Any, CultureInfo.InvariantCulture, out double lNewNumber))
                {
                    mIsResult = true;
                    LastNumber = ResultLabel;
                    UIResultLabel.Content = ResultLabel;
                }
            }
        }

        private void UIBackReturnButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UICOSButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UISINButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UITANButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIHYPButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UICOTButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UISECButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UICSCButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIACOSButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIASINButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIATANButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIACOTButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIASECButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIACSCButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIExpButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UISquareButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIOpenParenthesisButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UICloseParenthesisButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
