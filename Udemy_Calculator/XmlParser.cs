using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Udemy_Calculator
{
    public static class XmlParser
    {
        private static XmlDocument mDoc;
        private const string mPath = @"~/../../../Params/Theme.xml";

        public static void LoadParamsXmlTheme()
        {
            // Open internal theme file       
            mDoc = new XmlDocument();
            // Open XML file and affect values
            mDoc.Load(mPath);

            if (mDoc == null)
            {
                TraceLogs.AddWarning($"{GlobalUsage.GetCurrentMethodName}: unable to save the current theme ({CommonTheme.ThemeSelectedName}) because the source file is null !!!");
            }
        }

        /// <summary>
        /// Saving the current theme name selected to the xml file
        /// </summary>
        /// <param name="pThemeName"></param>
        public static void SaveThemeNameToXmlFile(string pThemeName)
        {
            XmlNode lNode = mDoc.SelectSingleNode("//Themes/ThemeSelected");
            lNode.Attributes["name"].InnerText = pThemeName;
            mDoc.Save(mPath);
        }

        /// <summary>
        ///Save the complete theme to the xml file
        /// </summary>
        /// <param name="pCurrentTheme"></param>
        public static void SaveTheme(string pCurrentTheme = "")
        {
            string lCurrentTheme = pCurrentTheme.Length > 0 ? pCurrentTheme : mDoc.SelectSingleNode("//Themes/ThemeSelected").Attributes["name"].InnerText;

            if (mDoc == null)
            {
                // Openning theme.xml file
                LoadParamsXmlTheme();
            }

            // Get current theme from xml relative to pCurrentTheme
            XmlNodeList lNodes = mDoc?.DocumentElement?.ChildNodes;

            lNodes[0].Attributes["name"].Value = pCurrentTheme;

            foreach (XmlElement lNode in lNodes)
            {
                if (lNode.Attributes["name"].InnerText == lCurrentTheme)
                {
                    // Follow every node corresponding to the saved data from CommonTheme properties
                    foreach (XmlElement lSubNode in lNode.ChildNodes)
                    {
                        foreach (XmlElement lSubSubNode in lSubNode)
                        {
                            CommonTheme.ListSelectedTheme.Where(p => p.ParentName == lSubNode.Name).ToList().ForEach(p =>
                            {
                                if (lSubSubNode.Name.Contains("Background"))
                                {
                                    lSubSubNode.InnerText = p.Background.ToString();
                                }
                                if (lSubSubNode.Name.Contains("Foreground"))
                                {
                                    lSubSubNode.InnerText = p.Foreground.ToString();
                                }
                                if (lSubSubNode.Name.Contains("BorderBrush"))
                                {
                                    lSubSubNode.InnerText = p.BorderBrush.ToString();
                                }
                            });
                        }
                    }
                }
            }

            //Save the new theme
            mDoc.Save(mPath);

            TraceLogs.AddInfo($"{GlobalUsage.GetCurrentMethodName}: the current theme ({pCurrentTheme}) has been saved in the xml file properly.");
        }
    }
}
