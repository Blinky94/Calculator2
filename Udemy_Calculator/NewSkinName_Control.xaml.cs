using System.Windows;
using System.Windows.Input;
using Udemy_Calculator.SerializedObjects;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for NewSkin_Control.xaml
    /// </summary>
    public partial class NewSkinName_Control : Window
    {
        // Init singleton
        private static readonly NewSkinName_Control mNewSkinControl = new NewSkinName_Control();

        /// <summary>
        /// Instance Singleton
        /// </summary>
        /// <returns></returns>
        public static NewSkinName_Control GetInstance()
        {
            IsSkinEdited = false;

            return mNewSkinControl;
        }

        public static bool IsSkinEdited { get; private set; }

        /// <summary>
        /// Singleton Edition
        /// </summary>
        /// <param name="pEditName"></param>
        /// <returns></returns>
        public NewSkinName_Control EditSkin()
        {
            NewSkin_TextBox.Text = CommonSkins.SkinSelectedName;
            IsSkinEdited = true;

            return mNewSkinControl;
        }

        private NewSkinName_Control()
        {
            InitializeComponent();

            Exit_GeneralButtonControl.OnGeneralButtonClicked += new RoutedEventHandler(Button_Exit);
            Save_GeneralButtonControl.OnGeneralButtonClicked += new RoutedEventHandler(Button_Save);
        }


        #region Moving the Calculator

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            this.Opacity = 0.75F;
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            this.Opacity = 1F;
            base.OnMouseLeftButtonUp(e);
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            string lNewSkinName = NewSkin_TextBox.Text;

            if (!string.IsNullOrEmpty(lNewSkinName))
            {
                // New Skin to save
                if (!IsSkinEdited)
                {
                    // Create new object in skin with its default values
                    CommonSkins.SkinsObj.AddNewBlankSkin(lNewSkinName);             
                }
                // Only the name to change to the existing skin
                else
                {
                    // Get the selected skin object
                    CommonSkins.SelectedSkinObj.Name = lNewSkinName;            
                }

                // Update the default value skin added
                CommonSkins.UpdateDefaultSkinSelected(lNewSkinName);

                //Save Skin
                XmlParser.SerializeSkinsToXML(CommonSkins.SkinsObj);

                HideAndResetControl();

                // Close and reopen OptionControl
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() != typeof(OptionWindow))
                    {
                        continue;
                    }

                    var lOptionWindow = (window as OptionWindow);

                    //Loading the xml configuration to provide access to all elements
                    CommonSkins.LoadSkinsFromXml();

                    // Combobox List for main Skin to select
                    lOptionWindow.SkinNamesComboBox.ItemsSource = CommonSkins.GetParentSkinNames;

                    // Set the new skin as default skin selected
                    lOptionWindow.SkinNamesComboBox.SelectedIndex = lOptionWindow.SkinNamesComboBox.Items.IndexOf(lNewSkinName);
                }
            }
        }

        /// <summary>
        /// Hidding and reseting text in the control
        /// </summary>
        private static void HideAndResetControl()
        {
            // Suppress new skin name for the next add
            mNewSkinControl.NewSkin_TextBox.Text = string.Empty;

            // Hide the new skin window name
            mNewSkinControl.Hide();
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            HideAndResetControl();
        }

        #endregion
    }
}
