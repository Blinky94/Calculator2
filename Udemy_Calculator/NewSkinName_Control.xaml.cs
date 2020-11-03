using System.Windows;
using System.Windows.Input;

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
            return mNewSkinControl;
        }

        private NewSkinName_Control()
        {
            InitializeComponent();

            Cancel_GeneralButtonControl.OnGeneralButtonClicked += new RoutedEventHandler(Button_Cancel);
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
                // Create new object in skin with its default values
                CommonSkins.SkinsObj.AddNewBlankSkin(lNewSkinName);

                //Save Skin
                XmlParser.SerializeSkinsToXML(CommonSkins.SkinsObj);
                mNewSkinControl.Close();
            }           
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            //cancel change
            mNewSkinControl.Close();
        }

        #endregion
    }
}
