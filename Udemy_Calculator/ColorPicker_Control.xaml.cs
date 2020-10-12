using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator
{
    public partial class ColorPicker_Control : UserControl
    {

        public static readonly DependencyProperty ColorPickerTextProperty =
          DependencyProperty.Register("ColorPickerText",
          typeof(string),
          typeof(ColorPicker_Control),
          new PropertyMetadata(""));
        public string ColorPickerText
        {
            get { return (string)GetValue(ColorPickerTextProperty); }
            set { SetValue(ColorPickerTextProperty, value); }
        }

        public ColorPicker_Control()
        {
            InitializeComponent();
        }

        private void UIColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            // HERE THE CODE UPDATE COLOR FOR CONTROL CONCERNED !!!

        }
    }
}
