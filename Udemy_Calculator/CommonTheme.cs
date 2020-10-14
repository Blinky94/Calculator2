using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml;

namespace Udemy_Calculator
{
    /// <summary>
    /// Class to set the common theme with colors and thickness
    /// </summary>
    public static class CommonTheme
    {
        #region public

        public static SolidColorBrush MainCalculatorBackground { get; set; }
        public static SolidColorBrush MainCalculatorForeground { get; set; }
        public static SolidColorBrush MainCalculatorBorderBrush { get; set; }
        public static SolidColorBrush Background2ndeButton { get; set; }
        public static SolidColorBrush Foreground2ndeButton { get; set; }
        public static SolidColorBrush BorderBrush2ndeButton { get; set; }
        public static SolidColorBrush BackgroundBaseButtons { get; set; }
        public static SolidColorBrush ForegroundBaseButtons { get; set; }
        public static SolidColorBrush BorderBrushBaseButtons { get; set; }
        public static SolidColorBrush BackgroundScientificButtons { get; set; }
        public static SolidColorBrush ForegroundScientificButtons { get; set; }
        public static SolidColorBrush BorderBrushScientificButtons { get; set; }
        public static SolidColorBrush BackgroundOperatorsButtons { get; set; }
        public static SolidColorBrush ForegroundOperatorsButtons { get; set; }
        public static SolidColorBrush BorderBrushOperatorsButtons { get; set; }
        public static SolidColorBrush BackgroundNumericalsButtons { get; set; }
        public static SolidColorBrush ForegroundNumericalsButtons { get; set; }
        public static SolidColorBrush BorderBrushNumericalsButtons { get; set; }
        public static SolidColorBrush BackgroundMemoryButtons { get; set; }
        public static SolidColorBrush ForegroundMemoryButtons { get; set; }
        public static SolidColorBrush BorderBrushMemoryButtons { get; set; }
        public static SolidColorBrush BackgroundTrigonometryButtons { get; set; }
        public static SolidColorBrush ForegroundTrigonometryButtons { get; set; }
        public static SolidColorBrush BorderBrushTrigonometryButtons { get; set; }

        /// <summary>
        /// Return the value from the "ThemeSelected" in the xml file
        /// </summary>
        public static string ThemeSelectedName => ThemeSelected();

        /// <summary>
        /// Getting all theme names
        /// </summary>
        public static List<string> GetParentThemeNames => AllParentNodeNameFromXmlFile();

        /// <summary>
        /// Getting all subNodes long name from theme
        /// </summary>
        public static List<string> GetParentThemeLongNames => AllParentNodeLongName();

        /// <summary>
        /// access to the theme xml configuration
        /// </summary>
        public static XmlDocument LoadXmlConfiguration { get; private set; }

        /// <summary>
        /// Get the selected list theme with all elements
        /// </summary>
        public static List<Theme> ListSelectedTheme { get; set; }

        /// <summary>
        /// Get all the themes from the Xml file configuration
        /// </summary>
        public static List<Theme> ListOfAllThemes { get; set; }

        /// <summary>
        /// Load all properties from xml data provider from current control
        /// </summary>
        /// <param name="pControl"></param>
        /// <returns></returns>
        public static void LoadFromXmlProvider(Control pControl)
        {
            XmlDataProvider lXml = (XmlDataProvider)pControl.FindResource("colorThemesXml");
            LoadXmlConfiguration = lXml.Document;

            ListSelectedTheme = new List<Theme>();
            ListOfAllThemes = new List<Theme>();

            GetAllThemesListObject();
            SetSelectedThemeListObject();
        }

        /// <summary>
        /// Set the theme selected and apply to all the windows opened
        /// </summary>
        /// <param name="pSelectedTheme">Optional parameter</param>
        public static void SetSelectedThemeListObject(string pSelectedTheme = "")
        {
            ListSelectedTheme = !string.IsNullOrEmpty(pSelectedTheme) ? ListOfAllThemes.Where(p => pSelectedTheme == p.ThemeRootName).ToList() : ListOfAllThemes.Where(p => p.ThemeSelected == p.ThemeRootName).ToList();

            UpdateThemeToAllWindow();
        }

        #endregion

        #region private

        /// <summary>
        /// Getting all the xml file configuration objects list
        /// </summary>
        private static void GetAllThemesListObject()
        {
            var lNodeList = LoadXmlConfiguration?.SelectNodes("Themes");

            if (lNodeList == null)
                return;
            string lThemeSelected;

            foreach (XmlElement lElement in lNodeList)
            {
                lThemeSelected = lElement.FirstChild.Attributes["name"].Value;

                foreach (XmlElement lNode in lElement.ChildNodes)
                {
                    foreach (XmlElement lSubNode in lNode)
                    {
                        var lTheme = new Theme();

                        foreach (XmlNode lPropertyNode in lSubNode.ChildNodes)
                        {
                            if (lPropertyNode.Name.Contains("Background"))
                            {
                                lTheme.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(lPropertyNode.InnerText);
                            }
                            else if (lPropertyNode.Name.Contains("Foreground"))
                            {
                                lTheme.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(lPropertyNode.InnerText);
                            }
                            else if (lPropertyNode.Name.Contains("BorderBrush"))
                            {
                                lTheme.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(lPropertyNode.InnerText);
                            }
                        }

                        lTheme.ThemeSelected = lThemeSelected;
                        lTheme.ThemeRootName = lNode.GetAttribute("name");
                        lTheme.ParentName = lSubNode.Name;
                        lTheme.ParentLongName = lSubNode.Attributes["name"].Value;

                        ListOfAllThemes.Add(lTheme);
                    }
                }
            }
        }

