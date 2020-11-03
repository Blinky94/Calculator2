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

            UIMCButton.OnMemoryButtonClicked += new RoutedEventHandler(UIMCButton_Click);
            DummyButton.OnScientificButtonClicked += new RoutedEventHandler(UISecondFuncButton_Click);
            UIXSquareButton.OnScientificButtonClicked += new RoutedEventHandler(UIXSquareButton_Click);
            UIXCubeButton.OnScientificButtonClicked += new RoutedEventHandler(UIXCubeButton_Click);
            UIXExpXButton.OnScientificButtonClicked += new RoutedEventHandler(UIXExpXButton_Click);
            UINFactorielButton.OnScientificButtonClicked += new RoutedEventHandler(UINFactorielButton_Click);
            UIRootXButton.OnScientificButtonClicked += new RoutedEventHandler(UIRootXButton_Click);
            UITwoSquareRootButton.OnScientificButtonClicked += new RoutedEventHandler(UITwoSquareRootButton_Click);
            UIThreeSquareRootButton.OnScientificButtonClicked += new RoutedEventHandler(UIThreeSquareRootButton_Click);
            UIXSquareRootYButton.OnScientificButtonClicked += new RoutedEventHandler(UIXSquareRootYButton_Click);
            UITenExpXButton.OnScientificButtonClicked += new RoutedEventHandler(UITenExpXButton_Click);
            UIMODButton.OnScientificButtonClicked += new RoutedEventHandler(UIMODButton_Click);
            UIAbsoluteValButton.OnScientificButtonClicked += new RoutedEventHandler(UIAbsoluteValButton_Click);
            UIRandomButton.OnScientificButtonClicked += new RoutedEventHandler(UIRandomButton_Click);
            UIEXPButton.OnScientificButtonClicked += new RoutedEventHandler(UIEXPButton_Click);
            UIEXPSquareButton.OnScientificButtonClicked += new RoutedEventHandler(UIEXPSquareButton_Click);
            UIOneOnXButton.OnScientificButtonClicked += new RoutedEventHandler(UIOneOnXButton_Click);
            UIExpButton.OnScientificButtonClicked += new RoutedEventHandler(UIExpButton_Click);
            UISquareRootButton.OnScientificButtonClicked += new RoutedEventHandler(UISquareRootButton_Click);
            UIPIButton.OnScientificButtonClicked += new RoutedEventHandler(UIPIButton_Click);
            UILOGButton.OnScientificButtonClicked += new RoutedEventHandler(UILOGButton_Click);
            UILNButton.OnScientificButtonClicked += new RoutedEventHandler(UILNButton_Click);
            UICOSButton.OnScientificButtonClicked += new RoutedEventHandler(UICOSButton_Click);
            UIOpenParenthesisButton.OnScientificButtonClicked += new RoutedEventHandler(UIOpenParenthesisButton_Click);
            UICloseParenthesisButton.OnScientificButtonClicked += new RoutedEventHandler(UICloseParenthesisButton_Click);
            UICOTButton.OnTrigonometryButtonClicked += new RoutedEventHandler(UICOTButton_Click);
            UISECButton.OnTrigonometryButtonClicked += new RoutedEventHandler(UISECButton_Click);
            UICSCButton.OnTrigonometryButtonClicked += new RoutedEventHandler(UICSCButton_Click);
            UITANButton.OnTrigonometryButtonClicked += new RoutedEventHandler(UITANButton_Click);
            UIACOTButton.OnTrigonometryButtonClicked += new RoutedEventHandler(UIACOTButton_Click);
            UIASECButton.OnTrigonometryButtonClicked += new RoutedEventHandler(UIASECButton_Click);
            UIACSCButton.OnTrigonometryButtonClicked += new RoutedEventHandler(UIACSCButton_Click);
            UIATANButton.OnTrigonometryButtonClicked += new RoutedEventHandler(UIATANButton_Click);
            UIHYPButton.OnTrigonometryButtonClicked += new RoutedEventHandler(UIHYPButton_Click);
            UIACButton.OnBaseButtonClicked += new RoutedEventHandler(UIACButton_Click);
            UIBackReturnButton.OnBaseButtonClicked += new RoutedEventHandler(UIBackReturnButton_Click);
            UIPercentButton.OnBaseButtonClicked += new RoutedEventHandler(UIPercentButton_Click);
            UIDivideButton.OnOperatorButtonClicked += new RoutedEventHandler(UIOperatorButton_Click);
            UISevenButton.OnNumericalButtonClicked += new RoutedEventHandler(UINumberButton_Click);
            UIHeightButton.OnNumericalButtonClicked += new RoutedEventHandler(UINumberButton_Click);
            UINineButton.OnNumericalButtonClicked += new RoutedEventHandler(UINumberButton_Click);
            UIMultiplyButton.OnOperatorButtonClicked += new RoutedEventHandler(UIOperatorButton_Click);
            UIFourButton.OnNumericalButtonClicked += new RoutedEventHandler(UINumberButton_Click);
            UIFiveButton.OnNumericalButtonClicked += new RoutedEventHandler(UINumberButton_Click);
            UISixButton.OnNumericalButtonClicked += new RoutedEventHandler(UINumberButton_Click);
            UIPlusButton.OnOperatorButtonClicked += new RoutedEventHandler(UIOperatorButton_Click);
            UIOneButton.OnNumericalButtonClicked += new RoutedEventHandler(UINumberButton_Click);
            UITwoButton.OnNumericalButtonClicked += new RoutedEventHandler(UINumberButton_Click);
            UIThreeButton.OnNumericalButtonClicked += new RoutedEventHandler(UINumberButton_Click);
            UIMinusButton.OnOperatorButtonClicked += new RoutedEventHandler(UIOperatorButton_Click);
            UIZeroButton.OnNumericalButtonClicked += new RoutedEventHandler(UINumberButton_Click);
            UISignButton.OnOperatorButtonClicked += new RoutedEventHandler(UISignOrUnSignButton_Click);
            UIPointButton.OnNumericalButtonClicked += new RoutedEventHandler(UIPointButton_Click);
            UIEqualButton.OnOperatorButtonClicked += new RoutedEventHandler(UIEqualButton_Click);
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
            ToCalculusDisplay = ToCalculusDisplay.Replace("(", "").Replace(")", "");

            if (double.TryParse(ToCalculusDisplay, NumberStyles.Any, CultureInfo.InvariantCulture, out double lResult))
            {
                lResult *= (-1);
                string lStr = lResult.ToString();

                if (Math.Sign(lResult) == -1)
                {
                    lStr = $"({lResult})";
                    if (!mIsResult)
                    {
                        ((MainWindow)Application.Current.MainWindow).UIHistory.RemoveElement(ToCalculusDisplay.Length);
                    }
                }

                if (mIsResult)
                {
                    mIsResult = false;
                }

                ToCalculusDisplay = lStr;

                ((MainWindow)Application.Current.MainWindow).UIHistory.AppendElement(ToCalculusDisplay, mIsResult, LastNumber);
                OnCalculusDisplayChanged();
            }
        }

        // 50 + 6% (0,06) = 50,06
        private void UIPercentButton_Click(object sender, RoutedEventArgs e)
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

                    ToCalculusDisplay = ToCalculusDisplay.Replace("(", "").Replace(")", "");
                    if (double.TryParse(ToCalculusDisplay, NumberStyles.Any, CultureInfo.InvariantCulture, out double lNumberParsed) == false)
                    {
                        TraceLogs.AddError($"{GlobalUsage.GetCurrentMethodName}: unable to parse the current number {ToCalculusDisplay}");
                    }
                    if (!double.IsNaN(lNumberParsed))
                    {
                        // Ici PEMDAS
                        string lResult;
                        mPemdas = new PEMDAS(lFormula);
                        lResult = mPemdas.ComputeFormula();

                        ToCalculusDisplay = lResult;
                        ((MainWindow)Application.Current.MainWindow).UIHistory.AppendElement((e.Source as Button).Content.ToString(), mIsResult, LastNumber, true);

                       ((MainWindow)Application.Current.MainWindow).UIHistory.InsertNewParagraph(ParagraphType.Chunk);

                        LastNumber = ToCalculusDisplay;
                        mIsResult = true;

                        ((MainWindow)Application.Current.MainWindow).UIHistory.AppendElement(lResult.Replace(',', '.'), mIsResult, null, true);
                        ((MainWindow)Application.Current.MainWindow).UIHistory.InsertNewParagraph(ParagraphType.Formula);
                        ((MainWindow)Application.Current.MainWindow).UIHistory.InsertNewParagraph(ParagraphType.Chunk);
                        ((MainWindow)Application.Current.MainWindow).UIHistory.InsertNewParagraph(ParagraphType.Result);
                        OnCalculusDisplayChanged();
                    }
                }
                catch (Exception ex)
                {
                    TraceLogs.AddWarning($"{ex.Message}: Unable to compute this formula ({lFormula})");
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

        //Exposant ^
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

        private void UIPIButton_Click(object sender, RoutedEventArgs e)
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

        // Exposant e
        private void UIEXPButton_Click(object sender, RoutedEventArgs e)
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
