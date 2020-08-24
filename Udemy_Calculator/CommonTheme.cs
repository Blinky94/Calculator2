using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Media;

namespace Udemy_Calculator
{
    /// <summary>
    /// Class to set the common theme with colors and thickness
    /// </summary>
    public static class CommonTheme
    {
        internal static SolidColorBrush MainCalculatorBackground;
        internal static SolidColorBrush MainCalculatorForeground;
        internal static SolidColorBrush MainCalculatorBorderBrush;
        internal static double MainCalculatorBorderThickness;
        internal static SolidColorBrush BackgroundBaseButtons;
        internal static SolidColorBrush ForegroundBaseButtons;
        internal static SolidColorBrush BorderBrushBaseButtons;
        internal static double BorderThicknessBaseButtons;
        internal static SolidColorBrush Background2ndeButton;
        internal static SolidColorBrush Foreground2ndeButton;
        internal static SolidColorBrush BorderBrush2ndeButton;
        internal static SolidColorBrush BackgroundScientificButtons;
        internal static SolidColorBrush ForegroundScientificButtons;
        internal static SolidColorBrush BorderBrushScientificButtons;
        internal static SolidColorBrush BackgroundOperatorsButtons;
        internal static SolidColorBrush ForegroundOperatorsButtons;
        internal static SolidColorBrush BorderBrushOperatorsButtons;
        internal static SolidColorBrush BackgroundNumericalsButtons;
        internal static SolidColorBrush ForegroundNumericalsButtons;
        internal static SolidColorBrush BorderBrushNumericalsButtons;
        internal static SolidColorBrush BackgroundMemoryButtons;
        internal static SolidColorBrush ForegroundMemoryButtons;
        internal static SolidColorBrush BorderBrushMemoryButtons;
        internal static SolidColorBrush BackgroundTrigonometryButtons;
        internal static SolidColorBrush ForegroundTrigonometryButtons;
        internal static SolidColorBrush BorderBrushTrigonometryButtons;

        private static string mThemeSelectedName;
        /// <summary>
        /// Return the value from the "ThemeSelected" in the xml file
        /// </summary>
        public static string ThemeSelectedName
        {
            get
            {
                return mThemeSelectedName;
            }
            set
            {
                mThemeSelectedName = value;

                // Write the current theme selected to xml file
                XmlParser.SaveThemeNameToXmlFile(mThemeSelectedName);
            }
        }

        /// <summary>
        /// Load all properties from xml file
        /// </summary>
        public static void LoadPropertiesFromXmlFile()
        {
            var lCurrentTheme = CompleteListThemes.Where(p => p.ParentThemeName == ThemeSelectedName).ToList();

            lCurrentTheme.ForEach(p =>
            {
                SetProperty(p.ParameterName, p.ParameterValue);
            });
        }

        /// <summary>
        /// Return the list of theme with the selected one
        /// </summary>
        public static List<ThemeElements> ListThemesWithThemeSelected
        {
            get
            {
                return CompleteListThemes.Where(p => p.ParentThemeName == ThemeSelectedName).ToList();
            }
        }

        /// <summary>
        /// Get the complete list of themes
        /// </summary>
        public static List<ThemeElements> CompleteListThemes { get; internal set; }

        /// <summary>
        /// Get the complete list of parent name themes
        /// </summary>
        public static List<string> GetListParentThemesName
        {
            get
            {
                return CompleteListThemes.Select(p => p.ParentThemeName).Distinct().ToList();
            }
        }

        /// <summary>
        /// Modify the new theme selected into the current list of themeElements
        /// </summary>
        /// <param name="pTSelected"></param>
        public static void ModifyThemeSelectedToList(string pTSelected = "")
        {
            string lPTSelected = !string.IsNullOrEmpty(pTSelected) ? pTSelected : ThemeSelectedName;

            CompleteListThemes.ForEach(p => { p.ThemeSelected = lPTSelected; });
        }

        /// <summary>
        /// Set the theme with pName as the parameter name, and pValue as the value of this parameter
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pValue"></param>
        public static void SetProperty(string pName, string pValue)
        {
            SolidColorBrush lSolidColorBrushValue = default;

            if (double.TryParse(pValue.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double lDoubleValue) == false)
            {
                if (!string.IsNullOrEmpty(pValue))
                {
                    lSolidColorBrushValue = (SolidColorBrush)new BrushConverter().ConvertFromString(pValue);
                }
            }

            switch (pName)
            {
                case "MainCalculatorBackground":
                    MainCalculatorBackground = lSolidColorBrushValue; break;
                case "MainCalculatorForeground":
                    MainCalculatorForeground = lSolidColorBrushValue; break;
                case "MainCalculatorBorderBrush":
                    MainCalculatorBorderBrush = lSolidColorBrushValue; break;
                case "MainCalculatorBorderThickness":
                    MainCalculatorBorderThickness = lDoubleValue; break;
                case "BackgroundBaseButtons":
                    BackgroundBaseButtons = lSolidColorBrushValue; break;
                case "ForegroundBaseButtons":
                    ForegroundBaseButtons = lSolidColorBrushValue; break;
                case "BorderBrushBaseButtons":
                    BorderBrushBaseButtons = lSolidColorBrushValue; break;
                case "BorderThicknessBaseButtons":
                    BorderThicknessBaseButtons = lDoubleValue; break;
                case "Background2ndeButton":
                    Background2ndeButton = lSolidColorBrushValue; break;
                case "Foreground2ndeButton":
                    Foreground2ndeButton = lSolidColorBrushValue; break;
                case "BorderBrush2ndeButton":
                    BorderBrush2ndeButton = lSolidColorBrushValue; break;
                case "BackgroundScientificButtons":
                    BackgroundScientificButtons = lSolidColorBrushValue; break;
                case "ForegroundScientificButtons":
                    ForegroundScientificButtons = lSolidColorBrushValue; break;
                case "BorderBrushScientificButtons":
                    BorderBrushScientificButtons = lSolidColorBrushValue; break;
                case "BackgroundOperatorsButtons":
                    BackgroundOperatorsButtons = lSolidColorBrushValue; break;
                case "ForegroundOperatorsButtons":
                    ForegroundOperatorsButtons = lSolidColorBrushValue; break;
                case "BorderBrushOperatorsButtons":
                    BorderBrushOperatorsButtons = lSolidColorBrushValue; break;
                case "BackgroundNumericalsButtons":
                    BackgroundNumericalsButtons = lSolidColorBrushValue; break;
                case "ForegroundNumericalsButtons":
                    ForegroundNumericalsButtons = lSolidColorBrushValue; break;
                case "BorderBrushNumericalsButtons":
                    BorderBrushNumericalsButtons = lSolidColorBrushValue; break;
                case "BackgroundMemoryButtons":
                    BackgroundMemoryButtons = lSolidColorBrushValue; break;
                case "ForegroundMemoryButtons":
                    ForegroundMemoryButtons = lSolidColorBrushValue; break;
                case "BorderBrushMemoryButtons":
                    BorderBrushMemoryButtons = lSolidColorBrushValue; break;
                case "BackgroundTrigonometryButtons":
                    BackgroundTrigonometryButtons = lSolidColorBrushValue; break;
                case "ForegroundTrigonometryButtons":
                    ForegroundTrigonometryButtons = lSolidColorBrushValue; break;
                case "BorderBrushTrigonometryButtons":
                    BorderBrushTrigonometryButtons = lSolidColorBrushValue; break;
            }
        }
    }
}
