using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator_Control : UserControl
    {
        #region Delegate to display Event

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

        internal Delegate CalculusDisplayDelegate;

        public void OnCalculusDisplayChanged()
        {
            CalculusDisplayDelegate?.DynamicInvoke(ToCalculusDisplay);
        }

        #endregion

        #region Delegate to Debug log console Event

        internal Delegate CalculatorControl_DebugLogsDelegate;


        #endregion

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

        private readonly string mSpecialSymbols = "×÷+-";
        private bool mIsResult;

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

            lInput = lNumber.ToString().Replace(',', '.');
            ((MainWindow)Application.Current.MainWindow).UIHistory.AppendElement(lInput, mIsResult, LastNumber);
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

                ((MainWindow)Application.Current.MainWindow).UIHistory.AppendElement(lAdded + (e.Source as Button).Content.ToString(), mIsResult, LastNumber);
                OnCalculusDisplayChanged();
                mIsResult = false;
            }
        }

        private void UIACButton_Click(object sender, RoutedEventArgs e)
        {
            ToCalculusDisplay = "0";
            LastNumber = "0";

            ((MainWindow)Application.Current.MainWindow).UIHistory.AppendElement(string.Empty);
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
                    _ = lDuplicate.Replace("(", "").Replace(")", "");

                    double.TryParse(lDuplicate, NumberStyles.Any, CultureInfo.InvariantCulture, out double lNum);

                    ToCalculusDisplay = (lNum * (-1)).ToString();
                }

                ((MainWindow)Application.Current.MainWindow).UIHistory.AppendElement(ToCalculusDisplay, mIsResult, LastNumber);
                OnCalculusDisplayChanged();
            }
        }

        // 50 + 6% (0,06) = 50,06
        private void UIPercentageButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ToCalculusDisplay, NumberStyles.Any, CultureInfo.InvariantCulture, out double lResult))
            {
                ToCalculusDisplay = (lResult * (0.01)).ToString();

                ((MainWindow)Application.Current.MainWindow).UIHistory.AppendElement(ToCalculusDisplay, false);
                OnCalculusDisplayChanged();
            }
        }

        private void UIOperatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mSpecialSymbols.Contains(ToCalculusDisplay.LastOrDefault()))
            {
                ToCalculusDisplay = (e.Source as Button).Content.ToString();

                ((MainWindow)Application.Current.MainWindow).UIHistory.AppendElement((e.Source as Button).Content.ToString().Replace(',', '.'), mIsResult, LastNumber);

                LastNumber = ToCalculusDisplay;
                OnCalculusDisplayChanged();
            }
        }

        private PEMDAS mPemdas;

        public virtual void UIEqualButton_Click(object sender, RoutedEventArgs e)
        {
            string lFormula = string.Empty;
            if (!mIsResult)
            {
                try
                {
                    lFormula = ((MainWindow)Application.Current.MainWindow).UIHistory.ReturnFormula();

                    if (!double.IsNaN(double.Parse(ToCalculusDisplay, NumberStyles.Any, CultureInfo.InvariantCulture)))
                    {
                        // Ici PEMDAS
                        string lResult;
                        mPemdas = new PEMDAS(lFormula);
                        lResult = mPemdas.ComputeFormula();

                        ToCalculusDisplay = lResult;
                        ((MainWindow)Application.Current.MainWindow).UIHistory.AppendElement((e.Source as Button).Content.ToString(), mIsResult, LastNumber, true);

                        ((MainWindow)Application.Current.MainWindow).UIHistory.NewElement();

                        LastNumber = ToCalculusDisplay;
                        mIsResult = true;

                        ((MainWindow)Application.Current.MainWindow).UIHistory.AppendElement(lResult.Replace(',', '.'), mIsResult, null, true);
                        ((MainWindow)Application.Current.MainWindow).UIHistory.NewElement();
                        OnCalculusDisplayChanged();
                    }
                }
                catch (Exception ex)
                {
                    TraceLogs.AddWarning($"{ex.Message}: Calcul impossible ({lFormula})");
                }
            }
        }

        private void UIBackReturnButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UICOSButton_Click(object sender, RoutedEventArgs e)
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

        private void UIXSquareButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIXCubeButton_Click(object sender, RoutedEventArgs e)
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

        private void UIRootXButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
