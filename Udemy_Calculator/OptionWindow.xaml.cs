using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Udemy_Calculator.SerializedObjects;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for OptionWindow.xaml
    /// </summary>
    public partial class OptionWindow : Window
    {
        // Init singleton
        private static readonly OptionWindow mOptionWindow = new OptionWindow();

        /// <summary>
        /// Initialize all the local event
        /// </summary>
        private void SetAllLocalEvents()
        {
            // Event on Skin name selected
            Add_GeneralButtonControl.OnGeneralButtonClicked += new RoutedEventHandler(Button_Add);
            Cancel_GeneralButtonControl.OnGeneralButtonClicked += new RoutedEventHandler(Button_Cancel);
            Save_GeneralButtonControl.OnGeneralButtonClicked += new RoutedEventHandler(Button_Save);
        }

        /// <summary>
        /// Define all the lists values in every History ComboBox
        /// </summary>
        private void SetHistoryListToControls()
        {
            // List of font available
            List<string> lFontFamilyList = CommonSkins.GetFontList;
            // Get Formula font family
            FontFamilyComboBox.ItemsComboBox.ItemsSource = lFontFamilyList;
            // Get Chunk font family
            FontSizeComboBox.ItemsComboBox.ItemsSource = Enum.GetNames(typeof(SizeOfFont)).ToList();
            // Get Result font family
            FontWeightComboBox.ItemsComboBox.ItemsSource = CommonSkins.GetFontWeightList;
        }

        private OptionWindow()
        {
            // Private constructor to instanciate local objects here 
            InitializeComponent();

            // Initialize all local event
            SetAllLocalEvents();

            //Loading the xml configuration to provide access to all elements
            CommonSkins.LoadSkinsFromXml();

            // Set all history combBox values
            SetHistoryListToControls();

            // Applying every properties values to the listBox from selected Skin
            PopulateSkinStylesToSkinListBox();

            // Combobox List for main Skin to select
            SkinNamesComboBox.ItemsSource = CommonSkins.GetParentSkinNames;

            // Selecting default values into controls
            if (CommonSkins.GetParentSkinLongNames.Count > 0)
            {
                SkinStylesLongNameListBox.SelectedItem = CommonSkins.GetParentSkinLongNames[0];
            }

            if (!string.IsNullOrEmpty(CommonSkins.SkinSelectedName))
            {
                SkinNamesComboBox.SelectedIndex = SkinNamesComboBox.Items.IndexOf(CommonSkins.SkinSelectedName);
            }
        }

        private void ComboBoxFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lCombo = (sender as ComboBox);
            var lComboTitle = (lCombo.Parent as Grid).Children[0] as Label;

            string lValueSelected = FontFamilyComboBox.ItemsComboBox.SelectedItem.ToString();

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
                    if (lComboTitle.Uid == "FontFamily")
                    {
                        CommonSkins.SelectedSkinObj.Skin.Where(p => p.LongName == lItemSelected).Select(p => p.FontFamily).FirstOrDefault().Value = lValueSelected;
                    }
                }

                // Applying the new Skin
                CommonSkins.UpdateResourcesWithSkins();
            }
        }

        /// <summary>
        /// Applying values from list to the listBox control
        /// </summary>
        private void PopulateSkinStylesToSkinListBox()
        {
            SkinStylesLongNameListBox.ItemsSource = CommonSkins.GetParentSkinLongNames;
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
            //Save Skin
            XmlParser.SerializeSkinsToXML(CommonSkins.SkinsObj);
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            //cancel change
            mOptionWindow.Hide();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            NewSkinName_Control.GetInstance().Show();
        }

        private void ComboBoxSkinList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CommonSkins.SkinSelectedName = (sender as ComboBox).SelectedValue.ToString();

            // Apply the default color for the first ListItem
            if (SkinStylesLongNameListBox != null && SkinStylesLongNameListBox.Items.Count > 0)
            {
                SkinStylesLongNameListBox_Selected(SkinStylesLongNameListBox, null);
            }

            // Applying the new Skin
            CommonSkins.UpdateResourcesWithSkins();
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

        private void SkinStylesLongNameListBox_Selected(object sender, RoutedEventArgs e)
        {
            if ((sender as ListBox).SelectedItem == null)
            {
                (sender as ListBox).SelectedItem = CommonSkins.GetParentSkinLongNames[0];
            }

            var lSelectedVal = (sender as ListBox).SelectedItem.ToString();

            var lStyle = (SkinStylesCls)CommonSkins.SelectedSkinObj.Skin.Where(p => p.LongName == lSelectedVal).Select(pStyle => pStyle).FirstOrDefault();

            UpdatePropertyValuesAndTitles(lStyle?.Background?.Value, BackgroundColorPicker, lStyle?.Background?.Name);
            UpdatePropertyValuesAndTitles(lStyle?.Foreground?.Value, ForegroundColorPicker, lStyle?.Foreground?.Name);
            UpdatePropertyValuesAndTitles(lStyle?.Borderbrush?.Value, BorderbrushColorPicker, lStyle?.Borderbrush?.Name);
            UpdatePropertyValuesAndTitles(lStyle?.FontFamily?.Value, FontFamilyComboBox, lStyle?.FontFamily?.Name);
            UpdatePropertyValuesAndTitles(((SizeOfFont)Convert.ToInt32(lStyle?.FontSize?.Value)).ToString(), FontSizeComboBox, lStyle?.FontSize?.Name);
            UpdatePropertyValuesAndTitles(GlobalUsage.ConvertStringToFontWeight(lStyle?.FontWeight?.Value).ToString(), FontWeightComboBox, lStyle?.FontWeight?.Name);
        }
    }
}
