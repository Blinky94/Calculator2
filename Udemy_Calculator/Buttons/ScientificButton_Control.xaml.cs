using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator.Buttons
{
    /// <summary>
    /// Interaction logic for ScientificButton_Control.xaml
    /// </summary>
    public partial class ScientificButton_Control : UserControl
    {
        public static readonly DependencyProperty ScientificButtonContentProperty = DependencyProperty.Register("ScientificButtonContent", typeof(string), typeof(ScientificButton_Control), new PropertyMetadata("Null"));

        public string ScientificButtonContent
        {
            get { return (string)GetValue(ScientificButtonContentProperty); }
            set { SetValue(ScientificButtonContentProperty, value); }
        }

        public ScientificButton_Control()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event to raise when general button is clicked
        /// </summary>
        public event RoutedEventHandler OnScientificButtonClicked;

        private void ScientificButtonControl_Click(object sender, RoutedEventArgs e)
        {
            OnScientificButtonClicked?.Invoke(sender, e);
        }
    }
}
