using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void UpdateUIDisplayHandler(string pContent);
        public event UpdateUIDisplayHandler UIDisplayValueEvent;

        public MainWindow()
        {
            InitializeComponent();

            UIMenuSide.UIMenuSelected.Content = CalculatorMode.Standard.ToString();

            UIDisplayValueEvent += new UpdateUIDisplayHandler(ModifyUIDisplay);
            UICalculator.CalculusDisplayDelegate = UIDisplayValueEvent;
            
            SetMenuThemeItems();
            UIMenuSide.GetThemes();
        }

        /// <summary>
        /// Set the menu theme items
        /// </summary>
        private void SetMenuThemeItems()
        {
            var lXMLDataProvider = (XmlDataProvider)FindResource("ThemeList");
            var lXmlThemes = lXMLDataProvider.Document;

            UIMenuSide.GetThemesList = SetListOfParameters(lXmlThemes);

            if (!UIMenuSide.SetMenuItems())
            {
                Debug.WriteLine("MenuItem is not loading correctly !");
            }
        }

        private void ChangeColor()
        {
            //if (pHasIcon)
            //{
            //    pName = pName.Replace(".", "");
            //    lMenuItem.Icon = new Image { Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/Images/" + $"{pName}_icon{pReverseStr}.png", UriKind.Absolute)) };
            //}

            // Reverse text color ?
            //if (pReverseStr == "")
            //{
            //    lMenuItem.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
            //    UIMenuSelected.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
            //}
            //else
            //{
            //    lMenuItem.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            //    UIMenuSelected.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            //}
        }

        private List<ThemeObj> SetListOfParameters(XmlDocument pDoc)
        {
            List<ThemeObj> lList = new List<ThemeObj>();
            string lThemeSelected = string.Empty;

            foreach (XmlElement lElement in pDoc.DocumentElement.ChildNodes)
            {
                if (lElement.Name == "ThemeSelected")
                {
                    lThemeSelected = lElement.GetAttribute("name");
                }

                foreach (XmlElement lNode in lElement)
                {
                    foreach (XmlElement lSubNode in lNode)
                    {      
                        lList.Add(new ThemeObj()
                        {
                            ThemeSelected = lThemeSelected,
                            ParentThemeName = lElement.GetAttribute("name"),
                            ChildThemeName = lNode.Name,
                            ChildThemeText = lNode.GetAttribute("name"),
                            ParameterName = lSubNode.Name,
                            ParameterStringValue = lSubNode.InnerText
                        }) ;
                    }                 
                }
            }

            return lList;
        }

        public void ModifyUIDisplay(string pContent)
        {
            UIDisplay.UIDisplayCalculus.Text = pContent;
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

        #endregion
    }
}