        /// <summary>
        /// Updating the current theme to all windows
        /// </summary>
        private static void UpdateThemeToAllWindow()
        {
            SetThemesProperties("MainTheme", "MainWindowStyle");
            SetThemesProperties("BaseButtons", "BaseButtonsStyle");
            SetThemesProperties("ScientificButtons", "ScientificButtonsStyle");
            SetThemesProperties("SndeButton", "SndeButtonStyle");
            SetThemesProperties("OperatorsButtons", "OperatorsButtonsStyle");
            SetThemesProperties("NumericalsButtons", "NumericalsButtonsStyle");
            SetThemesProperties("MemoryButtons", "MemoryButtonsStyle");
            SetThemesProperties("TrigonometryButtons", "TrigonometryButtonsStyle");
            SetThemesProperties("BaseButtons", "MainThemeButtonStyle");
            SetThemesProperties("MainTheme", "MainLabelStyle");
            SetThemesProperties("SndeButton", "MenuStyle");
        }

        /// <summary>
        /// Define the new properties values for a specific theme and style
        /// from the old Style property defined
        /// </summary>
        /// <param name="pThemeName">Theme name in the xml file config</param>
        /// <param name="pThemeStyle">Theme style name declared in xaml</param>
        private static void SetThemesProperties(string pThemeName, string pThemeStyle)
        {
            var lCurrentStyle = (Style)Application.Current.FindResource(pThemeStyle);
            var lNewStyle = new Style(lCurrentStyle.TargetType, lCurrentStyle.BasedOn);

            lNewStyle.BasedOn = lCurrentStyle.BasedOn;

            // Copying all old values to the new one
            foreach (Setter lSetter in lCurrentStyle.Setters)
            {
                if (lSetter.Property.Name != "Background" && lSetter.Property.Name != "Foreground" && lSetter.Property.Name != "BorderBrush")
                {
                    lNewStyle.Setters.Add(lSetter);
                }
            }

            foreach (var lTrigger in lCurrentStyle.Triggers)
            {
                lNewStyle.Triggers.Add(lTrigger);
            }

            if (CommonTheme.ListSelectedTheme.Count > 0)
            {
                var lThemeProperty = CommonTheme.ListSelectedTheme.Where(p => p.ParentName == pThemeName);

                lNewStyle.Setters.Add(new Setter(Control.BackgroundProperty, (Brush)lThemeProperty.Select(p => p.Background).First()));
                lNewStyle.Setters.Add(new Setter(Control.ForegroundProperty, (Brush)lThemeProperty.Select(p => p.Foreground).First()));
                lNewStyle.Setters.Add(new Setter(Control.BorderBrushProperty, (Brush)lThemeProperty.Select(p => p.BorderBrush).First()));

                Application.Current.Resources[pThemeStyle] = lNewStyle;
            }
        }

        /// <summary>
        /// Getting all the name of parent nodes themes (Ice, Default, Fire...)
        /// </summary>
        /// <returns></returns>
        private static List<string> AllParentNodeNameFromXmlFile()
        {
            var lNodeList = LoadXmlConfiguration?.SelectNodes("Themes/Theme");

            List<string> lList = new List<string>();

            if (lNodeList != null)
            {
                foreach (XmlNode lNode in lNodeList)
                {
                    lList.Add(lNode.Attributes["name"].Value);
                }
            }


            return lList;
        }

        /// <summary>
        /// Getting all the long name for each theme configuration
        /// </summary>
        /// <returns></returns>
        private static List<string> AllParentNodeLongName()
        {
            var lNodes = LoadXmlConfiguration?.SelectSingleNode($"Themes/Theme[@name='{ThemeSelectedName}']");

            List<string> lList = new List<string>();

            if (lNodes != null && lNodes.ChildNodes != null)
            {
                foreach (XmlNode lParentNode in lNodes?.ChildNodes)
                {
                    lList.Add(lParentNode.Attributes["name"].Value);
                }
            }

            return lList;
        }

        /// <summary>
        /// Getting the theme name selected
        /// </summary>
        /// <returns></returns>
        private static string ThemeSelected()
        {
            return LoadXmlConfiguration?.SelectSingleNode("Themes/ThemeSelected/@name").Value;
        }

        #endregion

    }
}
