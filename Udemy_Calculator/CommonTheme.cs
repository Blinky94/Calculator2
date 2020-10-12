using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml;
using System.Linq;
using System.Windows;
using System;

namespace Udemy_Calculator
{
    /// <summary>
    /// Class to set the common theme with colors and thickness
    /// </summary>
    public static class CommonTheme
    {
        #region public

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
        /// Get the theme selected object from the ListOfAllThemes object
        /// </summary>
        /// <param name="pSelectedTheme">Optional parameter</param>
        public static void SetSelectedThemeListObject(string pSelectedTheme = "")
        {
            if (string.IsNullOrEmpty(pSelectedTheme))
            {
                ListSelectedTheme = ListOfAllThemes.Where(p => p.ThemeSelected == p.ThemeRootName).ToList();
            }
            else
            {
                ListSelectedTheme = ListOfAllThemes.Where(p => pSelectedTheme == p.ThemeRootName).ToList();
            }

            UpdateThemeToAllWindow();
        }

        #endregion

        #region private

        /// <summary>
        /// Getting all the xml file configuration objects list
        /// </summary>
        private static void GetAllThemesListObject()
        {
            var lNodeList = LoadXmlConfiguration.SelectNodes("Themes");

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
            SetThemesProperties("BaseButtons", "BaseButtons");
            SetThemesProperties("ScientificButtons", "ScientificButtons");
            SetThemesProperties("SndeButton", "SndeButton");
            SetThemesProperties("OperatorsButtons", "OperatorsButtons");
            SetThemesProperties("NumericalsButtons", "NumericalsButtons");
            SetThemesProperties("MemoryButtons", "MemoryButtons");
            SetThemesProperties("Trigonometry", "TrigonometryButtons");
            SetThemesProperties("Trigonometry", "MainThemeButtonStyle");
            SetThemesProperties("MainTheme", "MainLabel");
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

            // Copying all old values to the new one
            foreach (Setter lSetter in lCurrentStyle.Setters)
            {
                if (lSetter.Property.Name != "Background" && lSetter.Property.Name != "Foreground" && lSetter.Property.Name != "BorderBrush")
                {
                    lNewStyle.Setters.Add(lSetter);
                }
            }

            var lThemeProperty = CommonTheme.ListSelectedTheme.Where(p => p.ParentName == pThemeName);

            lNewStyle.Setters.Add(new Setter(Control.BackgroundProperty, (Brush)lThemeProperty.Select(p => p.Background).First()));
            lNewStyle.Setters.Add(new Setter(Control.ForegroundProperty, (Brush)lThemeProperty.Select(p => p.Foreground).First()));
            lNewStyle.Setters.Add(new Setter(Control.BorderBrushProperty, (Brush)lThemeProperty.Select(p => p.BorderBrush).First()));

            Application.Current.Resources[pThemeStyle] = lNewStyle;
        }

        /// <summary>
        /// Getting all the name of parent nodes themes (Ice, Default, Fire...)
        /// </summary>
        /// <returns></returns>
        private static List<string> AllParentNodeNameFromXmlFile()
        {
            var lNodeList = LoadXmlConfiguration.SelectNodes("Themes/Theme");

            List<string> lList = new List<string>();
            foreach (XmlNode lNode in lNodeList)
            {
                lList.Add(lNode.Attributes["name"].Value);
            }

            return lList;
        }

        /// <summary>
        /// Getting all the long name for each theme configuration
        /// </summary>
        /// <returns></returns>
        private static List<string> AllParentNodeLongName()
        {
            var lNodes = LoadXmlConfiguration.SelectSingleNode($"Themes/Theme[@name='{ThemeSelectedName}']");

            List<string> lList = new List<string>();
            foreach (XmlNode lParentNode in lNodes.ChildNodes)
            {
                lList.Add(lParentNode.Attributes["name"].Value);
            }

            return lList;
        }

        /// <summary>
        /// Getting the theme name selected
        /// </summary>
        /// <returns></returns>
        private static string ThemeSelected()
        {
            return LoadXmlConfiguration.SelectSingleNode("Themes/ThemeSelected/@name").Value;
        }

