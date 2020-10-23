using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator.Buttons
{
    /// <summary>
    /// Interaction logic for NumericalButton_Control.xaml
    /// </summary>
    public partial class NumericalButton_Control : UserControl
    {
        public static readonly DependencyProperty NumericalButtonContentProperty = DependencyProperty.Register("NumericalButtonContent", typeof(string), typeof(NumericalButton_Control), new PropertyMetadata("Null"));

        public string NumericalButtonContent
        {
            get { return (string)GetValue(NumericalButtonContentProperty); }
            set { SetValue(NumericalButtonContentProperty, value); }
        }

        public NumericalButton_Control()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event to raise when general button is clicked
        /// </summary>
        public event RoutedEventHandler OnNumericalButtonClicked;

        private void NumericalButtonControl_Click(object sender, RoutedEventArgs e)
        {
            OnNumericalButtonClicked?.Invoke(sender, e);
        }
    }
}
