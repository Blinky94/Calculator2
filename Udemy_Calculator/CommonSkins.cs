using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml;
using Udemy_Calculator.SerializedObjects;

namespace Udemy_Calculator
{
    /// <summary>
    /// Class to set the common Skin with colors and thickness
    /// </summary>
    public static class CommonSkins
    {
        #region public

        /// <summary>
        /// Return the value from the "SkinSelected" in the xml file
        /// Set the value
        /// </summary>
        public static string SkinSelectedName { get; set; }

        /// <summary>
        /// Getting all Skin names
        /// </summary>
        public static List<string> GetParentSkinNames
        {
            get
            {
                var lNodeList = XmlParser.SkinsXmlDoc?.SelectNodes("Skins/Skin");

                return lNodeList?.Cast<XmlNode>().ToList().Select(p => p.Attributes["name"].Value).ToList();
            }
        }

        /// <summary>
        /// Getting all the fonts availables in the solution
        /// </summary>
        public static List<string> GetFontList => GetAllAvailablesFonts();

        /// <summary>
        /// Update the selected skin to the Skins objects
        /// </summary>
        /// <param name="pSkinName"></param>
        public static void UpdateDefaultSkinSelected(string pSkinName)
        {
            SkinsObj.Selected = pSkinName;
        }

        /// <summary>
        /// Getting all subNodes long name from Skin
        /// </summary>
        public static List<string> GetParentSkinLongNames
        {
            get
            {
                var lNodes = XmlParser.SkinsXmlDoc?.SelectSingleNode($"Skins/Skin[@name='{SkinSelectedName}']");

                return lNodes?.ChildNodes?.Cast<XmlNode>().ToList().Select(p => p.Attributes["longName"].Value).ToList();
            }
        }

        /// <summary>
        /// Get the selected Skin with all its items
        /// </summary>
        public static SkinCls SelectedSkinObj
        {
            get
            {
                return (SkinCls)SkinsObj?.Skins.Where(sk => sk.Name == SkinSelectedName).Select(sk => sk).FirstOrDefault();
            }
        }

        /// <summary>
        /// Get all the skins from the Xml file configuration
        /// </summary>
        public static SkinsCls SkinsObj { get; set; }

        /// <summary>
        /// Get all font weight available
        /// </summary>
        public static List<string> GetFontWeightList => GetAllFontWeightsAvailables();

        /// <summary>
        /// Load all properties from xml data provider from current control
        /// </summary>
        /// <param name="pControl"></param>
        /// <returns></returns>
        public static void LoadSkinsFromXml()
        {
            string lSkinsXml = XmlParser.LoadParamsXmlSkins();

            if (lSkinsXml != null)
            {
                // Deserialize xml string to Skins object
                SkinSelectedName = XmlParser.SkinsXmlDoc.LastChild.Attributes["selected"].Value;
                SkinsObj = XmlParser.DeserializeSkinsFromXML(lSkinsXml);

                // Set skin style to all windows
                SetSkinStyles();
            }
        }

