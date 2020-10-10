using System.Windows.Controls;

namespace Udemy_Calculator
{
    public partial class ColorPicker_Control : UserControl
    {
        public ColorPicker_Control()
        {
            InitializeComponent();
        }

        private void UIColorPicker_SelectedColorChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            // HERE THE CODE UPDATE COLOR FOR CONTROL CONCERNED !!!
        }
    }
}
