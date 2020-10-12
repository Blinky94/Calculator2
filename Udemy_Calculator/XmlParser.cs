using System.Collections.Generic;
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

        ///// <summary>
        ///// Save the complete theme to the xml file
        ///// </summary>
        //public static void SaveTheme(string pCurrentTheme = "")
        //{
        //    string lCurrentTheme = pCurrentTheme.Length > 0 ? pCurrentTheme : mDoc.SelectSingleNode("//Themes/ThemeSelected").Attributes["name"].InnerText;

        //    // Get current theme from xml relative to pCurrentTheme
        //    XmlNodeList lNodes = mDoc?.DocumentElement?.ChildNodes;

        //    //foreach (XmlElement lNode in lNodes)
        //    //{
        //    //    if (lNode.Attributes["name"].InnerText == lCurrentTheme)
        //    //    {
        //    //        // Follow every node corresponding to the saved data from CommonTheme properties
        //    //        foreach (XmlElement lSubNode in lNode.ChildNodes)
        //    //        {
        //    //            switch (lSubNode.Attributes["name"].InnerText)
        //    //            {
        //    //                case "Main theme":
        //    //                    foreach (XmlElement lSubSubNode in lSubNode)
        //    //                    {
        //    //                        switch (lSubSubNode.Attributes["name"].InnerText)
        //    //                        {
        //    //                            case "Background":
        //    //                                lSubSubNode.InnerText = CommonTheme.MainCalculatorBackground.ToString();
        //    //                                break;
        //    //                            case "Foreground":
        //    //                                lSubSubNode.InnerText = CommonTheme.MainCalculatorForeground.ToString();
        //    //                                break;
        //    //                            case "Border color":
        //    //                                lSubSubNode.InnerText = CommonTheme.MainCalculatorBorderBrush.ToString();
        //    //                                break;
        //    //                        }
        //    //                    }
        //    //                    break;

        //    //                case "General buttons":
        //    //                    foreach (XmlElement lSubSubNode in lSubNode)
        //    //                    {
        //    //                        switch (lSubSubNode.Attributes["name"].InnerText)
        //    //                        {
        //    //                            case "Background":
        //    //                                lSubSubNode.InnerText = CommonTheme.BackgroundBaseButtons.ToString();
        //    //                                break;
        //    //                            case "Foreground":
        //    //                                lSubSubNode.InnerText = CommonTheme.ForegroundBaseButtons.ToString();
        //    //                                break;
        //    //                            case "Border color":
        //    //                                lSubSubNode.InnerText = CommonTheme.BorderBrushBaseButtons.ToString();
        //    //                                break;     
        //    //                        }
        //    //                    }
        //    //                    break;
        //    //                case "Seconde button":
        //    //                    foreach (XmlElement lSubSubNode in lSubNode)
        //    //                    {
        //    //                        switch (lSubSubNode.Attributes["name"].InnerText)
        //    //                        {
        //    //                            case "Background":
        //    //                                lSubSubNode.InnerText = CommonTheme.Background2ndeButton.ToString();
        //    //                                break;
        //    //                            case "Foreground":
        //    //                                lSubSubNode.InnerText = CommonTheme.Foreground2ndeButton.ToString();
        //    //                                break;
        //    //                            case "Border color":
        //    //                                lSubSubNode.InnerText = CommonTheme.BorderBrush2ndeButton.ToString();
        //    //                                break;
        //    //                        }
        //    //                    }
        //    //                    break;
        //    //                case "Scientific buttons":
        //    //                    foreach (XmlElement lSubSubNode in lSubNode)
        //    //                    {
        //    //                        switch (lSubSubNode.Attributes["name"].InnerText)
        //    //                        {
        //    //                            case "Background":
        //    //                                lSubSubNode.InnerText = CommonTheme.BackgroundScientificButtons.ToString();
        //    //                                break;
        //    //                            case "Foreground":
        //    //                                lSubSubNode.InnerText = CommonTheme.ForegroundScientificButtons.ToString();
        //    //                                break;
        //    //                            case "Border color":
        //    //                                lSubSubNode.InnerText = CommonTheme.BorderBrushScientificButtons.ToString();
        //    //                                break;
        //    //                        }
        //    //                    }
        //    //                    break;
        //    //                case "Operator buttons":
        //    //                    foreach (XmlElement lSubSubNode in lSubNode)
        //    //                    {
        //    //                        switch (lSubSubNode.Attributes["name"].InnerText)
        //    //                        {
        //    //                            case "Background":
        //    //                                lSubSubNode.InnerText = CommonTheme.BackgroundOperatorsButtons.ToString();
        //    //                                break;
        //    //                            case "Foreground":
        //    //                                lSubSubNode.InnerText = CommonTheme.ForegroundOperatorsButtons.ToString();
        //    //                                break;
        //    //                            case "Border color":
        //    //                                lSubSubNode.InnerText = CommonTheme.BorderBrushOperatorsButtons.ToString();
        //    //                                break;
        //    //                        }
        //    //                    }
        //    //                    break;
        //    //                case "Numerical buttons":
        //    //                    foreach (XmlElement lSubSubNode in lSubNode)
        //    //                    {
        //    //                        switch (lSubSubNode.Attributes["name"].InnerText)
        //    //                        {
        //    //                            case "Background":
        //    //                                lSubSubNode.InnerText = CommonTheme.BackgroundNumericalsButtons.ToString();
        //    //                                break;
        //    //                            case "Foreground":
        //    //                                lSubSubNode.InnerText = CommonTheme.ForegroundNumericalsButtons.ToString();
        //    //                                break;
        //    //                            case "Border color":
        //    //                                lSubSubNode.InnerText = CommonTheme.BorderBrushNumericalsButtons.ToString();
        //    //                                break;
        //    //                        }
        //    //                    }
        //    //                    break;
        //    //                case "Memory buttons":
        //    //                    foreach (XmlElement lSubSubNode in lSubNode)
        //    //                    {
        //    //                        switch (lSubSubNode.Attributes["name"].InnerText)
        //    //                        {
        //    //                            case "Background":
        //    //                                lSubSubNode.InnerText = CommonTheme.BackgroundMemoryButtons.ToString();
        //    //                                break;
        //    //                            case "Foreground":
        //    //                                lSubSubNode.InnerText = CommonTheme.ForegroundMemoryButtons.ToString();
        //    //                                break;
        //    //                            case "Border color":
        //    //                                lSubSubNode.InnerText = CommonTheme.BorderBrushMemoryButtons.ToString();
        //    //                                break;
        //    //                        }
        //    //                    }
        //    //                    break;
        //    //                case "Trigonometry buttons":
        //    //                    foreach (XmlElement lSubSubNode in lSubNode)
        //    //                    {
        //    //                        switch (lSubSubNode.Attributes["name"].InnerText)
        //    //                        {
        //    //                            case "Background":
        //    //                                lSubSubNode.InnerText = CommonTheme.BackgroundTrigonometryButtons.ToString();
        //    //                                break;
        //    //                            case "Foreground":
        //    //                                lSubSubNode.InnerText = CommonTheme.ForegroundTrigonometryButtons.ToString();
        //    //                                break;
        //    //                            case "Border color":
        //    //                                lSubSubNode.InnerText = CommonTheme.BorderBrushTrigonometryButtons.ToString();
        //    //                                break;
        //    //                        }
        //    //                    }
        //    //                    break;
        //    //            }
        //    //        }
        //    //    }
        //  //  }

        //    // Save the new theme
        //   // mDoc.Save(mPath);
        //}
    }
}
