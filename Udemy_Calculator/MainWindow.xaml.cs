using System.Windows;
using System.Windows.Input;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Event to raise number pressed by user to display to the UIDisplay
        public delegate void EventUpdateUIDisplayHandler(string pContent);
        public event EventUpdateUIDisplayHandler UIEventDisplayValueEvent;
        internal static ConsoleDebug mConsoleDebug;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            UIMenuSide.UIMenuSelected.Content = CalculatorMode.Scientific.ToString();

            // Event registered to the UIDisplay when number is typed
            UIEventDisplayValueEvent += new EventUpdateUIDisplayHandler(ModifyUIDisplay);
            UICalculator.CalculusDisplayDelegate = UIEventDisplayValueEvent;

            //mConsoleDebug = new ConsoleDebug();
            //mConsoleDebug.Show();
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
