using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator.CalculatorControls
{
    /// <summary>
    /// Interaction logic for NumericalButtonsPanelControl.xaml
    /// </summary>
    public partial class NumericalButtonsPanelControl : UserControl
    {
        private PEMDAS mPemdas;

        #region Delegate to display Event

        private string mToCalculusDisplay = "0";

        public string ToCalculusDisplay
        {
            get
            {
                return mToCalculusDisplay.Replace(",", ".");
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

        public NumericalButtonsPanelControl()
        {
            InitializeComponent();

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

        private void UIPointButton_Click(object sender, RoutedEventArgs e)
        {
            string lUIContent = ((MainWindow)Application.Current.MainWindow).UIHistory.ReturnLastOperandInTheFormula();

            if (!string.IsNullOrEmpty(lUIContent) && !lUIContent.Contains('.') && !lUIContent.Contains('(') && !lUIContent.Contains(')'))
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

        private void UIEqualButton_Click(object sender, RoutedEventArgs e)
        {
            string lFormula = string.Empty;
            if (!mIsResult)
            {
                try
                {
                    lFormula = ((MainWindow)Application.Current.MainWindow).UIHistory.ReturnFormula();

                    string lLastOpOfFormula = ((MainWindow)Application.Current.MainWindow).UIHistory.ReturnLastCharOfFormula();
                    if (!double.TryParse(lLastOpOfFormula, NumberStyles.Any, CultureInfo.InvariantCulture, out double lLastOpFormulaParsed) && lLastOpOfFormula != ")")
                    {
                        return;
                    }

                    if (mSpecialSymbols.Contains(lFormula.LastOrDefault()))
                    {
                        TraceLogs.AddWarning($"{GlobalUsage.GetCurrentMethodName}: The formula is not completed !!! : {ToCalculusDisplay}");
                        return;
                    }

                    ToCalculusDisplay = ToCalculusDisplay.Replace("(", "").Replace(")", "");
                    if (double.TryParse(ToCalculusDisplay, NumberStyles.Any, CultureInfo.InvariantCulture, out double lNumberParsed) == false)
                    {
                        TraceLogs.AddError($"{GlobalUsage.GetCurrentMethodName}: unable to parse the current number {ToCalculusDisplay}");
                    }
                    if (!double.IsNaN(lNumberParsed))
                    {
                        // Ici PEMDAS
                        mPemdas = new PEMDAS(lFormula);
                        string lResult = mPemdas.ComputeFormula();

                        ToCalculusDisplay = lResult;
                        ((MainWindow)Application.Current.MainWindow).UIHistory.AppendElement((e.Source as Button).Content.ToString(), mIsResult, LastNumber, true);

                        ((MainWindow)Application.Current.MainWindow).UIHistory.InsertNewParagraph(ParagraphType.Chunk);

                        //   LastNumber = ((MainWindow)Application.Current.MainWindow).UIHistory.ReturnLastOperandInTheFormula();
                        mIsResult = true;

                        ((MainWindow)Application.Current.MainWindow).UIHistory.AppendElement(lResult.Replace(',', '.'), mIsResult, null, true);
                        ((MainWindow)Application.Current.MainWindow).UIHistory.InsertNewParagraph(ParagraphType.Formula);
                        ((MainWindow)Application.Current.MainWindow).UIHistory.InsertNewParagraph(ParagraphType.Chunk);
                        ((MainWindow)Application.Current.MainWindow).UIHistory.InsertNewParagraph(ParagraphType.Result);

                        LastNumber = ToCalculusDisplay;
                        OnCalculusDisplayChanged();
                    }
                }
                catch (Exception ex)
                {
                    TraceLogs.AddWarning($"{ex.Message}: Unable to compute this formula ({lFormula})");
                }
            }
        }

        private void UINumberButton_Click(object sender, RoutedEventArgs e)
        {
            var lUIContent = ((MainWindow)Application.Current.MainWindow).UIHistory.ReturnLastCharOfFormula();

            string lInput = (e.Source as Button).Content.ToString().Replace(',', '.');

            if (lUIContent == ")")
            {
                TraceLogs.AddWarning($"{GlobalUsage.GetCurrentMethodName}: Last character is a parenthesis !!!");
                return;
            }

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

        private void UIOperatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mSpecialSymbols.Contains(ToCalculusDisplay.LastOrDefault()))
            {
                if (double.TryParse(ToCalculusDisplay, NumberStyles.Any, CultureInfo.InvariantCulture, out double lResult))
                {
                    if (Math.Sign(lResult) == -1)
                    {
                        ToCalculusDisplay = $"({ToCalculusDisplay})";
                    }
                }

                LastNumber = ToCalculusDisplay;

                ToCalculusDisplay = (e.Source as Button).Content.ToString();
                ((MainWindow)Application.Current.MainWindow).UIHistory.AppendElement((e.Source as Button).Content.ToString().Replace(',', '.'), mIsResult, LastNumber);

                OnCalculusDisplayChanged();
            }
        }

        // 50 + 6% (0,06) = 50,06
        private void UIPercentButton_Click(object sender, RoutedEventArgs e)
        {
            string lLastNum = ((MainWindow)Application.Current.MainWindow).UIHistory.ReturnLastOperandInTheFormula();

            if (string.IsNullOrEmpty(lLastNum))
            {
                lLastNum = ToCalculusDisplay;
            }

            if (decimal.TryParse(lLastNum, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal lResult))
            {
                string lNum = (lResult * (0.01m)).ToString();
                ToCalculusDisplay = lNum;

                if (Math.Sign(lResult) == -1)
                {
                    lNum = $"({lNum})";
                    lLastNum = $"({lLastNum})";
                }

                ((MainWindow)Application.Current.MainWindow).UIHistory.RemoveElement(lLastNum.Length);

                ((MainWindow)Application.Current.MainWindow).UIHistory.AppendElement(lNum, false);
                OnCalculusDisplayChanged();
            }
        }

        private void UISignOrUnSignButton_Click(object sender, RoutedEventArgs e)
        {
            string lLastChar = ((MainWindow)Application.Current.MainWindow).UIHistory.ReturnLastCharOfFormula();

            if (string.IsNullOrEmpty(lLastChar))
            {
                TraceLogs.AddWarning($"{GlobalUsage.GetCurrentMethodName}: No element in the formula !!!");
            }
            else if (mSpecialSymbols.Contains(lLastChar))
            {
                TraceLogs.AddWarning($"{GlobalUsage.GetCurrentMethodName}: Last character is not a number !!!");
                return;
            }

            string lNum = ((MainWindow)Application.Current.MainWindow).UIHistory.ReturnLastOperandInTheFormula();

            if (string.IsNullOrEmpty(lNum))
            {
                lNum = ToCalculusDisplay;
            }

            if (double.TryParse(lNum, NumberStyles.Any, CultureInfo.InvariantCulture, out double lResult))
            {
                double lResultBefore = lResult;
                lResult *= (-1);
                string lStr = lResult.ToString();

                if (Math.Sign(lResult) == -1)
                {
                    lStr = $"({lResult})";
                    if (!mIsResult)
                    {
                        ((MainWindow)Application.Current.MainWindow).UIHistory.RemoveElement(lNum.Length);
                    }
                }
                else
                {
                    if (Math.Sign(lResultBefore) == -1)
                    {
                        string OldlStr = $"({lResultBefore})";
                        if (!mIsResult)
                        {
                            ((MainWindow)Application.Current.MainWindow).UIHistory.RemoveElement(OldlStr.Length);
                        }
                    }
                }

                if (mIsResult)
                {
                    mIsResult = false;
                }

                ToCalculusDisplay = lStr;

                ((MainWindow)Application.Current.MainWindow).UIHistory.AppendElement(lStr, mIsResult, LastNumber);
                OnCalculusDisplayChanged();
            }
        }

        private void UIBackReturnButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UIACButton_Click(object sender, RoutedEventArgs e)
        {
            ToCalculusDisplay = "0";
            LastNumber = "0";

            ((MainWindow)Application.Current.MainWindow).UIHistory.AppendElement(string.Empty);
            OnCalculusDisplayChanged();
        }
    }
}
