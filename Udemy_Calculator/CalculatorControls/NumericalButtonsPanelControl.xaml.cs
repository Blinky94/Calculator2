using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
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
                return mToCalculusDisplay.Replace(",", ".").Replace("(", "").Replace(")", "");
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

            if (!lUIContent.Contains('.') && !lUIContent.Contains('(') && !lUIContent.Contains(')'))
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

        private void UISignOrUnSignButton_Click(object sender, RoutedEventArgs e)
        {
            string lNum = ToCalculusDisplay;

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

                ((MainWindow)Application.Current.MainWindow).UIHistory.AppendElement(lStr, mIsResult, LastNumber);
                OnCalculusDisplayChanged();
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

        private void UINumberButton_Click(object sender, RoutedEventArgs e)
        {
            // VOIR POURQUOI AVEC UN NOMBRE AVEC PARENTHESE ON PEUT ENTRER UN AUTRE NOMBRE !!!
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
