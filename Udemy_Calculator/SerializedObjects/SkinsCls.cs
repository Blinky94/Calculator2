using System.Collections.Generic;
using System.Xml.Serialization;

namespace Udemy_Calculator.SerializedObjects
{
    [XmlRoot("Skins")]
    public class SkinsCls
    {
        public SkinsCls() { }

        [XmlAttribute("selected")]
        public string Selected { get; set; }

        [XmlElement("Skin")]
        public List<SkinCls> Skins { get; set; }

        /// <summary>
        /// Adding new blank skin in the list
        /// </summary>
        internal void AddNewBlankSkin(string pSkinName)
        {
            SkinCls lNewSkin = new SkinCls(pSkinName);
            Skins.Add(lNewSkin);
        }
    }
}
