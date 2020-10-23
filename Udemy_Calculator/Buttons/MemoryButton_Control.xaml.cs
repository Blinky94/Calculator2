using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator.Buttons
{
    /// <summary>
    /// Interaction logic for MemoryButton_Control.xaml
    /// </summary>
    public partial class MemoryButton_Control : UserControl
    {
        public static readonly DependencyProperty MemoryButtonContentProperty = DependencyProperty.Register("MemoryButtonContent", typeof(string), typeof(MemoryButton_Control), new PropertyMetadata("Null"));
      
        public string MemoryButtonContent
        {
            get { return (string)GetValue(MemoryButtonContentProperty); }
            set { SetValue(MemoryButtonContentProperty, value); }
        }

        public MemoryButton_Control()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event to raise when general button is clicked
        /// </summary>
        public event RoutedEventHandler OnMemoryButtonClicked;

        private void MemoryButtonControl_Click(object sender, RoutedEventArgs e)
        {
            OnMemoryButtonClicked?.Invoke(sender, e);
        }
    }
}
