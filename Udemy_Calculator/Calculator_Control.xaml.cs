using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator_Control : UserControl
    {
        private string mToCalculusDisplay = "0";

        public string ToCalculusDisplay
        {
            get
            {
                return mToCalculusDisplay.Replace(',', '.');
            }
            private set
            {
                mToCalculusDisplay = value;
            }
        }

        public Delegate CalculusDisplayDelegate;

        public void OnCalculusDisplayChanged()
        {
            CalculusDisplayDelegate.DynamicInvoke(ToCalculusDisplay);
        }

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

        private string mSpecialSymbols = "×÷+-";
        private bool mIsResult = false;

        public Calculator_Control()
        {
            InitializeComponent();
        }

        private void UINumberButton_Click(object sender, RoutedEventArgs e)
        {
            string lInput = (e.Source as Button).Content.ToString().Replace(',', '.');

            double.TryParse(lInput, NumberStyles.Any, CultureInfo.InvariantCulture, out double lNumber);

            if (ToCalculusDisplay == "0" || mSpecialSymbols.Contains(ToCalculusDisplay) || mIsResult)
            {
                ToCalculusDisplay = lNumber.ToString();
                mIsResult = false;
            }
            else
            {
                ToCalculusDisplay = $"{ToCalculusDisplay}{lNumber}";
            }

            OnCalculusDisplayChanged();
        }

        private void UIPointButton_Click(object sender, RoutedEventArgs e)
        {
            string lUIContent = ToCalculusDisplay;

            if (!lUIContent.Contains('.'))
            {
                // If preceding is operator or nothing, add 0 before the point
                string lOperators = "÷/×xX*-+√";
                string lAdded = string.Empty;

                if (lOperators.Contains(lUIContent[lUIContent.Length - 1]))
                {
                    lAdded = "0";
                    ToCalculusDisplay = $"{ToCalculusDisplay}{lAdded}.";
                }
                else
                {
                    ToCalculusDisplay = $"{ToCalculusDisplay}.";
                }

                OnCalculusDisplayChanged();
            }
        }

        private void UIACButton_Click(object sender, RoutedEventArgs e)
        {
            ToCalculusDisplay = "0";
            LastNumber = "0";

            OnCalculusDisplayChanged();
        }

        private void UISignOrUnSignButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ToCalculusDisplay, NumberStyles.Any, CultureInfo.InvariantCulture, out double lResult))
            {
                if (!mIsResult)
                {
                    ToCalculusDisplay = (lResult * (-1)).ToString();
                }
                else
                {
                    mIsResult = false;
                    string lDuplicate = LastNumber;
                    lDuplicate.Replace("(", "").Replace(")", "");

                    double.TryParse(lDuplicate, NumberStyles.Any, CultureInfo.InvariantCulture, out double lNum);

                    ToCalculusDisplay = (lNum * (-1)).ToString();
                }

                OnCalculusDisplayChanged();
            }
        }

        // 50 + 6% (0,06) = 50,06
        private void UIPercentageButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ToCalculusDisplay, NumberStyles.Any, CultureInfo.InvariantCulture, out double lResult))
            {
                ToCalculusDisplay = (lResult * (0.01)).ToString();
                OnCalculusDisplayChanged();
            }
        }

        private void UIOperatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mSpecialSymbols.Contains(ToCalculusDisplay.LastOrDefault()))
            {
                LastNumber = ToCalculusDisplay;

                ToCalculusDisplay = (e.Source as Button).Content.GetType() != typeof(System.Windows.Controls.Image) ? (e.Source as Button).Content.ToString() : (e.Source as Button).Uid.ToString();

                OnCalculusDisplayChanged();
            }
        }

        public virtual void UIEqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mIsResult)
            {
                if (double.TryParse(ToCalculusDisplay, NumberStyles.Any, CultureInfo.InvariantCulture, out double lNewNumber))
                {
                    mIsResult = true;
                    LastNumber = ToCalculusDisplay;
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

        private void UILOGButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UILNButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UISquareRootButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UISquareButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void UIXSquareButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIXCubeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIYExpXButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UINFactorielButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UITwoSquareRootButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIThreeSquareRootButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIXExpXButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIXSquareRootYButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UITenExpXButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIMODButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIAbsoluteValButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIMCButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIMPlusButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIMMinusButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIMemoryPlusButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIMemoryRecallButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIRandomButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIEXPButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void UIEXPSquareButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIOneOnXButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UISecondFuncButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
