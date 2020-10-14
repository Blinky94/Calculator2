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

            //Loading the xml configuration to provide access to all elements
            CommonTheme.LoadFromXmlProvider(this);

            // Applying every properties values to the listBox from selected theme
            PopulateListPropertiesToSource();

            // Combobox List for main theme to select
            ThemeListComboBox.ItemsSource = CommonTheme.GetParentThemeNames;

            // Selecting default values into controls
            if (CommonTheme.GetParentThemeLongNames.Count > 0)
            {
                ItemsListBox.SelectedItem = CommonTheme.GetParentThemeLongNames[0];
            }

            if (!string.IsNullOrEmpty(CommonTheme.ThemeSelectedName))
            {
                ThemeListComboBox.SelectedIndex = ThemeListComboBox.Items.IndexOf(CommonTheme.ThemeSelectedName);
            }
        }

        /// <summary>
        /// Applying values from list to the listBox control
        /// </summary>
        private void PopulateListPropertiesToSource()
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
            //XmlParser.SaveTheme();
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

            // Updating the list of objects selected
            CommonTheme.SetSelectedThemeListObject(lSelectedVal);
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

        private void ItemsListBox_Selected(object sender, RoutedEventArgs e)
        {
            var lSelectedVal = (sender as ListBox).SelectedItem.ToString();

            foreach (var lTheme in CommonTheme.ListSelectedTheme.Where(p => p.ParentLongName == lSelectedVal))
            {
                BackgroundColorPicker.UIColorPicker.SelectedColor = (lTheme.Background).Color;
                ForegroundColorPicker.UIColorPicker.SelectedColor = (lTheme.Foreground).Color;
                BorderBrushColorPicker.UIColorPicker.SelectedColor = (lTheme.BorderBrush).Color;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
