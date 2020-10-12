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
        public string ThemeRootName { get; set; }

        /// <summary>
        /// Parent node name from the current theme
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// Parent node long name from the current theme
        /// </summary>
        public string ParentLongName { get; set; }

        /// <summary>
        /// Property background value
        /// </summary>
        public SolidColorBrush Background { get; set; }

        /// <summary>
        /// Property foreground value
        /// </summary>
        public SolidColorBrush Foreground { get; set; }

        /// <summary>
        /// Property borderbrush value
        /// </summary>
        public SolidColorBrush BorderBrush { get; set; }
    }
}
