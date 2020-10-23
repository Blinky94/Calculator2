using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator.Buttons
{
    /// <summary>
    /// Interaction logic for BaseButton_Control.xaml
    /// </summary>
    public partial class BaseButton_Control : UserControl
    {
        public static readonly DependencyProperty BaseButtonContentProperty = DependencyProperty.Register("BaseButtonContent", typeof(string), typeof(BaseButton_Control), new PropertyMetadata("Null"));

        public string BaseButtonContent
        {
            get { return (string)GetValue(BaseButtonContentProperty); }
            set { SetValue(BaseButtonContentProperty, value); }
        }

        public BaseButton_Control()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event to raise when general button is clicked
        /// </summary>
        public event RoutedEventHandler OnBaseButtonClicked;

        private void BaseButtonControl_Click(object sender, RoutedEventArgs e)
        {
            OnBaseButtonClicked?.Invoke(sender, e);
        }
    }
}
