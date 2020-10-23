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
                            CommonTheme.ListSelectedTheme.Where(p => p.SubThemeName == lSubNode.Name).ToList().ForEach(p =>
                            {
                                if (lSubSubNode.Name.Contains("Color1") && p.Color1 != null)
                                {
                                    lSubSubNode.InnerText = p.Color1.ToString();
                                }
                                if (lSubSubNode.Name.Contains("Color2") && p.Color2 != null)
                                {
                                    lSubSubNode.InnerText = p.Color2.ToString();
                                }
                                if (lSubSubNode.Name.Contains("Color3") && p.Color3 != null)
                                {
                                    lSubSubNode.InnerText = p.Color3.ToString();
                                }
                                if (lSubSubNode.Name.Contains("Color4") && p.Color4 != null)
                                {
                                    lSubSubNode.InnerText = p.Color4.ToString();
                                }
                                if (lSubSubNode.Name.Contains("FontFamily1") && p.FontFamily1 != null)
                                {
                                    lSubSubNode.InnerText = p.FontFamily1.ToString();
                                }
                                if (lSubSubNode.Name.Contains("FontSize1") && p.FontSize1 != 0)
                                {
                                    lSubSubNode.InnerText = p.FontSize1.ToString();
                                }
                                if (lSubSubNode.Name.Contains("FontWeight1") && p.FontWeight1 != null)
                                {
                                    lSubSubNode.InnerText = p.FontWeight1.ToString();
                                }
                                if (lSubSubNode.Name.Contains("FontFamily2") && p.FontFamily2 != null)
                                {
                                    lSubSubNode.InnerText = p.FontFamily2.ToString();
                                }
                                if (lSubSubNode.Name.Contains("FontSize2") && p.FontSize2 != 0)
                                {
                                    lSubSubNode.InnerText = p.FontSize2.ToString();
                                }
                                if (lSubSubNode.Name.Contains("FontWeight2") && p.FontWeight2 != null)
                                {
                                    lSubSubNode.InnerText = p.FontWeight2.ToString();
                                }
                                if (lSubSubNode.Name.Contains("FontFamily3") && p.FontFamily3 != null)
                                {
                                    lSubSubNode.InnerText = p.FontFamily3.ToString();
                                }
                                if (lSubSubNode.Name.Contains("FontSize3") && p.FontSize3 != 0)
                                {
                                    lSubSubNode.InnerText = p.FontSize3.ToString();
                                }
                                if (lSubSubNode.Name.Contains("FontWeight3") && p.FontWeight3 != null)
                                {
                                    lSubSubNode.InnerText = p.FontWeight3.ToString();
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
