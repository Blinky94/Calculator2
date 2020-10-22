using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        public event SelectionChangedEventHandler OnSelectionChanged;

        private void ItemsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnSelectionChanged?.Invoke(sender, e);
        }
    }
}
