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

        #region BackgroundBaseButtons

        public static readonly DependencyProperty BackgroundBaseButtonsProperty =
            DependencyProperty.Register("BackgroundBaseButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BackgroundBaseButtons
        {
            get { return (Brush)GetValue(BackgroundBaseButtonsProperty); }
            set { SetValue(BackgroundBaseButtonsProperty, value); }
        }

        #endregion

        #region ForegroundBaseButtons

        public static readonly DependencyProperty ForegroundBaseButtonsProperty =
            DependencyProperty.Register("ForegroundBaseButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush ForegroundBaseButtons
        {
            get { return (Brush)GetValue(ForegroundBaseButtonsProperty); }
            set { SetValue(ForegroundBaseButtonsProperty, value); }
        }

        #endregion

        #region BorderBrushBaseButtons

        public static readonly DependencyProperty BorderBrushBaseButtonsProperty =
            DependencyProperty.Register("BorderBrushBaseButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BorderBrushBaseButtons
        {
            get { return (Brush)GetValue(BorderBrushBaseButtonsProperty); }
            set { SetValue(BorderBrushBaseButtonsProperty, value); }
        }

        #endregion

        #region BorderThicknessBaseButtons

        public static readonly DependencyProperty BorderThicknessBaseButtonsProperty =
            DependencyProperty.Register("BorderThicknessBaseButtons",
            typeof(double),
            typeof(Calculator_Control),
            new PropertyMetadata(5D));
        public double BorderThicknessBaseButtons
        {
            get { return (double)GetValue(BorderThicknessBaseButtonsProperty); }
            set { SetValue(BorderThicknessBaseButtonsProperty, value); }
        }

        #endregion

        #region BackgroundScientificButtons

        public static readonly DependencyProperty BackgroundScientificButtonsProperty =
            DependencyProperty.Register("BackgroundScientificButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BackgroundScientificButtons
        {
            get { return (Brush)GetValue(BackgroundScientificButtonsProperty); }
            set { SetValue(BackgroundScientificButtonsProperty, value); }
        }

        #endregion

        #region ForegroundScientificButtons

        public static readonly DependencyProperty ForegroundScientificButtonsProperty =
            DependencyProperty.Register("ForegroundScientificButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush ForegroundScientificButtons
        {
            get { return (Brush)GetValue(ForegroundScientificButtonsProperty); }
            set { SetValue(ForegroundScientificButtonsProperty, value); }
        }

        #endregion

        #region BorderBrushScientificButtons

        public static readonly DependencyProperty BorderBrushScientificButtonsProperty =
            DependencyProperty.Register("BorderBrushScientificButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BorderBrushScientificButtons
        {
            get { return (Brush)GetValue(BorderBrushScientificButtonsProperty); }
            set { SetValue(BorderBrushScientificButtonsProperty, value); }
        }

        #endregion

        #region BorderThicknessScientificButtons

        public static readonly DependencyProperty BorderThicknessScientificButtonsProperty =
            DependencyProperty.Register("BorderThicknessScientificButtons",
            typeof(double),
            typeof(Calculator_Control),
            new PropertyMetadata(5D));
        public double BorderThicknessScientificButtons
        {
            get { return (double)GetValue(BorderThicknessScientificButtonsProperty); }
            set { SetValue(BorderThicknessScientificButtonsProperty, value); }
        }

        #endregion

        #region BackgroundOperatorsButtons

        public static readonly DependencyProperty BackgroundOperatorsButtonsProperty =
            DependencyProperty.Register("BackgroundOperatorsButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BackgroundOperatorsButtons
        {
            get { return (Brush)GetValue(BackgroundOperatorsButtonsProperty); }
            set { SetValue(BackgroundOperatorsButtonsProperty, value); }
        }

        #endregion

        #region ForegroundOperatorsButtons

        public static readonly DependencyProperty ForegroundOperatorsButtonsProperty =
            DependencyProperty.Register("ForegroundOperatorsButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush ForegroundOperatorsButtons
        {
            get { return (Brush)GetValue(ForegroundOperatorsButtonsProperty); }
            set { SetValue(ForegroundOperatorsButtonsProperty, value); }
        }

        #endregion

        #region BorderBrushOperatorsButtons

        public static readonly DependencyProperty BorderBrushOperatorsButtonsProperty =
            DependencyProperty.Register("BorderBrushOperatorsButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BorderBrushOperatorsButtons
        {
            get { return (Brush)GetValue(BorderBrushOperatorsButtonsProperty); }
            set { SetValue(BorderBrushOperatorsButtonsProperty, value); }
        }

        #endregion

        #region BorderThicknessOperatorsButtons

        public static readonly DependencyProperty BorderThicknessOperatorsButtonsProperty =
            DependencyProperty.Register("BorderThicknessOperatorsButtons",
            typeof(double),
            typeof(Calculator_Control),
            new PropertyMetadata(5D));
        public double BorderThicknessOperatorsButtons
        {
            get { return (double)GetValue(BorderThicknessOperatorsButtonsProperty); }
            set { SetValue(BorderThicknessOperatorsButtonsProperty, value); }
        }

        #endregion

        #region BackgroundNumericalsButtons

        public static readonly DependencyProperty BackgroundNumericalsButtonsProperty =
            DependencyProperty.Register("BackgroundNumericalsButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BackgroundNumericalsButtons
        {
            get { return (Brush)GetValue(BackgroundNumericalsButtonsProperty); }
            set { SetValue(BackgroundNumericalsButtonsProperty, value); }
        }

        #endregion

        #region ForegroundNumericalsButtons

        public static readonly DependencyProperty ForegroundNumericalsButtonsProperty =
            DependencyProperty.Register("ForegroundNumericalsButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush ForegroundNumericalsButtons
        {
            get { return (Brush)GetValue(ForegroundNumericalsButtonsProperty); }
            set { SetValue(ForegroundNumericalsButtonsProperty, value); }
        }

        #endregion

        #region BorderBrushNumericalsButtons

        public static readonly DependencyProperty BorderBrushNumericalsButtonsProperty =
            DependencyProperty.Register("BorderBrushNumericalsButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BorderBrushNumericalsButtons
        {
            get { return (Brush)GetValue(BorderBrushNumericalsButtonsProperty); }
            set { SetValue(BorderBrushNumericalsButtonsProperty, value); }
        }

        #endregion

        #region BorderThicknessNumericalsButtons

        public static readonly DependencyProperty BorderThicknessNumericalsButtonsProperty =
            DependencyProperty.Register("BorderThicknessNumericalsButtons",
            typeof(double),
            typeof(Calculator_Control),
            new PropertyMetadata(5D));
        public double BorderThicknessNumericalsButtons
        {
            get { return (double)GetValue(BorderThicknessNumericalsButtonsProperty); }
            set { SetValue(BorderThicknessNumericalsButtonsProperty, value); }
        }

        #endregion

        #region BackgroundMemoryButtons

        public static readonly DependencyProperty BackgroundMemoryButtonsProperty =
            DependencyProperty.Register("BackgroundMemoryButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BackgroundMemoryButtons
        {
            get { return (Brush)GetValue(BackgroundMemoryButtonsProperty); }
            set { SetValue(BackgroundMemoryButtonsProperty, value); }
        }

        #endregion

        #region ForegroundMemoryButtons

        public static readonly DependencyProperty ForegroundMemoryButtonsProperty =
            DependencyProperty.Register("ForegroundMemoryButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush ForegroundMemoryButtons
        {
            get { return (Brush)GetValue(ForegroundMemoryButtonsProperty); }
            set { SetValue(ForegroundMemoryButtonsProperty, value); }
        }

        #endregion

        #region BorderBrushMemoryButtons

        public static readonly DependencyProperty BorderBrushMemoryButtonsProperty =
            DependencyProperty.Register("BorderBrushMemoryButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BorderBrushMemoryButtons
        {
            get { return (Brush)GetValue(BorderBrushMemoryButtonsProperty); }
            set { SetValue(BorderBrushMemoryButtonsProperty, value); }
        }

        #endregion

        #region BorderThicknessMemoryButtons

        public static readonly DependencyProperty BorderThicknessMemoryButtonsProperty =
            DependencyProperty.Register("BorderThicknessMemoryButtons",
            typeof(double),
            typeof(Calculator_Control),
            new PropertyMetadata(5D));
        public double BorderThicknessMemoryButtons
        {
            get { return (double)GetValue(BorderThicknessMemoryButtonsProperty); }
            set { SetValue(BorderThicknessMemoryButtonsProperty, value); }
        }

        #endregion

        #region BackgroundTrigonometryButtons

        public static readonly DependencyProperty BackgroundTrigonometryButtonsProperty =
            DependencyProperty.Register("BackgroundTrigonometryButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BackgroundTrigonometryButtons
        {
            get { return (Brush)GetValue(BackgroundTrigonometryButtonsProperty); }
            set { SetValue(BackgroundTrigonometryButtonsProperty, value); }
        }

        #endregion

        #region ForegroundTrigonometryButtons

        public static readonly DependencyProperty ForegroundTrigonometryButtonsProperty =
            DependencyProperty.Register("ForegroundTrigonometryButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush ForegroundTrigonometryButtons
        {
            get { return (Brush)GetValue(ForegroundTrigonometryButtonsProperty); }
            set { SetValue(ForegroundTrigonometryButtonsProperty, value); }
        }

        #endregion

        #region BorderBrushTrigonometryButtons

        public static readonly DependencyProperty BorderBrushTrigonometryButtonsProperty =
            DependencyProperty.Register("BorderBrushTrigonometryButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BorderBrushTrigonometryButtons
        {
            get { return (Brush)GetValue(BorderBrushTrigonometryButtonsProperty); }
            set { SetValue(BorderBrushTrigonometryButtonsProperty, value); }
        }

        #endregion

        #region BorderThicknessTrigonometryButtons

        public static readonly DependencyProperty BorderThicknessTrigonometryButtonsProperty =
            DependencyProperty.Register("BorderThicknessTrigonometryButtons",
            typeof(double),
            typeof(Calculator_Control),
            new PropertyMetadata(5D));
        public double BorderThicknessTrigonometryButtons
        {
            get { return (double)GetValue(BorderThicknessTrigonometryButtonsProperty); }
            set { SetValue(BorderThicknessTrigonometryButtonsProperty, value); }
        }

        #endregion

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

            OnCalculusDisplayChanged();
        }

        private void UIPointButton_Click(object sender, RoutedEventArgs e)
        {
            string lUIContent = ToCalculusDisplay;

            if (!lUIContent.Contains('.'))
            {
                // If preceding is operator or nothing, add 0 before the point
                string lOperators = "÷/×xX*-+√";
                string lAdded;

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
                    _ = lDuplicate.Replace("(", "").Replace(")", "");

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

                ToCalculusDisplay = (e.Source as Button).Content.GetType() != typeof(Image) ? (e.Source as Button).Content.ToString() : (e.Source as Button).Uid;

                OnCalculusDisplayChanged();
            }
        }

        public virtual void UIEqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mIsResult)
            {
                if (!double.IsNaN(double.Parse(ToCalculusDisplay, NumberStyles.Any, CultureInfo.InvariantCulture)))
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
