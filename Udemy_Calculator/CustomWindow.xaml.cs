using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

            // Set the custom window theme position next to the main window calculator application
            Canvas.SetLeft(this, Application.Current.MainWindow.Left + Application.Current.MainWindow.Width);
            Canvas.SetTop(this, Application.Current.MainWindow.Top);
        }

        public void SetControlsCustomThemes()
        {
            // Create control item them depending of type concerned
            CommonTheme.ListThemesWithThemeSelected?.ForEach(p =>
            {
                SetElementsThemes(p);
            });

            // Set the ComboBox list themes with the selected one
            SetMainThemeList(CommonTheme.ListParentThemesName, CommonTheme.ThemeSelectedName);

            // For all
            //SetTheme(value, name);
        }

        private void SetCustomTheme()
        {
            foreach (var lChild in UICustomThemesList.Children)
            {
                if (lChild is Label)
                {
                    (lChild as Label).Foreground = CommonTheme.MainCalculatorForeground;
                }
                if (lChild is ColorPicker_Control)
                {
                    (lChild as ColorPicker_Control).UITitle.Foreground = CommonTheme.MainCalculatorForeground;
                    (lChild as ColorPicker_Control).UIColorPicker.SelectedColor = CommonTheme.MainCalculatorBackground.Color;

                }
                if (lChild is Spinner_Control)
                {
                    (lChild as Spinner_Control).UITitle.Foreground = CommonTheme.MainCalculatorBackground;
                }
            }
        }

        // take the last label in memory to appear only one time by theme (first is an empty one)
        private string mLastLabel = string.Empty;

        /// <summary>
        /// Fill the UICustomThemesList with right custom theme element (Color Picker, Spinner...)
        /// </summary>
        /// <param name="pElements"></param>
        private void SetElementsThemes(ThemeElements pElements)
        {
            if (mLastLabel != pElements.ChildThemeText)
            {
                Label lLabel = new Label
                {
                    Content = pElements.ChildThemeText,
                    FontSize = 15,
                    Margin = new Thickness(5, 0, 0, 0)
                };

                UICustomThemesList.Children.Add(lLabel);

                mLastLabel = pElements.ChildThemeText;
            }

            // Type ColorPicker : If there are text and if it not thickness option, create ControlPicker control
            if (pElements.ParameterText.Length > 0 && pElements.ParameterText != "Thickness")
            {
                ColorPicker_Control lColorPicker = new ColorPicker_Control();
                lColorPicker.UITitle.Text = pElements.ParameterText;
                lColorPicker.UITitle.Margin = new Thickness(10, 0, 0, 0);
                lColorPicker.UIColorPicker.Uid = pElements.ParameterName;
                lColorPicker.UIColorPicker.SelectedColorChanged += UIColorPicker_SelectedColorChanged;

                if (pElements.ParameterValue != "")
                {
                    lColorPicker.UIColorPicker.SelectedColor = (Color)ColorConverter.ConvertFromString(pElements.ParameterValue);
                }

                UICustomThemesList.Children.Add(lColorPicker);

#if DEBUG
                //Debug.WriteLine("COUNTING PICKER : " + UICustomThemesList?.Children?.OfType<ColorPicker_Control>().ToList().Count().ToString());
                //Debug.WriteLine("COUNTING LABEL : " + UICustomThemesList?.Children?.OfType<Label>().ToList().Count().ToString());
                //Debug.WriteLine("COUNTING SPINNER : " + UICustomThemesList?.Children?.OfType<Spinner_Control>().ToList().Count().ToString());
#endif
            }

            // Type Spinner : If it is thickness option, create Spinner control
            if (pElements.ParameterText == "Thickness")
            {
                Spinner_Control lSpinner = new Spinner_Control();

                if (pElements.ParameterValue != "")
                {
                    lSpinner.UITitle.Text = pElements.ParameterText;
                    lSpinner.UITitle.Margin = new Thickness(10, 0, 0, 0);

                    lSpinner.UIText.Uid = pElements.ParameterName;
                    lSpinner.UIText.HorizontalContentAlignment = HorizontalAlignment.Right;
                    lSpinner.UIText.TextChanged += UISpinner_SelectedThicknessChanged;
                    lSpinner.UIButtonUp.Uid = pElements.ParameterName;
                    lSpinner.UIButtonDown.Uid = pElements.ParameterName;
                    lSpinner.UIButtonUp.Click += UIButton_Click;
                    lSpinner.UIButtonDown.Click += UIButton_Click;

                    lSpinner.UIText.Text = pElements.ParameterValue;
                }
                else
                {
                    // If no value on thickness parameter, the zone is taken but hidden
                    lSpinner.Visibility = Visibility.Hidden;
                }

                UICustomThemesList.Children.Add(lSpinner);
            }
        }

        /// <summary>
        /// Define the ComboxBox list of themes
        /// </summary>
        /// <param name="pListParentThemeName"></param>
        /// <param name="pThemeSelected"></param>
        private void SetMainThemeList(List<string> pListParentThemeName, string pThemeSelected)
        {
            pListParentThemeName?.ForEach(p =>
            {
                ComboBoxThemeList.Items.Add(new ComboBoxItem()
                {
                    Content = p,
                    IsSelected = p == pThemeSelected
                });
            });
        }

        /// <summary>
        /// Event : UI Button from spinner control, when clicking up or down, value is changing simultaneously
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIButton_Click(object sender, RoutedEventArgs e)
        {
            //TextBox lUITextBox = null;
            string lControlName = (e.Source as Button).Name;
            string lControlDestName = (e.Source as Button).Uid;

            // Managing thickness changements, set the spinner control concerned
            UICustomThemesList.Children.OfType<Spinner_Control>().Where(p => p.UIText.Uid == (e.Source as Button).Uid).ToList().ForEach(p =>
            {
                if (double.TryParse(lUITextBox.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double lUITextValue))
                {
                    if (lControlName == "UIButtonUp")
                    {
                        lUITextValue += 0.1;
                    }
                    else if (lControlName == "UIButtonDown")
                    {
                        if (lUITextValue > 0.0)
                        {
                            lUITextValue -= 0.1;
                        }
                    }

                    p.UIText.Text = lUITextValue.ToString();
                    //if (p. is Label)
                    //{
                    //    (lChild as Label).Foreground = CommonTheme.MainCalculatorForeground;
                    //}
                }              
            });

            //if(lControlName == ((MainWindow)Application.Current.MainWindow).mainCalculator.UICalculator.)
            //((MainWindow)Application.Current.MainWindow).mainCalculator.UICalculator.BorderThicknessBaseButtons = 
        }

        /// <summary>
        /// Event changing on each UICustomList control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            var lControlName = (e.OriginalSource as Xceed.Wpf.Toolkit.ColorPicker).Uid;
            var lControlValue = (e.OriginalSource as Xceed.Wpf.Toolkit.ColorPicker).SelectedColorText;

            CommonTheme.SetProperty(lControlName, lControlValue);

            //Applying theme to the MainWindow
            ((MainWindow)Application.Current.MainWindow).mainCalculator.SetThemes();
            Canvas.Background = CommonTheme.MainCalculatorBackground;
            foreach (var lChild in UICustomThemesList.Children)
            {
                if (lChild is Label)
                {
                    (lChild as Label).Foreground = CommonTheme.MainCalculatorForeground;
                }
            }
        }

        /// <summary>
        /// Set thickness theme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UISpinner_SelectedThicknessChanged(object sender, TextChangedEventArgs e)
        {
            //CommonTheme.SetProperty((e.OriginalSource as TextBox).Uid, (e.OriginalSource as TextBox).Text.Replace(',', '.'));
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            //Save theme
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            //cancel change
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            // Add new theme
        }

        private void ComboBoxThemeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lSelectedItem = (sender as ComboBox).SelectedItem;
            CommonTheme.ThemeSelectedName = (lSelectedItem as ComboBoxItem).Content.ToString();
            CommonTheme.SetThemesProperties();
        }
    }
}
