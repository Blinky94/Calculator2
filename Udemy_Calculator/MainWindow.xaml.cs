using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SolidColorBrush MainCalculatorBackground { get; set; }      
        public SolidColorBrush MainCalculatorForeground { get; set; }
        public SolidColorBrush MainCalculatorBorderBrush { get; set; }  
        public SolidColorBrush Background2ndeButton { get; set; }
        public SolidColorBrush Foreground2ndeButton { get; set; }
        public SolidColorBrush BorderBrush2ndeButton { get; set; }
        public SolidColorBrush BackgroundBaseButtons { get; set; }
        public SolidColorBrush ForegroundBaseButtons { get; set; }
        public SolidColorBrush BorderBrushBaseButtons { get; set; }
        public SolidColorBrush BackgroundScientificButtons { get; set; }
        public SolidColorBrush ForegroundScientificButtons { get; set; }
        public SolidColorBrush BorderBrushScientificButtons { get; set; }
        public SolidColorBrush BackgroundOperatorsButtons { get; set; }
        public SolidColorBrush ForegroundOperatorsButtons { get; set; }
        public SolidColorBrush BorderBrushOperatorsButtons { get; set; }
        public SolidColorBrush BackgroundNumericalsButtons { get; set; }
        public SolidColorBrush ForegroundNumericalsButtons { get; set; }
        public SolidColorBrush BorderBrushNumericalsButtons { get; set; }
        public SolidColorBrush BackgroundMemoryButtons { get; set; }
        public SolidColorBrush ForegroundMemoryButtons { get; set; }
        public SolidColorBrush BorderBrushMemoryButtons { get; set; }
        public SolidColorBrush BackgroundTrigonometryButtons { get; set; }
        public SolidColorBrush ForegroundTrigonometryButtons { get; set; }
        public SolidColorBrush BorderBrushTrigonometryButtons { get; set; }

        private void LoadXmlValues()
        {
            // Load the current theme from the params xml file
            XmlParser.LoadParamsXmlTheme();

            // Get the list from xml file loaded
            var lList = XmlParser.GetListOfParametersFromXmlFile().Where(p => p.ParentThemeName == p.ThemeSelected).Select(p => p);

            foreach (var item in lList)
            {
                var lBrushVal = (SolidColorBrush)new BrushConverter().ConvertFromString(item.ParameterValue);
                switch (item.ParameterName)
                {
                    case "MainCalculatorBackground":
                        MainCalculatorBackground = lBrushVal;
                        break;
                    case "MainCalculatorForeground":
                        MainCalculatorForeground = lBrushVal;
                        break;
                    case "MainCalculatorBorderBrush":
                        MainCalculatorBorderBrush = lBrushVal;
                        break;
                    case "BackgroundBaseButtons":
                        BackgroundBaseButtons = lBrushVal;
                        break;
                    case "ForegroundBaseButtons":
                        ForegroundBaseButtons = lBrushVal;
                        break;
                    case "BorderBrushBaseButtons":
                        BorderBrushBaseButtons = lBrushVal;
                        break;
                    case "Background2ndeButton":
                        Background2ndeButton = lBrushVal;
                        break;
                    case "Foreground2ndeButton":
                        Foreground2ndeButton = lBrushVal;
                        break;
                    case "BorderBrush2ndeButton":
                        BorderBrush2ndeButton = lBrushVal;
                        break;
                    case "BackgroundScientificButtons":
                        BackgroundScientificButtons = lBrushVal;
                        break;
                    case "ForegroundScientificButtons":
                        ForegroundScientificButtons = lBrushVal;
                        break;
                    case "BorderBrushScientificButtons":
                        BorderBrushScientificButtons = lBrushVal;
                        break;
                    case "BackgroundOperatorsButtons":
                        BackgroundOperatorsButtons = lBrushVal;
                        break;
                    case "ForegroundOperatorsButtons":
                        ForegroundOperatorsButtons = lBrushVal;
                        break;
                    case "BorderBrushOperatorsButtons":
                        BorderBrushOperatorsButtons = lBrushVal;
                        break;
                    case "BackgroundNumericalsButtons":
                        BackgroundNumericalsButtons = lBrushVal;
                        break;
                    case "ForegroundNumericalsButtons":
                        ForegroundNumericalsButtons = lBrushVal;
                        break;
                    case "BorderBrushNumericalsButtons":
                        BorderBrushNumericalsButtons = lBrushVal;
                        break;
                    case "BackgroundMemoryButtons":
                        BackgroundMemoryButtons = lBrushVal;
                        break;
                    case "ForegroundMemoryButtons":
                        ForegroundMemoryButtons = lBrushVal;
                        break;
                    case "BorderBrushMemoryButtons":
                        BorderBrushMemoryButtons = lBrushVal;
                        break;
                    case "BackgroundTrigonometryButtons":
                        BackgroundTrigonometryButtons = lBrushVal;
                        break;
                    case "ForegroundTrigonometryButtons":
                        ForegroundTrigonometryButtons = lBrushVal;
                        break;
                    case "BorderBrushTrigonometryButtons":
                        BorderBrushTrigonometryButtons = lBrushVal;
                        break;
                }
            }
            //CommonTheme.ThemeSelectedName = CommonTheme.CompleteListThemes.FirstOrDefault().ThemeSelected;
        }


        // Event to raise number pressed by user to display to the UIDisplay
        public delegate void EventUpdateUIDisplayHandler(string pContent);
        public event EventUpdateUIDisplayHandler UIEventDisplayValueEvent;

        private void SetEvents()
        {
            // Event registered to the UIDisplay when number is typed
            UIEventDisplayValueEvent += new EventUpdateUIDisplayHandler(ModifyUIDisplay);
            UICalculator.CalculusDisplayDelegate = UIEventDisplayValueEvent;

            // Event registered to send all logs entries to the console log window
            mConsoleDebug = new ConsoleDebug();
            mConsoleDebug.Show();
        }

        internal static ConsoleDebug mConsoleDebug;

        public MainWindow()
        {
            InitializeComponent();
            LoadXmlValues();
            this.DataContext = this;
            UIMenuSide.UIMenuSelected.Content = CalculatorMode.Scientific.ToString();

            // Set all events
            SetEvents();

            // Load the current theme from the params xml file
            XmlParser.LoadParamsXmlTheme();
            // Get the list from xml file loaded
            UIMenuSide.SetThemesList = XmlParser.GetListOfParametersFromXmlFile();

            CommonTheme.ThemeSelectedName = CommonTheme.CompleteListThemes.FirstOrDefault().ThemeSelected;
            // UIMenuSide.SetMenuItems();
            CommonTheme.LoadPropertiesFromXmlFile();
            SetThemes();

            //GlobalUsage.SetSqlitePragmaConfiguration();
        }

        /// <summary>
        /// Set all the themes color for the current window
        /// </summary>
        public void SetThemes()
        {
            //MainCalculator.Background = CommonTheme.MainCalculatorBackground;
            //UIMenuSide.UIMenuSelected.Foreground = CommonTheme.MainCalculatorForeground;
            //MainCalculator.BorderBrush = CommonTheme.MainCalculatorBorderBrush;
            //UICalculator.BackgroundBaseButtons = CommonTheme.BackgroundBaseButtons;
            //UICalculator.ForegroundBaseButtons = CommonTheme.ForegroundBaseButtons;
            //UICalculator.BorderBrushBaseButtons = CommonTheme.BorderBrushBaseButtons;
            //UICalculator.UISecondFuncButton.Background = CommonTheme.Background2ndeButton;
            //UICalculator.UISecondFuncButton.Foreground = CommonTheme.Foreground2ndeButton;
            //UICalculator.UISecondFuncButton.BorderBrush = CommonTheme.BorderBrush2ndeButton;
            //UICalculator.BackgroundScientificButtons = CommonTheme.BackgroundScientificButtons;
            //UICalculator.ForegroundScientificButtons = CommonTheme.ForegroundScientificButtons;
            //UICalculator.BorderBrushScientificButtons = CommonTheme.BorderBrushScientificButtons;
            //UICalculator.BackgroundOperatorsButtons = CommonTheme.BackgroundOperatorsButtons;
            //UICalculator.ForegroundOperatorsButtons = CommonTheme.ForegroundOperatorsButtons;
            //UICalculator.BorderBrushOperatorsButtons = CommonTheme.BorderBrushOperatorsButtons;
            //UICalculator.BackgroundNumericalsButtons = CommonTheme.BackgroundNumericalsButtons;
            //UICalculator.ForegroundNumericalsButtons = CommonTheme.ForegroundNumericalsButtons;
            //UICalculator.BorderBrushNumericalsButtons = CommonTheme.BorderBrushNumericalsButtons;
            //UICalculator.BackgroundMemoryButtons = CommonTheme.BackgroundMemoryButtons;
            //UICalculator.ForegroundMemoryButtons = CommonTheme.ForegroundMemoryButtons;
            //UICalculator.BorderBrushMemoryButtons = CommonTheme.BorderBrushMemoryButtons;
            //UICalculator.BackgroundTrigonometryButtons = CommonTheme.BackgroundTrigonometryButtons;
            //UICalculator.ForegroundTrigonometryButtons = CommonTheme.ForegroundTrigonometryButtons;
            //UICalculator.BorderBrushTrigonometryButtons = CommonTheme.BorderBrushTrigonometryButtons;
            //UIDisplay.UIDisplayBorderBrush = CommonTheme.MainCalculatorBorderBrush;
            //foreach (var lWindow in Application.Current.Windows)
            //{
            //    if (lWindow is ConsoleDebug)
            //    {
            //        (lWindow as ConsoleDebug).UIGridConsoleDebug.Background = CommonTheme.MainCalculatorBackground;
            //        foreach (var lChild in (lWindow as ConsoleDebug).GridCheckBoxes.Children)
            //        {
            //            if (lChild is CheckBox)
            //            {
            //                (lChild as CheckBox).Foreground = CommonTheme.MainCalculatorForeground;
            //            }
            //        }
            //    }
            //}
        }

        public void ModifyUIDisplay(string pContent)
        {
            UIDisplay.UIDisplayCalculus.Text = pContent;
        }

        public void SpecialKeysPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F3)
            {
                // Show/Hide consoleDebug window
                if (mConsoleDebug.IsVisible)
                {
                    mConsoleDebug.Hide();
                    this.Focus();
                }
                else
                {
                    mConsoleDebug.Show();
                    this.Focus();
                }
            }
        }

        #region Moving the Calculator

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            this.Opacity = 0.75F;
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            this.Opacity = 1F;
            base.OnMouseLeftButtonUp(e);
        }

        #endregion

        private void UIHistoryExpander_Expanded(object sender, RoutedEventArgs e)
        {
            TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: Expand the history panel");

            UIHistory.Visibility = Visibility.Visible;
        }

        private void UIHistoryExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            TraceLogs.AddOutput($"{GlobalUsage.GetCurrentMethodName}: Collapse the history panel");

            UIHistory.Visibility = Visibility.Collapsed;
        }
    }
}
