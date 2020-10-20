using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            SolidColorBrush lColorSelected = new SolidColorBrush((Color)e.NewValue);

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(OptionWindow))
                {
                    var loPtionWindox = window as OptionWindow;

                    var lItemSelected = loPtionWindox.ItemsListBox.SelectedItem.ToString();
                    var lColorPickerText = TitleLabel.Content.ToString();

                    CommonTheme.ListOfAllThemes.Where(p => p.ThemeRootName == CommonTheme.ThemeSelectedName).Where(p => p.ParentLongName == lItemSelected).ToList().ForEach(p =>
                    {
                        if(lColorPickerText == "Color1")
                        {
                            p.Color1 = lColorSelected;
                        }
                        else if (lColorPickerText == "Color2")
                        {
                            p.Color2 = lColorSelected;
                        }
                        else if (lColorPickerText == "Color3")
                        {
                            p.Color3 = lColorSelected;
                        }
                        else if (lColorPickerText == "Color4")
                        {
                            p.Color4 = lColorSelected;
                        }
                    });
                }
            }

            // Applying the new theme
            CommonTheme.SetSelectedThemeListObject(CommonTheme.ThemeSelectedName);
        }
    }
}
