using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region MainCalculatorBackground

        public static readonly DependencyProperty MainCalculatorBackgroundProperty =
            DependencyProperty.Register("MainCalculatorBackground",
            typeof(Brush),
            typeof(MainWindow),
            new PropertyMetadata(Brushes.Transparent));
        public Brush MainCalculatorBackground
        {
            get { return (Brush)GetValue(MainCalculatorBackgroundProperty); }
            set { SetValue(MainCalculatorBackgroundProperty, value); }
        }

        #endregion

        #region MainCalculatorBorderBrush

        public static readonly DependencyProperty MainCalculatorBorderBrushProperty =
            DependencyProperty.Register("MainCalculatorBorderBrush",
            typeof(Brush),
            typeof(MainWindow),
            new PropertyMetadata(Brushes.Transparent));
        public Brush MainCalculatorBorderBrush
        {
            get { return (Brush)GetValue(MainCalculatorBorderBrushProperty); }
            set { SetValue(MainCalculatorBorderBrushProperty, value); }
        }

        #endregion

        #region MainCalculatorBorderThickness

        public static readonly DependencyProperty MainCalculatorBorderThicknessProperty =
            DependencyProperty.Register("MainCalculatorBorderThickness",
            typeof(double),
            typeof(MainWindow),
            new PropertyMetadata(5D));
        public double MainCalculatorBorderThickness
        {
            get { return (double)GetValue(MainCalculatorBorderThicknessProperty); }
            set { SetValue(MainCalculatorBorderThicknessProperty, value); }
        }

        #endregion


        public delegate void UpdateUIDisplayHandler(string pContent);
        public event UpdateUIDisplayHandler UIDisplayValueEvent;

        public MainWindow()
        {
            InitializeComponent();

            UIMenuSide.UIMenuSelected.Content = CalculatorMode.Standard.ToString();
            UIDisplay.UIDisplayCalculus.Text = "0";

            UIDisplayValueEvent += new UpdateUIDisplayHandler(ModifyUIDisplay);
            UICalculator.CalculusDisplayDelegate = UIDisplayValueEvent;

            var bookData = (XmlDataProvider)FindResource("ThemeList");
            var xmlDoc = bookData.Document;
            //UIMenuSide.GetThemesList = xdp.Data;
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
