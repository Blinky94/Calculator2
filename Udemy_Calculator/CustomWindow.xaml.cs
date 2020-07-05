using ColorPickerWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for SecondWindow.xaml
    /// </summary>
    public partial class CustomWindow : Window
    {
        public CustomWindow()
        {
            InitializeComponent();

            ColorPickerControl lColorPickerWPF = new ColorPickerControl();
            lColorPickerWPF.Visibility = Visibility.Visible;

            UNCANVAS.Children.Add(lColorPickerWPF);
        }


        private List<ThemeObj> mListSelectedTheme;

        public void AddRadioButtonsList(List<ThemeObj> pListTheme)
        {
            string lLastLabel = string.Empty;

            mListSelectedTheme = pListTheme?.Where(p => p.ThemeSelected == p.ParentThemeName).ToList();

            mListSelectedTheme.ForEach(p =>
            {
                if (lLastLabel != p.ChildThemeText)
                {
                    Label lLabel = new Label();
                    lLabel.Content = p.ChildThemeText;
                    UICustomThemes_RadioButtonList.Children.Add(lLabel);
                    lLastLabel = p.ChildThemeText;
                }

                RadioButton lRadioButton = new RadioButton();
                lRadioButton.Content = p.ParameterName;
                lRadioButton.Checked += LRadioButton_Checked;

                UICustomThemes_RadioButtonList.Children.Add(lRadioButton);

            });
        }

        private void LRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            string lRButtonName = (e.OriginalSource as RadioButton).Content.ToString();

            mListSelectedTheme.ForEach(p =>
            {
                if (p.ParameterName == lRButtonName)
                {
                    
                }
            });
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            //Open theme
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            //Add new theme
        }
    }
}
