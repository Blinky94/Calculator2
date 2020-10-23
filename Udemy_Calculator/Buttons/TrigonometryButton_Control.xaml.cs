using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator.Buttons
{
    /// <summary>
    /// Interaction logic for TrigonometryButton_Control.xaml
    /// </summary>
    public partial class TrigonometryButton_Control : UserControl
    {
        public static readonly DependencyProperty TrigonometryButtonContentProperty = DependencyProperty.Register("TrigonometryButtonContent", typeof(string), typeof(TrigonometryButton_Control), new PropertyMetadata("Null"));

        public string TrigonometryButtonContent
        {
            get { return (string)GetValue(TrigonometryButtonContentProperty); }
            set { SetValue(TrigonometryButtonContentProperty, value); }
        }
        public TrigonometryButton_Control()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event to raise when general button is clicked
        /// </summary>
        public event RoutedEventHandler OnTrigonometryButtonClicked;

        private void TrigonometryButtonControl_Click(object sender, RoutedEventArgs e)
        {
            OnTrigonometryButtonClicked?.Invoke(sender, e);
        }
    }
}
