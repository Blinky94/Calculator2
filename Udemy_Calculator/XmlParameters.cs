using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Udemy_Calculator
{
    public class XmlParameters
    {
        public List<ThemeObj> GetListParameters { get; private set; }

        private string mPath;

        public XmlParameters(string pPath = "")
        {
            if (pPath == string.Empty)
            {
                // Open internal parameters file
                mPath = @"~/../../../Params/Theme.xml";
            }
            else
            {
                // Open custom parameters file
                mPath = pPath;
            }
        }

        private List<ThemeObj> SetListOfParameters()
        {
            List<ThemeObj> lList = new List<ThemeObj>();

            // Open XML file and affect values
            XDocument lDoc = XDocument.Load(mPath);

            foreach (XElement lElement in lDoc.Root.Elements())
            {
                foreach (XNode lNode in lElement.Nodes())
                {
                    XElement node = lNode as XElement;
                    lList.Add(new ThemeObj()
                    {
                        ThemeName = lElement.Attribute("name").Value,
                        ParameterName = node.Name.ToString(),
                        ParameterValueStr = node.Value
                    });
                }
            }

            return lList;
        }

        public void ReadParameters()
        {
            GetListParameters = SetListOfParameters();
        }

        private void WriteParameters()
        {
            //Nothing at this time
        }
    }
}
