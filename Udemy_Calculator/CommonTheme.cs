using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Media;

namespace Udemy_Calculator
{
    /// <summary>
    /// Class to set the common theme with colors and thickness
    /// </summary>
    public class CommonTheme
    {
        public static SolidColorBrush MainCalculatorBackground;
        public static SolidColorBrush MainCalculatorForeground;
        public static SolidColorBrush MainCalculatorBorderBrush;
        public static double MainCalculatorBorderThickness;
        public static SolidColorBrush BackgroundBaseButtons;
        public static SolidColorBrush ForegroundBaseButtons;
        public static SolidColorBrush BorderBrushBaseButtons;
        public static double BorderThicknessBaseButtons;
        public static SolidColorBrush Background2ndeButton;
        public static SolidColorBrush Foreground2ndeButton;
        public static SolidColorBrush BorderBrush2ndeButton;
        public static SolidColorBrush BackgroundScientificButtons;
        public static SolidColorBrush ForegroundScientificButtons;
        public static SolidColorBrush BorderBrushScientificButtons;
        public static SolidColorBrush BackgroundOperatorsButtons;
        public static SolidColorBrush ForegroundOperatorsButtons;
        public static SolidColorBrush BorderBrushOperatorsButtons;
        public static SolidColorBrush BackgroundNumericalsButtons;
        public static SolidColorBrush ForegroundNumericalsButtons;
        public static SolidColorBrush BorderBrushNumericalsButtons;
        public static SolidColorBrush BackgroundMemoryButtons;
        public static SolidColorBrush ForegroundMemoryButtons;
        public static SolidColorBrush BorderBrushMemoryButtons;
        public static SolidColorBrush BackgroundTrigonometryButtons;
        public static SolidColorBrush ForegroundTrigonometryButtons;
        public static SolidColorBrush BorderBrushTrigonometryButtons;

        private static string mThemeSelectedName;
        /// <summary>
        /// Return the value from the "ThemeSelected" in the xml file
        /// </summary>
        public static string ThemeSelectedName
        {
            get
            {
                return mCompleteListThemes?.FirstOrDefault(p => p.ParentThemeName == p.ThemeSelected).ThemeSelected;
            }
            set
            {
                mThemeSelectedName = value;

                // Write the current theme selected to xml file
                XmlParser.SaveThemeNameToXmlFile(mThemeSelectedName);
            }
        }

        /// <summary>
        /// Set all properties from xml file
        /// </summary>
        public static void SetThemesProperties()
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
                return mCompleteListThemes.Where(p => p.ParentThemeName == ThemeSelectedName).ToList();
            }
        }

        // Get the complete list of themes
        private static List<ThemeElements> mCompleteListThemes;
        public static List<ThemeElements> CompleteListThemes
        {
            get
            {
                return mCompleteListThemes;
            }

            internal set
            {
                mCompleteListThemes = value;
            }
        }

        // Get the complete list of parent name themes
        public static List<string> ListParentThemesName
        {
            get
            {
                return mCompleteListThemes.Select(p => p.ParentThemeName).Distinct().ToList();
            }
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
