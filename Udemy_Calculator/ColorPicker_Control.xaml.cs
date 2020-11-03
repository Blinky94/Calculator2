using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Udemy_Calculator.SerializedObjects;

namespace Udemy_Calculator
{
    public partial class ColorPicker_Control : UserControl
    {
        public static readonly DependencyProperty ColorPickerTextProperty = DependencyProperty.Register("ColorPickerText", typeof(string), typeof(ColorPicker_Control), new PropertyMetadata(""));
        public string ColorPickerText
        {
            get { return (string)GetValue(ColorPickerTextProperty); }
            set { SetValue(ColorPickerTextProperty, value); }
        }

        public static readonly DependencyProperty ColorPickerUIDProperty = DependencyProperty.Register("ColorPickerUID", typeof(string), typeof(ColorPicker_Control), new PropertyMetadata(""));
        public string ColorPickerUID
        {
            get { return (string)GetValue(ColorPickerUIDProperty); }
            set { SetValue(ColorPickerUIDProperty, value); }
        }
        public ColorPicker_Control()
        {
            InitializeComponent();
        }

        private void UIColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            if (e.NewValue == null)
            {
                return;
            }

            SolidColorBrush lColorSelected = new SolidColorBrush((Color)e.NewValue);

            string lSkinLongName = string.Empty;
            string lSkinSelectedName = string.Empty;
            
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() != typeof(OptionWindow))
                {
                    continue;
                }
                var loPtionWindow = window as OptionWindow;

                var lItemSelected = loPtionWindow.SkinStylesLongNameListBox.SelectedItem.ToString();
                lSkinLongName = lItemSelected;
                lSkinSelectedName = CommonSkins.SelectedSkinObj.Skin.Where(p => p.LongName == lItemSelected).Select(pStyle => pStyle.Name).FirstOrDefault();

                var lColorPickerUID = TitleLabel.Uid.ToString();

                var lStyle = (SkinStylesCls)CommonSkins.SelectedSkinObj.Skin.Where(p => p.LongName == lItemSelected).Select(pStyle => pStyle).FirstOrDefault();

                if (lColorPickerUID.Contains("Background"))
                {
                    CommonSkins.SelectedSkinObj.Skin.Where(p => p.LongName == lItemSelected).Select(p => p.Background).FirstOrDefault().Value = lColorSelected.ToString();
                }
                else if (lColorPickerUID.Contains("Foreground"))
                {
                    CommonSkins.SelectedSkinObj.Skin.Where(p => p.LongName == lItemSelected).Select(p => p.Foreground).FirstOrDefault().Value = lColorSelected.ToString();
                }
                else if (lColorPickerUID.Contains("Borderbrush"))
                {
                    CommonSkins.SelectedSkinObj.Skin.Where(p => p.LongName == lItemSelected).Select(p => p.Borderbrush).FirstOrDefault().Value = lColorSelected.ToString();
                }
            }

            // Applying the new Skin
            CommonSkins.UpdateResourcesWithSkins(lSkinSelectedName);
        }
    }
}
