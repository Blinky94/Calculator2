using System.Windows.Media;

namespace Udemy_Calculator
{
    public class Theme
    {
        /// <summary>
        /// Theme name
        /// </summary>
        public string ThemeSelected { get; set; }

        /// <summary>
        /// Theme name
        /// </summary>
        public string ThemeName { get; set; }

        /// <summary>
        /// Parent node name from the current theme
        /// </summary>
        public string SubThemeName { get; set; }

        /// <summary>
        /// Parent node long name from the current theme
        /// </summary>
        public string SubThemeAttribute { get; set; }

        /// <summary>
        /// Property Color 1 value
        /// </summary>
        public SolidColorBrush Color1 { get; set; }

        /// <summary>
        /// Property Color 1 attribute value
        /// </summary>
        public string Color1Attribute { get; set; }

        /// <summary>
        /// Property Color 2 value
        /// </summary>
        public SolidColorBrush Color2 { get; set; }

        /// <summary>
        /// Property Color 2 attribute value
        /// </summary>
        public string Color2Attribute { get; set; }

        /// <summary>
        /// Property Color 3 value
        /// </summary>
        public SolidColorBrush Color3 { get; set; }

        /// <summary>
        /// Property Color 3 attribute value
        /// </summary>
        public string Color3Attribute { get; set; }

        /// <summary>
        /// Property Color 4 value
        /// </summary>
        public SolidColorBrush Color4 { get; set; }

        /// <summary>
        /// Property Color 4 attribute value
        /// </summary>
        public string Color4Attribute { get; set; }
    }
}
