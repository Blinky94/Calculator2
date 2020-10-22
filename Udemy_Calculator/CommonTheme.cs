using System;
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

        private static string mThemeSelectedName;
        /// <summary>
        /// Return the value from the "ThemeSelected" in the xml file
        /// Set the value
        /// </summary>
        public static string ThemeSelectedName
        {
            get => mThemeSelectedName;

            set => mThemeSelectedName = value;
        }

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
            ListSelectedTheme = !string.IsNullOrEmpty(pSelectedTheme) ? ListOfAllThemes.Where(p => pSelectedTheme == p.ThemeName).ToList() : ListOfAllThemes.Where(p => p.ThemeSelected == p.ThemeName).ToList();

            // Set all themes properties with the selected theme
            SetThemeProperties();
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
            {
                return;
            }

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
                            if (lPropertyNode.Name.Contains("Color1"))
                            {
                                lTheme.Color1 = (SolidColorBrush)new BrushConverter().ConvertFromString(lPropertyNode.InnerText);
                                lTheme.Color1Attribute = lPropertyNode.Attributes["name"].Value;
                            }
                            else if (lPropertyNode.Name.Contains("Color2"))
                            {
                                lTheme.Color2 = (SolidColorBrush)new BrushConverter().ConvertFromString(lPropertyNode.InnerText);
                                lTheme.Color2Attribute = lPropertyNode.Attributes["name"].Value;
                            }
                            else if (lPropertyNode.Name.Contains("Color3"))
                            {
                                lTheme.Color3 = (SolidColorBrush)new BrushConverter().ConvertFromString(lPropertyNode.InnerText);
                                lTheme.Color3Attribute = lPropertyNode.Attributes["name"].Value;
                            }
                            else if (lPropertyNode.Name.Contains("Color4"))
                            {
                                lTheme.Color4 = (SolidColorBrush)new BrushConverter().ConvertFromString(lPropertyNode.InnerText);
                                lTheme.Color4Attribute = lPropertyNode.Attributes["name"].Value;
                            }
                            else if (lPropertyNode.Name.Contains("FontFamily1"))
                            {
                                lTheme.FontFamily1 = new FontFamily(lPropertyNode.InnerText);
                                lTheme.FontFamilyAttribute1 = lPropertyNode.Attributes["name"].Value;
                            }
                            else if (lPropertyNode.Name.Contains("FontFamily2"))
                            {
                                lTheme.FontFamily2 = new FontFamily(lPropertyNode.InnerText);
                                lTheme.FontFamilyAttribute2 = lPropertyNode.Attributes["name"].Value;
                            }
                            else if (lPropertyNode.Name.Contains("FontFamily3"))
                            {
                                lTheme.FontFamily3 = new FontFamily(lPropertyNode.InnerText);
                                lTheme.FontFamilyAttribute3 = lPropertyNode.Attributes["name"].Value;
                            }
                            else if (lPropertyNode.Name.Contains("FontSize1"))
                            {
                                lTheme.FontSize1 = int.Parse(lPropertyNode.InnerText);
                                lTheme.FontSizeAttribute1 = lPropertyNode.Attributes["name"].Value;
                            }
                            else if (lPropertyNode.Name.Contains("FontSize2"))
                            {
                                lTheme.FontSize2 = int.Parse(lPropertyNode.InnerText);
                                lTheme.FontSizeAttribute2 = lPropertyNode.Attributes["name"].Value;
                            }
                            else if (lPropertyNode.Name.Contains("FontSize3"))
                            {
                                lTheme.FontSize3 = int.Parse(lPropertyNode.InnerText);
                                lTheme.FontSizeAttribute3 = lPropertyNode.Attributes["name"].Value;
                            }
                            else if (lPropertyNode.Name.Contains("FontWeight1"))
                            {
                                lTheme.FontWeight1 = GlobalUsage.ConvertStringToFontWeight(lPropertyNode.InnerText);
                                lTheme.FontWeightAttribute1 = lPropertyNode.Attributes["name"].Value;
                            }
                            else if (lPropertyNode.Name.Contains("FontWeight2"))
                            {
                                lTheme.FontWeight2 = GlobalUsage.ConvertStringToFontWeight(lPropertyNode.InnerText);
                                lTheme.FontWeightAttribute2 = lPropertyNode.Attributes["name"].Value;
                            }
                            else if (lPropertyNode.Name.Contains("FontWeight3"))
                            {
                                lTheme.FontWeight3 = GlobalUsage.ConvertStringToFontWeight(lPropertyNode.InnerText);
                                lTheme.FontWeightAttribute3 = lPropertyNode.Attributes["name"].Value;
                            }
                        }

                        lTheme.ThemeSelected = lThemeSelected;
                        lTheme.ThemeName = lNode.GetAttribute("name");
                        lTheme.SubThemeName = lSubNode.Name;
                        lTheme.SubThemeAttribute = lSubNode.Attributes["name"].Value;

                        ListOfAllThemes.Add(lTheme);
                    }
                }
            }
        }

        /// <summary>
        /// Set all the themes properties values with the selected one
        /// </summary>
        private static void SetThemeProperties()
        {
            foreach (Theme lTheme in CommonTheme.ListSelectedTheme)
            {
                string lPorpertyName;

                if (lTheme.Color1 != null)
                {
                    lPorpertyName = string.Concat(nameof(lTheme.Color1), lTheme.SubThemeName);
                    Application.Current.Resources[lPorpertyName] = lTheme.Color1;
                }
                if (lTheme.Color2 != null)
                {
                    lPorpertyName = string.Concat(nameof(lTheme.Color2), lTheme.SubThemeName);
                    Application.Current.Resources[lPorpertyName] = lTheme.Color2;
                }
                if (lTheme.Color3 != null)
                {
                    lPorpertyName = string.Concat(nameof(lTheme.Color3), lTheme.SubThemeName);
                    Application.Current.Resources[lPorpertyName] = lTheme.Color3;
                }
                if (lTheme.Color4 != null)
                {
                    lPorpertyName = string.Concat(nameof(lTheme.Color4), lTheme.SubThemeName);
                    Application.Current.Resources[lPorpertyName] = lTheme.Color4;
                }
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

        #endregion

    }
}
