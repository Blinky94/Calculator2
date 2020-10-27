using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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
        /// Getting all the fonts availables in the solution
        /// </summary>
        public static List<string> GetFontList => GetAllAvailablesFonts();

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
        /// Get all font weight available
        /// </summary>
        public static List<string> GetFontWeightList => GetAllFontWeightsAvailables();

        /// <summary>
        /// Load all properties from xml data provider from current control
        /// </summary>
        /// <param name="pControl"></param>
        /// <returns></returns>
        public static void LoadFromXmlProvider(Control pControl)
        {
            XmlDataProvider lXml = (XmlDataProvider)pControl.FindResource("ThemesXml");
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
        /// Get all font weight string available from the FontWeights class
        /// </summary>
        /// <returns></returns>
        private static List<string> GetAllFontWeightsAvailables()
        {
            var lList = new List<string>();

            var type = typeof(FontWeights);
            foreach (var p in type.GetProperties().Where(s => s.PropertyType == typeof(FontWeight)))
            {
                lList.Add(p.GetValue(null, null).ToString());
            }

            return lList.Distinct().ToList();
        }

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
        /// Set the style of history app.xaml
        /// </summary>
        /// <param name="pFontFamily"></param>
        /// <param name="pFontSize"></param>
        /// <param name="pFontWeight"></param>
        /// <returns></returns>
        private static Style SetStyleHistory(string pFontFamily, double pFontSize, string pFontWeight, TextAlignment pTextAlignment, SolidColorBrush pColor)
        {
            Style lStyle = new Style();

            lStyle.Setters.Add(new Setter(Paragraph.FontFamilyProperty, Application.Current.Resources[pFontFamily]));
            lStyle.Setters.Add(new Setter(Paragraph.FontSizeProperty, pFontSize));
            lStyle.Setters.Add(new Setter(Paragraph.FontWeightProperty, GlobalUsage.ConvertStringToFontWeight(pFontWeight)));
            lStyle.Setters.Add(new Setter(Paragraph.ForegroundProperty, pColor));
            lStyle.Setters.Add(new Setter(Paragraph.TextAlignmentProperty, pTextAlignment));
            
            return lStyle;
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
                if (CommonTheme.GetFontList.Contains(lTheme.FontFamily1?.ToString()))
                {
                    // Modify the formula style in the app resource dictionnary styles
                    Application.Current.Resources["HistoryFormulaStyle"] = SetStyleHistory(lTheme.FontFamily1.ToString(), (double)lTheme.FontSize1, lTheme.FontWeight1.ToString(), TextAlignment.Left, lTheme.Color2);

                    // Filling every kind of block history depending of its type
                    foreach (var eleme in ((MainWindow)Application.Current.MainWindow).UIHistory.UIHistoryFlowDocument.Blocks)
                    {
                        if(eleme.Name == "UIHistoryFormulaParagraph")
                        {
                            eleme.Style = Application.Current.FindResource("HistoryFormulaStyle") as Style;
                        }
                        else if (eleme.Name == "UIHistoryChunkParagraph")
                        {
                            eleme.Style = Application.Current.FindResource("HistoryChunkStyle") as Style;
                        }
                        else if (eleme.Name == "UIHistoryResultParagraph")
                        {
                            eleme.Style = Application.Current.FindResource("HistoryResultStyle") as Style;
                        }
                    }
                }
                if (CommonTheme.GetFontList.Contains(lTheme.FontFamily2?.ToString()))
                {
                    // Modify the chunk style in the app resource dictionnary styles
                    Application.Current.Resources["HistoryChunkStyle"] = SetStyleHistory(lTheme.FontFamily2.ToString(), (double)lTheme.FontSize2, lTheme.FontWeight2.ToString(), TextAlignment.Center, lTheme.Color3);

                    // Filling every kind of block history depending of its type
                    foreach (var eleme in ((MainWindow)Application.Current.MainWindow).UIHistory.UIHistoryFlowDocument.Blocks)
                    {
                        if (eleme.Name == "UIHistoryFormulaParagraph")
                        {
                            eleme.Style = Application.Current.FindResource("HistoryFormulaStyle") as Style;
                        }
                        else if (eleme.Name == "UIHistoryChunkParagraph")
                        {
                            eleme.Style = Application.Current.FindResource("HistoryChunkStyle") as Style;
                        }
                        else if (eleme.Name == "UIHistoryResultParagraph")
                        {
                            eleme.Style = Application.Current.FindResource("HistoryResultStyle") as Style;
                        }
                    }
                }
                if (CommonTheme.GetFontList.Contains(lTheme.FontFamily3?.ToString()))
                {
                    // Modify the result style in the app resource dictionnary styles
                    Application.Current.Resources["HistoryResultStyle"] = SetStyleHistory(lTheme.FontFamily3.ToString(), (double)lTheme.FontSize3, lTheme.FontWeight3.ToString(), TextAlignment.Right, lTheme.Color4);

                    // Filling every kind of block history depending of its type
                    foreach (var eleme in ((MainWindow)Application.Current.MainWindow).UIHistory.UIHistoryFlowDocument.Blocks)
                    {
                        if (eleme.Name == "UIHistoryFormulaParagraph")
                        {
                            eleme.Style = Application.Current.FindResource("HistoryFormulaStyle") as Style;
                        }
                        else if (eleme.Name == "UIHistoryChunkParagraph")
                        {
                            eleme.Style = Application.Current.FindResource("HistoryChunkStyle") as Style;
                        }
                        else if (eleme.Name == "UIHistoryResultParagraph")
                        {
                            eleme.Style = Application.Current.FindResource("HistoryResultStyle") as Style;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Getting all the fonts available in the App xaml
        /// </summary>
        /// <returns></returns>
        private static List<string> GetAllAvailablesFonts()
        {
            var lListOfFonts = new List<string>();

            foreach (System.Collections.DictionaryEntry lResourceDict in Application.Current.Resources)
            {
                if (lResourceDict.Value is FontFamily)
                {
                    lListOfFonts.Add(lResourceDict.Key.ToString());
                }
            }

            return lListOfFonts;
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
