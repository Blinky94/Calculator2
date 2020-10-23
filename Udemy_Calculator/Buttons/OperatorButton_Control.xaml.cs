using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator.Buttons
{
    /// <summary>
    /// Interaction logic for OperatorButton_Control.xaml
    /// </summary>
    public partial class OperatorButton_Control : UserControl
    {
        public static readonly DependencyProperty OperatorButtonContentProperty = DependencyProperty.Register("OperatorButtonContent", typeof(string), typeof(OperatorButton_Control), new PropertyMetadata("Null"));

        public string OperatorButtonContent
        {
            get { return (string)GetValue(OperatorButtonContentProperty); }
            set { SetValue(OperatorButtonContentProperty, value); }
        }

        public OperatorButton_Control()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event to raise when general button is clicked
        /// </summary>
        public event RoutedEventHandler OnOperatorButtonClicked;

        private void OperatorButtonControl_Click(object sender, RoutedEventArgs e)
        {
            OnOperatorButtonClicked?.Invoke(sender, e);
        }
    }
}
