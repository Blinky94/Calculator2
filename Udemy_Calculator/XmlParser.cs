using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Udemy_Calculator.SerializedObjects;

namespace Udemy_Calculator
{
    public static class XmlParser
    {
        private const string mPath = @"~/../../../Params/Theme.xml";
        public static XmlDocument SkinsXmlDoc { get; private set; }

        public static string LoadParamsXmlSkins()
        {
            // Open internal Skin file       
            SkinsXmlDoc = new XmlDocument();

            if (SkinsXmlDoc != null)
            {
                // Open XML file
                SkinsXmlDoc.Load(mPath);
            }
            else
            {
                TraceLogs.AddError($"{GlobalUsage.GetCurrentMethodName}: unable to load Skins ({CommonSkins.SkinSelectedName}) because the source file is not found !!!");
            }

            return SkinsXmlDoc.InnerXml;
        }

        /// <summary>
        /// Serialization of SkinsCls into the xml file
        /// </summary>
        /// <param name="pSkinsToXml"></param>
        public static void SerializeSkinsToXML(SkinsCls pSkinsToXml)
        {
            using (StreamWriter writer = new StreamWriter(mPath))
            {
                var serializer = new XmlSerializer(typeof(SkinsCls), new XmlRootAttribute("Skins"));
                serializer.Serialize(writer, pSkinsToXml);
            }
        }

        /// <summary>
        /// Deserialization of an XML SkinsCls
        /// </summary>
        /// <param name="pSkinsFromXml"></param>
        /// <returns></returns>
        public static SkinsCls DeserializeSkinsFromXML(string pSkinsFromXml)
        {
            var stringReader = new StringReader(pSkinsFromXml);
            var serializer = new XmlSerializer(typeof(SkinsCls), new XmlRootAttribute("Skins"));

            return (SkinsCls)serializer.Deserialize(stringReader);
        }

        ///// <summary>
        ///// Check if the skin selected allready exists.
        ///// </summary>
        ///// <returns></returns>
        //private static bool IsCurrentSkinAllreadyExists(string pName)
        //{
        //    if (mDoc != null)
        //    {
        //        // Parsing all Skins names
        //        if (CommonSkin.GetParentSkinNames.Contains(pName))
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}
    }
}
