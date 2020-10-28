using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for HistoryStyle_Control.xaml
    /// </summary>
    public partial class HistoryStyle_Control : UserControl
    {
        //colorPickerColorText
        public static readonly DependencyProperty ColorPickerColorTextProperty = DependencyProperty.Register("ColorPickerColorText", typeof(string), typeof(GeneralButton_Control), new PropertyMetadata(""));
        public string ColorPickerColorText
        {
            get { return (string)GetValue(ColorPickerColorTextProperty); }
            set { SetValue(ColorPickerColorTextProperty, value); }
        }

        //comboBoxFamilyText
        public static readonly DependencyProperty ComboBoxFamilyTextProperty = DependencyProperty.Register("ComboBoxFamilyText", typeof(string), typeof(GeneralButton_Control), new PropertyMetadata(""));
        public string ComboBoxFamilyText
        {
            get { return (string)GetValue(ComboBoxFamilyTextProperty); }
            set { SetValue(ComboBoxFamilyTextProperty, value); }
        }

        //comboBoxSizeText
        public static readonly DependencyProperty ComboBoxSizeTextProperty = DependencyProperty.Register("ComboBoxSizeText", typeof(string), typeof(GeneralButton_Control), new PropertyMetadata(""));
        public string ComboBoxSizeText
        {
            get { return (string)GetValue(ComboBoxSizeTextProperty); }
            set { SetValue(ComboBoxSizeTextProperty, value); }
        }

        //comboBoxWeightText
        public static readonly DependencyProperty ComboBoxWeightTextProperty = DependencyProperty.Register("ComboBoxWeightText", typeof(string), typeof(GeneralButton_Control), new PropertyMetadata(""));
        public string ComboBoxWeightText
        {
            get { return (string)GetValue(ComboBoxWeightTextProperty); }
            set { SetValue(ComboBoxWeightTextProperty, value); }
        }

        public HistoryStyle_Control()
        {
            InitializeComponent();
        }
    }
}
