using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Udemy_Calculator.SerializedObjects
{
    [Serializable]
    public class SkinCls
    {
        public SkinCls() { }

        [XmlElement("SkinStyles")]
        public List<SkinStylesCls> Skin { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        public SkinCls(string pSkinName)
        {
            Name = pSkinName;
            Skin = new List<SkinStylesCls>();

            var lNewSkinStyle = new SkinStylesCls("MainSkin", "Main Skin")
            {
                Background = new SkinStyleBackground() { Name = "Background", Value = "#ffffff" },
                Foreground = new SkinStyleForeground() { Name = "Foreground", Value = "#000000" },
                Borderbrush = new SkinStyleBorderbrush() { Name = "Border brush", Value = "#000000" }
            };

            Skin.Add(lNewSkinStyle);

            lNewSkinStyle = new SkinStylesCls("BaseButtons", "General buttons")
            {
                Background = new SkinStyleBackground() { Name = "Background", Value = "#ffffff" },
                Foreground = new SkinStyleForeground() { Name = "Foreground", Value = "#000000" },
                Borderbrush = new SkinStyleBorderbrush() { Name = "Border brush", Value = "#000000" }
            };

            Skin.Add(lNewSkinStyle);

            lNewSkinStyle = new SkinStylesCls("SndeButton", "Second button")
            {
                Background = new SkinStyleBackground() { Name = "Background", Value = "#ffffff" },
                Foreground = new SkinStyleForeground() { Name = "Foreground", Value = "#000000" },
                Borderbrush = new SkinStyleBorderbrush() { Name = "Border brush", Value = "#000000" }
            };

            Skin.Add(lNewSkinStyle);

            lNewSkinStyle = new SkinStylesCls("ScientificButtons", "Scientific buttons")
            {
                Background = new SkinStyleBackground() { Name = "Background", Value = "#ffffff" },
                Foreground = new SkinStyleForeground() { Name = "Foreground", Value = "#000000" },
                Borderbrush = new SkinStyleBorderbrush() { Name = "Border brush", Value = "#000000" }
            };

            Skin.Add(lNewSkinStyle);

            lNewSkinStyle = new SkinStylesCls("OperatorsButtons", "Operator buttons")
            {
                Background = new SkinStyleBackground() { Name = "Background", Value = "#ffffff" },
                Foreground = new SkinStyleForeground() { Name = "Foreground", Value = "#000000" },
                Borderbrush = new SkinStyleBorderbrush() { Name = "Border brush", Value = "#000000" }
            };

            Skin.Add(lNewSkinStyle);

            lNewSkinStyle = new SkinStylesCls("NumericalsButtons", "Numerical buttons")
            {
                Background = new SkinStyleBackground() { Name = "Background", Value = "#ffffff" },
                Foreground = new SkinStyleForeground() { Name = "Foreground", Value = "#000000" },
                Borderbrush = new SkinStyleBorderbrush() { Name = "Border brush", Value = "#000000" }
            };

            Skin.Add(lNewSkinStyle);

            lNewSkinStyle = new SkinStylesCls("MemoryButtons", "Memory buttons")
            {
                Background = new SkinStyleBackground() { Name = "Background", Value = "#ffffff" },
                Foreground = new SkinStyleForeground() { Name = "Foreground", Value = "#000000" },
                Borderbrush = new SkinStyleBorderbrush() { Name = "Border brush", Value = "#000000" }
            };

            Skin.Add(lNewSkinStyle);

            lNewSkinStyle = new SkinStylesCls("TrigonometryButtons", "Trigonometry buttons")
            {
                Background = new SkinStyleBackground() { Name = "Background", Value = "#ffffff" },
                Foreground = new SkinStyleForeground() { Name = "Foreground", Value = "#000000" },
                Borderbrush = new SkinStyleBorderbrush() { Name = "Border brush", Value = "#000000" }
            };

            Skin.Add(lNewSkinStyle);

            lNewSkinStyle = new SkinStylesCls("History", "History style")
            {
                Background = new SkinStyleBackground() { Name = "Background", Value = "#ffffff" },
            };

            Skin.Add(lNewSkinStyle);

            lNewSkinStyle = new SkinStylesCls("Formula", "History formula styles")
            {
                Foreground = new SkinStyleForeground() { Name = "Result foreground", Value = "#000000" },
                FontFamily = new SkinStyleFontFamily() { Name = "Result font family", Value = "Roboto" },
                FontSize = new SkinStyleFontSize() { Name = "Result font size", Value = "20" },
                FontWeight = new SkinStyleFontWeight() { Name = "Result font weight", Value = "Bold" }
            };

            Skin.Add(lNewSkinStyle);

            lNewSkinStyle = new SkinStylesCls("Chunk", "History chunk styles")
            {
                Foreground = new SkinStyleForeground() { Name = "Result foreground", Value = "#000000" },
                FontFamily = new SkinStyleFontFamily() { Name = "Result font family", Value = "Roboto" },
                FontSize = new SkinStyleFontSize() { Name = "Result font size", Value = "20" },
                FontWeight = new SkinStyleFontWeight() { Name = "Result font weight", Value = "Bold" }
            };

            Skin.Add(lNewSkinStyle);

            lNewSkinStyle = new SkinStylesCls("Result", "History result styles")
            {
                Foreground = new SkinStyleForeground() { Name = "Result foreground", Value = "#000000" },
                FontFamily = new SkinStyleFontFamily() { Name = "Result font family", Value = "Roboto" },
                FontSize = new SkinStyleFontSize() { Name = "Result font size", Value = "20" },
                FontWeight = new SkinStyleFontWeight() { Name = "Result font weight", Value = "Bold" }
            };

            Skin.Add(lNewSkinStyle);
        }
    }
}
