using System.Collections.Generic;
using System.Xml;

namespace Udemy_Calculator
{
    public static class XmlParser
    {
        private static XmlDocument mDoc;
        private static string mPath = @"~/../../../Params/Theme.xml";

        public static void OpenTheme()
        {
            // Open internal theme file       
            XmlDocument lDoc = new XmlDocument();

            // Open XML file and affect values
            lDoc.Load(mPath);
            // Set the loaded xml theme file to the local static variable
            mDoc = lDoc;
        }

        /// <summary>
        /// Extract every parameters from the theme xml file and store them to a list of themeObject
        /// </summary>
        /// <param name="mDoc"></param>
        /// <returns></returns>
        public static List<ThemeElements> GetListOfParametersFromXmlFile()
        {
            List<ThemeElements> lList = new List<ThemeElements>();
            string lThemeSelected = string.Empty;

            foreach (XmlElement lElement in mDoc?.DocumentElement.ChildNodes)
            {
                if (lElement.Name == "ThemeSelected")
                {
                    lThemeSelected = lElement.GetAttribute("name");
                }

                foreach (XmlElement lNode in lElement)
                {
                    foreach (XmlElement lSubNode in lNode)
                    {
                        lList.Add(new ThemeElements()
                        {
                            ThemeSelected = lThemeSelected,
                            ParentThemeName = lElement.GetAttribute("name"),
                            ChildThemeName = lNode.Name,
                            ChildThemeText = lNode.GetAttribute("name"),
                            ParameterName = lSubNode.Name,
                            ParameterText = lSubNode.GetAttribute("name"),
                            ParameterValue = lSubNode.InnerText
                        });
                    }
                }
            }

            return lList;
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
        /// Save the complete theme to the xml file
        /// </summary>
        public static void SaveTheme()
        {

        }
    }
}
