using System.Windows;
using System.Windows.Input;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for NewSkin_Control.xaml
    /// </summary>
    public partial class NewSkin_Control : Window
    {
        // Init singleton
        private static readonly NewSkin_Control mNewSkinControl = new NewSkin_Control();

        /// <summary>
        /// Instance Singleton
        /// </summary>
        /// <returns></returns>
        public static NewSkin_Control GetInstance()
        {
            return mNewSkinControl;
        }

        private NewSkin_Control()
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
            //Save theme
            if (!string.IsNullOrEmpty(NewSkin_TextBox.Text))
            {
                XmlParser.SaveTheme(NewSkin_TextBox.Text);
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
