using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            //Loading the xml configuration to provide access to all elements
            CommonTheme.LoadFromXmlProvider(this);

            // Applying every properties values to the listBox from selected theme
            PopulateListPropertiesToListView();

            // Combobox List for main theme to select
            ThemeListComboBox.ItemsSource = CommonTheme.GetParentThemeNames;

            // Selecting default values into controls
            if (CommonTheme.GetParentThemeLongNames.Count > 0)
            {
                ItemsListBox.SelectedItem = CommonTheme.GetParentThemeLongNames[0];
            }

            CommonTheme.ThemeSelectedName = CommonTheme.LoadXmlConfiguration?.SelectSingleNode("Themes/ThemeSelected/@name").Value;

            if (!string.IsNullOrEmpty(CommonTheme.ThemeSelectedName))
            {
                ThemeListComboBox.SelectedIndex = ThemeListComboBox.Items.IndexOf(CommonTheme.ThemeSelectedName);
            }
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

            PopulateListPropertiesToListView();
            if (ItemsListBox != null && ItemsListBox.Items.Count > 0)
                // Apply the default color for the first ListItem
                ItemsListBox_Selected(ItemsListBox, null);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

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
                if (lTheme.Color1 != null)
                {
                    // Activating parameter
                    Color1Picker.IsEnabled = true;
                    // Affecting the color
                    Color1Picker.UIColorPicker.SelectedColor = (lTheme.Color1).Color;
                }
                else
                {
                    // Desactivating the parameter
                    Color1Picker.IsEnabled = false;
                    Color1Picker.UIColorPicker.SelectedColor = null;
                }

                if (lTheme.Color2 != null)
                {
                    // Activating parameter
                    Color2Picker.IsEnabled = true;
                    // Affecting the color
                    Color2Picker.UIColorPicker.SelectedColor = (lTheme.Color2).Color;
                }
                else
                {
                    // Desactivating the parameter
                    Color2Picker.IsEnabled = false;
                    Color2Picker.UIColorPicker.SelectedColor = null;
                }

                if (lTheme.Color3 != null)
                {
                    // Activating parameter
                    Color3Picker.IsEnabled = true;
                    // Affecting the color
                    Color3Picker.UIColorPicker.SelectedColor = (lTheme.Color3).Color;
                }
                else
                {
                    // Desactivating the parameter
                    Color3Picker.IsEnabled = false;
                    Color3Picker.UIColorPicker.SelectedColor = null;
                }

                if (lTheme.Color4 != null)
                {
                    // Activating parameter
                    Color4Picker.IsEnabled = true;
                    // Affecting the color
                    Color4Picker.UIColorPicker.SelectedColor = (lTheme.Color4).Color;
                }
                else
                {
                    // Desactivating the parameter
                    Color4Picker.IsEnabled = false;
                    Color4Picker.UIColorPicker.SelectedColor = null;
                }
            }
        }
    }
}
