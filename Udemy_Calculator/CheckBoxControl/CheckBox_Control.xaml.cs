using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator.CheckBox
{
    /// <summary>
    /// Interaction logic for CheckBox_Control.xaml
    /// </summary>
    public partial class CheckBox_Control : UserControl
    {
        public static readonly DependencyProperty CheckBoxContentProperty = DependencyProperty.Register("CheckBoxContent", typeof(string), typeof(CheckBox_Control), new PropertyMetadata("Null"));

        public string CheckBoxContent
        {
            get { return (string)GetValue(CheckBoxContentProperty); }
            set { SetValue(CheckBoxContentProperty, value); }
        }

        public CheckBox_Control()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event to raise when checkbox is checked
        /// </summary>
        public event RoutedEventHandler OnCheckBoxChecked;

        private void CheckBoxControl_Checked(object sender, RoutedEventArgs e)
        {
            OnCheckBoxChecked?.Invoke(sender, e);
        }

        /// <summary>
        /// Event to raise when checkbox is unchecked
        /// </summary>
        public event RoutedEventHandler OnCheckBoxUnChecked;

        private void CheckBoxControl_Unchecked(object sender, RoutedEventArgs e)
        {
            OnCheckBoxUnChecked?.Invoke(sender, e);
        }
    }
}
