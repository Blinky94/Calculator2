using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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
           // SetDefaultThemeCalculator();


            UIMenuSide.UIMenuSelected.Content = CalculatorMode.Standard.ToString();
            UIDisplay.UIDisplayCalculus.Text = "0";

            UIDisplayValueEvent += new UpdateUIDisplayHandler(ModifyUIDisplay);
            UICalculator.CalculusDisplayDelegate = UIDisplayValueEvent;
        }

        private void SetDefaultThemeCalculator()
        {
            XmlParameters lParams = new XmlParameters();
            lParams.ReadParameters();
            UIMenuSide.GetThemesList = lParams.GetListParameters;

            //Set default theme
            UIMenuSide.SetTheme();

            UIMenuSide.SetMenuIcon(UIMenuSide.MainCalculatorBackground);

            // Set Main color background
            MainCalculator.Background = (Brush)new BrushConverter().ConvertFrom(UIMenuSide.MainCalculatorBackground);
            // Set Main color borderbrush
            MainCalculator.BorderBrush = (Brush)new BrushConverter().ConvertFrom(UIMenuSide.MainCalculatorBorderBrush);
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
