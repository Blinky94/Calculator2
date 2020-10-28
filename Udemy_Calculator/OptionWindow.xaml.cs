using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for OptionWindow.xaml
    /// </summary>
    public partial class OptionWindow : Window
    {
        // Init singleton
        private static readonly OptionWindow mOptionWindow = new OptionWindow();

        private OptionWindow()
        {
            // Private constructor to instanciate local objects here 
            InitializeComponent();

            // Event on theme name selected
            ThemeListComboBox.OnSelectionChanged += new SelectionChangedEventHandler(ComboBoxThemeList_SelectionChanged);
            ComboBoxFontFamily1.OnSelectionChanged += new SelectionChangedEventHandler(ComboBoxFontFamily_SelectionChanged);
            ComboBoxFontSize1.OnSelectionChanged += new SelectionChangedEventHandler(ComboBoxFontSize_SelectionChanged);
            ComboBoxFontWeight1.OnSelectionChanged += new SelectionChangedEventHandler(ComboBoxFontWeight_SelectionChanged);
            ComboBoxFontFamily2.OnSelectionChanged += new SelectionChangedEventHandler(ComboBoxFontFamily_SelectionChanged);
            ComboBoxFontSize2.OnSelectionChanged += new SelectionChangedEventHandler(ComboBoxFontSize_SelectionChanged);
            ComboBoxFontWeight2.OnSelectionChanged += new SelectionChangedEventHandler(ComboBoxFontWeight_SelectionChanged);
            ComboBoxFontFamily3.OnSelectionChanged += new SelectionChangedEventHandler(ComboBoxFontFamily_SelectionChanged);
            ComboBoxFontSize3.OnSelectionChanged += new SelectionChangedEventHandler(ComboBoxFontSize_SelectionChanged);
            ComboBoxFontWeight3.OnSelectionChanged += new SelectionChangedEventHandler(ComboBoxFontWeight_SelectionChanged);
            Add_GeneralButtonControl.OnGeneralButtonClicked += new RoutedEventHandler(Button_Add);
            Cancel_GeneralButtonControl.OnGeneralButtonClicked += new RoutedEventHandler(Button_Cancel);
            Save_GeneralButtonControl.OnGeneralButtonClicked += new RoutedEventHandler(Button_Save);

            //Loading the xml configuration to provide access to all elements
            CommonTheme.LoadFromXmlProvider(this);

            // Applying every properties values to the listBox from selected theme
            PopulateListPropertiesToListView();

            // Combobox List for main theme to select
            ThemeListComboBox.ItemsComboBox.ItemsSource = CommonTheme.GetParentThemeNames;

            // List of font available
            List<string> lFontFamilyList = CommonTheme.GetFontList;
            // Get Formula font family
            ComboBoxFontFamily1.ItemsComboBox.ItemsSource = lFontFamilyList;
            // Get Chunk font family
            ComboBoxFontFamily2.ItemsComboBox.ItemsSource = lFontFamilyList;
            // Get Result font family
            ComboBoxFontFamily3.ItemsComboBox.ItemsSource = lFontFamilyList;

            // List of font size available
            var lFontSizeList = Enum.GetNames(typeof(SizeOfFont)).ToList();
            // Get Formula font size
            ComboBoxFontSize1.ItemsComboBox.ItemsSource = lFontSizeList;
            // Get Chunk font size
            ComboBoxFontSize2.ItemsComboBox.ItemsSource = lFontSizeList;
            // Get Result font size
            ComboBoxFontSize3.ItemsComboBox.ItemsSource = lFontSizeList;

            // List of font weight available
            List<string> lFontWeightList = CommonTheme.GetFontWeightList;
            // Get Formula font weight
            ComboBoxFontWeight1.ItemsComboBox.ItemsSource = lFontWeightList;
            // Get Chunk font weight
            ComboBoxFontWeight2.ItemsComboBox.ItemsSource = lFontWeightList;
            // Get Result font weight
            ComboBoxFontWeight3.ItemsComboBox.ItemsSource = lFontWeightList;

            // Selecting default values into controls
            if (CommonTheme.GetParentThemeLongNames.Count > 0)
            {
                ItemsListBox.SelectedItem = CommonTheme.GetParentThemeLongNames[0];
            }

            CommonTheme.ThemeSelectedName = CommonTheme.LoadXmlConfiguration?.SelectSingleNode("Themes/ThemeSelected/@name").Value;

            if (!string.IsNullOrEmpty(CommonTheme.ThemeSelectedName))
            {
                ThemeListComboBox.ItemsComboBox.SelectedIndex = ThemeListComboBox.ItemsComboBox.Items.IndexOf(CommonTheme.ThemeSelectedName);
            }
        }

        private void ComboBoxFontWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lCombo = (sender as ComboBox);
            var lComboTitle = (lCombo.Parent as Grid).Children[0] as Label;

            var lItemSelected = ItemsListBox.SelectedItem.ToString();
            foreach (var lTheme in CommonTheme.ListSelectedTheme.Where(p => p.SubThemeAttribute == lItemSelected))
            {
                if (lCombo.SelectedItem != null)
                {
                    if (lComboTitle.Uid.ToString() == "FontWeight1")
                    {
                        lTheme.FontWeight1 = GlobalUsage.ConvertStringToFontWeight(lCombo.SelectedItem.ToString());
                    }
                    if (lComboTitle.Uid.ToString() == "FontWeight2")
                    {
                        lTheme.FontWeight2 = GlobalUsage.ConvertStringToFontWeight(lCombo.SelectedItem.ToString());
                    }
                    if (lComboTitle.Uid.ToString() == "FontWeight3")
                    {
                        lTheme.FontWeight3 = GlobalUsage.ConvertStringToFontWeight(lCombo.SelectedItem.ToString());
                    }
                }
            }

            // Applying the new theme
            CommonTheme.SetSelectedThemeListObject(CommonTheme.ThemeSelectedName);
        }

        private void ComboBoxFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lCombo = (sender as ComboBox);
            var lComboTitle = (lCombo.Parent as Grid).Children[0] as Label;

            var lItemSelected = ItemsListBox.SelectedItem.ToString();
            foreach (var lTheme in CommonTheme.ListSelectedTheme.Where(p => p.SubThemeAttribute == lItemSelected))
            {
                if (lCombo.SelectedItem != null)
                {
                    if (lComboTitle.Uid.ToString() == "FontSize1")
                    {
                        lTheme.FontSize1 = (int)Enum.Parse(typeof(SizeOfFont), lCombo.SelectedItem.ToString());
                    }
                    if (lComboTitle.Uid.ToString() == "FontSize2")
                    {
                        lTheme.FontSize2 = (int)Enum.Parse(typeof(SizeOfFont), lCombo.SelectedItem.ToString());
                    }
                    if (lComboTitle.Uid.ToString() == "FontSize3")
                    {
                        lTheme.FontSize3 = (int)Enum.Parse(typeof(SizeOfFont), lCombo.SelectedItem.ToString());
                    }
                }
            }

            // Applying the new theme
            CommonTheme.SetSelectedThemeListObject(CommonTheme.ThemeSelectedName);
        }

        private void ComboBoxFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lCombo = (sender as ComboBox);
            var lComboTitle = (lCombo.Parent as Grid).Children[0] as Label;

            var lItemSelected = ItemsListBox.SelectedItem.ToString();
            foreach (var lTheme in CommonTheme.ListSelectedTheme.Where(p => p.SubThemeAttribute == lItemSelected))
            {
                if (lCombo.SelectedItem != null)
                {
                    if (lComboTitle.Uid.ToString() == "FontFamily1")
                    {
                        lTheme.FontFamily1 = new FontFamily(lCombo.SelectedItem.ToString());
                    }
                    if (lComboTitle.Uid.ToString() == "FontFamily2")
                    {
                        lTheme.FontFamily2 = new FontFamily(lCombo.SelectedItem.ToString());
                    }
                    if (lComboTitle.Uid.ToString() == "FontFamily3")
                    {
                        lTheme.FontFamily3 = new FontFamily(lCombo.SelectedItem.ToString());
                    }
                }
            }

            // Applying the new theme
            CommonTheme.SetSelectedThemeListObject(CommonTheme.ThemeSelectedName);
        }

        /// <summary>
        /// Applying values from list to the listBox control
        /// </summary>
        private void PopulateListPropertiesToListView()
        {
            ItemsListBox.ItemsSource = CommonTheme.GetParentThemeLongNames;
        }

        /// <summary>
        /// Instance Singleton
        /// </summary>
        /// <returns></returns>
        public static OptionWindow GetInstance()
        {
            return mOptionWindow;
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            //Save theme
            XmlParser.SaveTheme(CommonTheme.ThemeSelectedName);
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            //cancel change
            mOptionWindow.Hide();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            // Add new theme
        }

        private void ComboBoxThemeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lSelectedVal = (sender as ComboBox).SelectedValue.ToString();

            CommonTheme.ThemeSelectedName = lSelectedVal;

            // Updating the list of objects selected
            CommonTheme.SetSelectedThemeListObject(lSelectedVal);

            // Populate the list of properties (BaseButtons,ScientificButtons...)
            PopulateListPropertiesToListView();

            // Apply the default color for the first ListItem
            if (ItemsListBox != null && ItemsListBox.Items.Count > 0)
            {
                ItemsListBox_Selected(ItemsListBox, null);
            }
        }

        #region Moving the Window

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            this.Opacity = 1F;
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            this.Opacity = 1F;
            base.OnMouseLeftButtonUp(e);
        }

        #endregion

        /// <summary>
        /// Update Property value type to a Control with the right title to put on Label
        /// </summary>
        /// <param name="pFontSize"></param>
        /// <param name="pControl"></param>
        /// <param name="pTitle"></param>
        private static void UpdatePropertyValuesAndTitles(string pValue, Control pControl, string pTitle)
        {
            if (pControl is ComboBox_Control)
            {
                ComboBox_Control lComboBxCtrl = pControl as ComboBox_Control;
                if (!string.IsNullOrEmpty(pValue) && pValue != "0")
                {
                    // Activating parameter
                    lComboBxCtrl.Visibility = Visibility.Visible;
                    // Affecting the value
                    lComboBxCtrl.ItemsComboBox.SelectedItem = pValue;
                    // Set the right text for the property
                    lComboBxCtrl.ComboBoxTitle.Content = pTitle;
                }
                else
                {
                    // Desactivating the parameter
                    lComboBxCtrl.Visibility = Visibility.Collapsed;
                    lComboBxCtrl.ItemsComboBox.SelectedItem = null;
                }
            }
            else if (pControl is ColorPicker_Control)
            {
                ColorPicker_Control lColorPkrCtrl = pControl as ColorPicker_Control;
                if (!string.IsNullOrEmpty(pValue))
                {
                    // Activating parameter
                    lColorPkrCtrl.Visibility = Visibility.Visible;
                    // Affecting the color
                    lColorPkrCtrl.UIColorPicker.SelectedColor = ((SolidColorBrush)new BrushConverter().ConvertFromString(pValue)).Color;
                    // Set the right text for the property
                    lColorPkrCtrl.ColorPickerText = pTitle;
                }
                else
                {
                    // Desactivating the parameter
                    lColorPkrCtrl.Visibility = Visibility.Collapsed;
                    lColorPkrCtrl.UIColorPicker.SelectedColor = null;
                }
            }
        }

        private void ItemsListBox_Selected(object sender, RoutedEventArgs e)
        {
            if ((sender as ListBox).SelectedItem == null)
            {
                (sender as ListBox).SelectedItem = CommonTheme.GetParentThemeLongNames[0];
            }

            var lSelectedVal = (sender as ListBox).SelectedItem.ToString();

            foreach (var lTheme in CommonTheme.ListSelectedTheme.Where(p => p.SubThemeAttribute == lSelectedVal))
            {
                UpdatePropertyValuesAndTitles(lTheme.Color1?.ToString(), Color1Picker, lTheme.Color1Attribute);
                UpdatePropertyValuesAndTitles(lTheme.Color2?.ToString(), Color2Picker, lTheme.Color2Attribute);
                UpdatePropertyValuesAndTitles(lTheme.Color3?.ToString(), Color3Picker, lTheme.Color3Attribute);
                UpdatePropertyValuesAndTitles(lTheme.Color4?.ToString(), Color4Picker, lTheme.Color4Attribute);
                UpdatePropertyValuesAndTitles(lTheme.FontFamily1?.ToString(), ComboBoxFontFamily1, lTheme.FontFamilyAttribute1);
                UpdatePropertyValuesAndTitles(lTheme.FontFamily2?.ToString(), ComboBoxFontFamily2, lTheme.FontFamilyAttribute2);
                UpdatePropertyValuesAndTitles(lTheme.FontFamily3?.ToString(), ComboBoxFontFamily3, lTheme.FontFamilyAttribute3);
                UpdatePropertyValuesAndTitles(((SizeOfFont)lTheme.FontSize1).ToString(), ComboBoxFontSize1, lTheme.FontSizeAttribute1);
                UpdatePropertyValuesAndTitles(((SizeOfFont)lTheme.FontSize2).ToString(), ComboBoxFontSize2, lTheme.FontSizeAttribute2);
                UpdatePropertyValuesAndTitles(((SizeOfFont)lTheme.FontSize3).ToString(), ComboBoxFontSize3, lTheme.FontSizeAttribute3);

                string lVal = lTheme.FontWeight1 != null ? lTheme.FontWeight1.ToString() : string.Empty;
                UpdatePropertyValuesAndTitles(lVal, ComboBoxFontWeight1, lTheme.FontWeightAttribute1);

                lVal = lTheme.FontWeight2 != null ? lTheme.FontWeight2.ToString() : string.Empty;
                UpdatePropertyValuesAndTitles(lVal, ComboBoxFontWeight2, lTheme.FontWeightAttribute2);

                lVal = lTheme.FontWeight3 != null ? lTheme.FontWeight3.ToString() : string.Empty;
                UpdatePropertyValuesAndTitles(lVal, ComboBoxFontWeight3, lTheme.FontWeightAttribute3);
            }
        }
    }
}