        #endregion

        //    /// <summary>
        //    /// Set the theme with pName as the parameter name, and pValue as the value of this parameter
        //    /// </summary>
        //    /// <param name="pName"></param>
        //    /// <param name="pValue"></param>
        //    private static void SetProperty(string pValue)
        //    {
        //        SolidColorBrush lSolidColorBrushValue = default;

        //        if (double.TryParse(pValue.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double lDoubleValue) == false)
        //        {
        //            if (!string.IsNullOrEmpty(pValue))
        //            {
        //                lSolidColorBrushValue = (SolidColorBrush)new BrushConverter().ConvertFromString(pValue);
        //            }
        //        }

        //        //switch (pName)
        //        //{
        //        //    case "MainCalculatorBackground":
        //        //        MainCalculatorBackground = lSolidColorBrushValue; break;
        //        //    case "MainCalculatorForeground":
        //        //        MainCalculatorForeground = lSolidColorBrushValue; break;
        //        //    case "MainCalculatorBorderBrush":
        //        //        MainCalculatorBorderBrush = lSolidColorBrushValue; break;             
        //        //    case "BackgroundBaseButtons":
        //        //        BackgroundBaseButtons = lSolidColorBrushValue; break;
        //        //    case "ForegroundBaseButtons":
        //        //        ForegroundBaseButtons = lSolidColorBrushValue; break;
        //        //    case "BorderBrushBaseButtons":
        //        //        BorderBrushBaseButtons = lSolidColorBrushValue; break;              
        //        //    case "Background2ndeButton":
        //        //        Background2ndeButton = lSolidColorBrushValue; break;
        //        //    case "Foreground2ndeButton":
        //        //        Foreground2ndeButton = lSolidColorBrushValue; break;
        //        //    case "BorderBrush2ndeButton":
        //        //        BorderBrush2ndeButton = lSolidColorBrushValue; break;
        //        //    case "BackgroundScientificButtons":
        //        //        BackgroundScientificButtons = lSolidColorBrushValue; break;
        //        //    case "ForegroundScientificButtons":
        //        //        ForegroundScientificButtons = lSolidColorBrushValue; break;
        //        //    case "BorderBrushScientificButtons":
        //        //        BorderBrushScientificButtons = lSolidColorBrushValue; break;
        //        //    case "BackgroundOperatorsButtons":
        //        //        BackgroundOperatorsButtons = lSolidColorBrushValue; break;
        //        //    case "ForegroundOperatorsButtons":
        //        //        ForegroundOperatorsButtons = lSolidColorBrushValue; break;
        //        //    case "BorderBrushOperatorsButtons":
        //        //        BorderBrushOperatorsButtons = lSolidColorBrushValue; break;
        //        //    case "BackgroundNumericalsButtons":
        //        //        BackgroundNumericalsButtons = lSolidColorBrushValue; break;
        //        //    case "ForegroundNumericalsButtons":
        //        //        ForegroundNumericalsButtons = lSolidColorBrushValue; break;
        //        //    case "BorderBrushNumericalsButtons":
        //        //        BorderBrushNumericalsButtons = lSolidColorBrushValue; break;
        //        //    case "BackgroundMemoryButtons":
        //        //        BackgroundMemoryButtons = lSolidColorBrushValue; break;
        //        //    case "ForegroundMemoryButtons":
        //        //        ForegroundMemoryButtons = lSolidColorBrushValue; break;
        //        //    case "BorderBrushMemoryButtons":
        //        //        BorderBrushMemoryButtons = lSolidColorBrushValue; break;
        //        //    case "BackgroundTrigonometryButtons":
        //        //        BackgroundTrigonometryButtons = lSolidColorBrushValue; break;
        //        //    case "ForegroundTrigonometryButtons":
        //        //        ForegroundTrigonometryButtons = lSolidColorBrushValue; break;
        //        //    case "BorderBrushTrigonometryButtons":
        //        //        BorderBrushTrigonometryButtons = lSolidColorBrushValue; break;
        //        //}
        //    }
    }
}
