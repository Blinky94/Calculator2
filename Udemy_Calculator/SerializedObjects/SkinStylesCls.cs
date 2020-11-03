using System;
using System.Xml.Serialization;

namespace Udemy_Calculator.SerializedObjects
{
    [Serializable]
    public class SkinStyleBackground
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    [Serializable]
    public class SkinStyleForeground
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    [Serializable]
    public class SkinStyleFontFamily
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    [Serializable]
    public class SkinStyleBorderbrush
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    [Serializable]
    public class SkinStyleFontSize
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    [Serializable]
    public class SkinStyleFontWeight
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public class SkinStylesCls
    {
        public SkinStylesCls() { }

        public SkinStylesCls(string pName, string pLongName)
        {
            Name = pName;
            LongName = pLongName;
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("longName")]
        public string LongName { get; set; }

        public SkinStyleBackground Background { get; set; }

        public SkinStyleForeground Foreground { get; set; }

        public SkinStyleBorderbrush Borderbrush { get; set; }

        public SkinStyleFontFamily FontFamily { get; set; }

        public SkinStyleFontSize FontSize { get; set; }

        public SkinStyleFontWeight FontWeight { get; set; }
    }
}
