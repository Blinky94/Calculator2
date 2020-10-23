using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for GeneralButton_Control.xaml
    /// </summary>
    public partial class GeneralButton_Control : UserControl
    {
        public static readonly DependencyProperty GeneralImageUrlProperty = DependencyProperty.Register("GeneralImageUrl", typeof(string), typeof(GeneralButton_Control), new PropertyMetadata(""));
        public string GeneralImageUrl
        {
            get { return (string)GetValue(GeneralImageUrlProperty); }
            set { SetValue(GeneralImageUrlProperty, value); }
        }

        public static readonly DependencyProperty GeneralImageUrlReverseProperty = DependencyProperty.Register("GeneralImageUrlReverse", typeof(string), typeof(GeneralButton_Control), new PropertyMetadata(""));
        public string GeneralImageUrlReverse
        {
            get { return (string)GetValue(GeneralImageUrlReverseProperty); }
            set { SetValue(GeneralImageUrlReverseProperty, value); }
        } 

        public GeneralButton_Control()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event to raise when general button is clicked
        /// </summary>
        public event RoutedEventHandler OnGeneralButtonClicked;

        private void GeneralButton_Click(object sender, RoutedEventArgs e)
        {
            OnGeneralButtonClicked?.Invoke(sender, e);
        }
    }
}
