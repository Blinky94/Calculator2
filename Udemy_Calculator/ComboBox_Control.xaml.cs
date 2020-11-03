using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for ComboBox_Control.xaml
    /// </summary>
    public partial class ComboBox_Control : UserControl
    {
        //ComboBoxText
        public static readonly DependencyProperty ComboBoxTextProperty = DependencyProperty.Register("ComboBoxText", typeof(string), typeof(ComboBox_Control), new PropertyMetadata(""));
        public string ComboBoxText
        {
            get { return (string)GetValue(ComboBoxTextProperty); }
            set { SetValue(ComboBoxTextProperty, value); }
        }

        //ComboBoxUID
        public static readonly DependencyProperty ComboBoxUIDProperty = DependencyProperty.Register("ComboBoxUID", typeof(string), typeof(ComboBox_Control), new PropertyMetadata(""));
        public string ComboBoxUID
        {
            get { return (string)GetValue(ComboBoxUIDProperty); }
            set { SetValue(ComboBoxUIDProperty, value); }
        }

        public ComboBox_Control()
        {
            InitializeComponent();
        }

        private void ItemsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lCombo = (sender as ComboBox);
            var lComboTitle = (lCombo.Parent as Grid).Children[0] as Label;

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() != typeof(OptionWindow))
                {
                    continue;
                }
                var loPtionWindow = window as OptionWindow;

                var lItemSelected = loPtionWindow.SkinStylesLongNameListBox.SelectedItem.ToString();
                if (lCombo.SelectedItem != null)
                {
                    var lVal = lCombo.SelectedItem.ToString();

                    if (lComboTitle.Uid == "FontFamily")
                    {
                        CommonSkins.SelectedSkinObj.Skin.Where(p => p.LongName == lItemSelected).Select(p => p.FontFamily).FirstOrDefault().Value = lVal;
                    }
                    else if (lComboTitle.Uid == "FontSize")
                    {
                        var lllConverted = ((int)Enum.Parse(typeof(SizeOfFont), lVal)).ToString();
                        CommonSkins.SelectedSkinObj.Skin.Where(p => p.LongName == lItemSelected).Select(p => p.FontSize).FirstOrDefault().Value = lllConverted;
                    }
                    else if (lComboTitle.Uid == "FontWeight")
                    {
                        CommonSkins.SelectedSkinObj.Skin.Where(p => p.LongName == lItemSelected).Select(p => p.FontWeight).FirstOrDefault().Value = GlobalUsage.ConvertStringToFontWeight(lVal).ToString();
                    }
                }

                // Applying the new Skin
                CommonSkins.UpdateResourcesWithSkins();
            }
        }
    }
}
