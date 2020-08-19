using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void UpdateUIDisplayHandler(string pContent);
        public event UpdateUIDisplayHandler UIDisplayValueEvent;

        public MainWindow()
        {
            InitializeComponent();

            UIMenuSide.UIMenuSelected.Content = CalculatorMode.Standard.ToString();

            UIDisplayValueEvent += new UpdateUIDisplayHandler(ModifyUIDisplay);
            UICalculator.CalculusDisplayDelegate = UIDisplayValueEvent;
           
            XmlParser.OpenTheme();
            UIMenuSide.SetThemesList = XmlParser.GetListOfParametersFromXmlFile();
            
            CommonTheme.ThemeSelectedName = CommonTheme.CompleteListThemes.FirstOrDefault().ThemeSelected;
            // UIMenuSide.SetMenuItems();
            CommonTheme.SetThemesProperties();
            SetThemes();
        }

        /// <summary>
        /// Set all the themes color for the current window
        /// </summary>
        public void SetThemes()
        {
            MainCalculator.Background = CommonTheme.MainCalculatorBackground;
            UIMenuSide.UIMenuSelected.Foreground = CommonTheme.MainCalculatorForeground;
            MainCalculator.BorderBrush = CommonTheme.MainCalculatorBorderBrush;
            MainCalculator.BorderThickness = new Thickness(CommonTheme.MainCalculatorBorderThickness);
            UICalculator.BackgroundBaseButtons = CommonTheme.BackgroundBaseButtons;
            UICalculator.ForegroundBaseButtons = CommonTheme.ForegroundBaseButtons;
            UICalculator.BorderBrushBaseButtons = CommonTheme.BorderBrushBaseButtons;
            UICalculator.BorderThicknessBaseButtons = CommonTheme.BorderThicknessBaseButtons;
            UICalculator.UISecondFuncButton.Background = CommonTheme.Background2ndeButton;
            UICalculator.UISecondFuncButton.Foreground = CommonTheme.Foreground2ndeButton;
            UICalculator.UISecondFuncButton.BorderBrush = CommonTheme.BorderBrush2ndeButton;
            UICalculator.BackgroundScientificButtons = CommonTheme.BackgroundScientificButtons;
            UICalculator.ForegroundScientificButtons = CommonTheme.ForegroundScientificButtons;
            UICalculator.BorderBrushScientificButtons = CommonTheme.BorderBrushScientificButtons;
            UICalculator.BackgroundOperatorsButtons = CommonTheme.BackgroundOperatorsButtons;
            UICalculator.ForegroundOperatorsButtons = CommonTheme.ForegroundOperatorsButtons;
            UICalculator.BorderBrushOperatorsButtons = CommonTheme.BorderBrushOperatorsButtons;
            UICalculator.BackgroundNumericalsButtons = CommonTheme.BackgroundNumericalsButtons;
            UICalculator.ForegroundNumericalsButtons = CommonTheme.ForegroundNumericalsButtons;
            UICalculator.BorderBrushNumericalsButtons = CommonTheme.BorderBrushNumericalsButtons;
            UICalculator.BackgroundMemoryButtons = CommonTheme.BackgroundMemoryButtons;
            UICalculator.ForegroundMemoryButtons = CommonTheme.ForegroundMemoryButtons;
            UICalculator.BorderBrushMemoryButtons = CommonTheme.BorderBrushMemoryButtons;
            UICalculator.BackgroundTrigonometryButtons = CommonTheme.BackgroundTrigonometryButtons;
            UICalculator.ForegroundTrigonometryButtons = CommonTheme.ForegroundTrigonometryButtons;
            UICalculator.BorderBrushTrigonometryButtons = CommonTheme.BorderBrushTrigonometryButtons;
        }
    
        public void ModifyUIDisplay(string pContent)
        {
            UIDisplay.UIDisplayCalculus.Text = pContent;
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
    }
}