        /// <summary>
        /// Update all resources skins (pSkinName empty) or specific skin
        /// </summary>
        /// <param name="pSkinName">Optional, to precise the skin selected name</param>
        public static void UpdateResourcesWithSkins(string pSkinName = "")
        {
            // Set skin style to all windows
            SetSkinStyles(pSkinName);
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
        /// Set all paragraph styles for History panel control
        /// </summary>
        private static void SetHistoryParagraphStyles()
        {
            // Filling every kind of block history depending of its type
            foreach (var lBlock in ((MainWindow)Application.Current.MainWindow).UIHistory.UIHistoryFlowDocument.Blocks)
            {
                if (lBlock.Name == "UIHistoryFormulaParagraph")
                {
                    lBlock.Style = Application.Current.FindResource("HistoryFormulaStyle") as Style;
                }
                else if (lBlock.Name == "UIHistoryChunkParagraph")
                {
                    lBlock.Style = Application.Current.FindResource("HistoryChunkStyle") as Style;
                }
                else if (lBlock.Name == "UIHistoryResultParagraph")
                {
                    lBlock.Style = Application.Current.FindResource("HistoryResultStyle") as Style;
                }
            }
        }

        /// <summary>
        /// Set Skinproperties to resources
        /// </summary>
        /// <param name="pSkinStyles"></param>
        /// <param name="pSelectedSkinName"></param>
        private static void SetSkinStylesToResources(SkinStylesCls pSkinStyles, string pSelectedSkinName)
        {
            string lPropertyName;

            if (!string.IsNullOrEmpty(pSkinStyles.Background?.Value))
            {
                lPropertyName = string.Concat("Background", pSelectedSkinName);
                Application.Current.Resources[lPropertyName] = (SolidColorBrush)new BrushConverter().ConvertFromString(pSkinStyles.Background.Value);
            }
            if (!string.IsNullOrEmpty(pSkinStyles.Foreground?.Value))
            {
                lPropertyName = string.Concat("Foreground", pSelectedSkinName);
                Application.Current.Resources[lPropertyName] = (SolidColorBrush)new BrushConverter().ConvertFromString(pSkinStyles.Foreground.Value);
            }
            if (!string.IsNullOrEmpty(pSkinStyles.Borderbrush?.Value))
            {
                lPropertyName = string.Concat("Borderbrush", pSelectedSkinName);
                Application.Current.Resources[lPropertyName] = (SolidColorBrush)new BrushConverter().ConvertFromString(pSkinStyles.Borderbrush.Value);
            }
        }

        /// <summary>
        /// Set Style to sepcific history category (Formula, Chunk or Result)
        /// </summary>
        /// <param name="pStyles"></param>
        /// <param name="pHistoryStyleName"></param>
        private static void SetHistoryStylesToResourceDictionary(SkinStylesCls pStyles, string pHistoryStyleName, TextAlignment pTextAlignment)
        {
            if (!string.IsNullOrEmpty(pStyles.FontFamily.Value) || !string.IsNullOrEmpty(pStyles.FontSize.Value) || !string.IsNullOrEmpty(pStyles.FontWeight.Value) || !string.IsNullOrEmpty(pStyles.Foreground.Value))
            {
                // Modify the style in the app resource dictionnary styles
                Application.Current.Resources[pHistoryStyleName] = SetStyleHistory(pStyles.FontFamily.Value, double.Parse(pStyles.FontSize.Value), pStyles.FontWeight.Value, pTextAlignment, (SolidColorBrush)new BrushConverter().ConvertFromString(pStyles.Foreground.Value.ToString()));

                SetHistoryParagraphStyles();
            }
        }

        /// <summary>
        /// Set specific skin resource in App.xaml with selected skin
        /// </summary>
        /// <param name="pSelectedSkinName"></param>
        private static void SetResourceDictionary(string pSelectedSkinName)
        {
            SelectedSkinObj.Skin.Where(p => p.Name == pSelectedSkinName).ToList().ForEach(pStyle =>
            {
                SetSkinStylesToResources(pStyle, pSelectedSkinName);

            });
        }

        /// <summary>
        /// Set all the Skins properties values with the selected one
        /// </summary>
        private static void SetSkinStyles(string pSkinName = "")
        {
            if (!string.IsNullOrEmpty(pSkinName) && pSkinName != "Formula" && pSkinName != "Chunk" && pSkinName != "Result")
            {
                SetResourceDictionary(pSkinName);
            }
            else
            {
                SetResourceDictionary("MainSkin");
                SetResourceDictionary("BaseButtons");
                SetResourceDictionary("SndeButton");
                SetResourceDictionary("ScientificButtons");
                SetResourceDictionary("OperatorsButtons");
                SetResourceDictionary("NumericalsButtons");
                SetResourceDictionary("MemoryButtons");
                SetResourceDictionary("TrigonometryButtons");
                SetResourceDictionary("History");
            }

            // Set History styles
            SkinStylesCls lHistoryFormulaStyle = (SkinStylesCls)SelectedSkinObj.Skin.Where(p => p.Name == "Formula").Select(pk => pk).FirstOrDefault();
            SetHistoryStylesToResourceDictionary(lHistoryFormulaStyle, "HistoryFormulaStyle", TextAlignment.Left);

            SkinStylesCls lHistoryChunkStyle = (SkinStylesCls)SelectedSkinObj.Skin.Where(p => p.Name == "Chunk").Select(pk => pk).FirstOrDefault();
            SetHistoryStylesToResourceDictionary(lHistoryChunkStyle, "HistoryChunkStyle", TextAlignment.Center);

            SkinStylesCls lHistoryResultStyle = (SkinStylesCls)SelectedSkinObj.Skin.Where(p => p.Name == "Result").Select(pk => pk).FirstOrDefault();
            SetHistoryStylesToResourceDictionary(lHistoryResultStyle, "HistoryResultStyle", TextAlignment.Right);
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

        #endregion

    }
}
