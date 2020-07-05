using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Udemy_Calculator
{
    public enum CalculatorMode { Standard, Scientific, Binary, Graphs, Calendar, Area, Electronic, Energy, Flow, Force, Length, Power, Pressure, Speed, Temperature, Time, Volume, Weight, Data, Angle, Currency, Options };

    /// <summary>
    /// Interaction logic for MenuSide.xaml
    /// </summary>
    public partial class MenuSide_Control : UserControl
    {
        private static List<string> mListThemeName;
        private static List<ThemeObj> mListTheme;

        #region Themes

        #region MainCalculatorBackground

        public static readonly DependencyProperty MainCalculatorBackgroundProperty =
            DependencyProperty.Register("MainCalculatorBackground",
            typeof(Brush),
            typeof(MainWindow),
            new PropertyMetadata(Brushes.Transparent));
        public Brush MainCalculatorBackground
        {
            get { return (Brush)GetValue(MainCalculatorBackgroundProperty); }
            set { SetValue(MainCalculatorBackgroundProperty, value); }
        }

        #endregion

        #region MainCalculatorBorderBrush

        public static readonly DependencyProperty MainCalculatorBorderBrushProperty =
            DependencyProperty.Register("MainCalculatorBorderBrush",
            typeof(Brush),
            typeof(MainWindow),
            new PropertyMetadata(Brushes.Transparent));
        public Brush MainCalculatorBorderBrush
        {
            get { return (Brush)GetValue(MainCalculatorBorderBrushProperty); }
            set { SetValue(MainCalculatorBorderBrushProperty, value); }
        }

        #endregion

        #region MainCalculatorBorderThickness

        public static readonly DependencyProperty MainCalculatorBorderThicknessProperty =
            DependencyProperty.Register("MainCalculatorBorderThickness",
            typeof(double),
            typeof(MainWindow),
            new PropertyMetadata(5D));
        public double MainCalculatorBorderThickness
        {
            get { return (double)GetValue(MainCalculatorBorderThicknessProperty); }
            set { SetValue(MainCalculatorBorderThicknessProperty, value); }
        }

        #endregion

        #region Background2ndeButton

        public static readonly DependencyProperty Background2ndeButtonProperty =
            DependencyProperty.Register("Background2ndeButton",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush Background2ndeButton
        {
            get { return (Brush)GetValue(Background2ndeButtonProperty); }
            set { SetValue(Background2ndeButtonProperty, value); }
        }

        #endregion

        #region Foreground2ndeButton

        public static readonly DependencyProperty Foreground2ndeButtonProperty =
            DependencyProperty.Register("Foreground2ndeButton",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush Foreground2ndeButton
        {
            get { return (Brush)GetValue(Foreground2ndeButtonProperty); }
            set { SetValue(Foreground2ndeButtonProperty, value); }
        }

        #endregion

        #region BorderBrush2ndeButton

        public static readonly DependencyProperty BorderBrush2ndeButtonProperty =
            DependencyProperty.Register("BorderBrush2ndeButton",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BorderBrush2ndeButton
        {
            get { return (Brush)GetValue(BorderBrush2ndeButtonProperty); }
            set { SetValue(BorderBrush2ndeButtonProperty, value); }
        }

        #endregion

        #region BorderThickness2ndeButton

        public static readonly DependencyProperty BorderThickness2ndeButtonProperty =
            DependencyProperty.Register("BorderThickness2ndeButton",
            typeof(double),
            typeof(Calculator_Control),
            new PropertyMetadata(5D));
        public double BorderThickness2ndeButton
        {
            get { return (double)GetValue(BorderThickness2ndeButtonProperty); }
            set { SetValue(BorderThickness2ndeButtonProperty, value); }
        }

        #endregion

        #region BackgroundBaseButtons

        public static readonly DependencyProperty BackgroundBaseButtonsProperty =
            DependencyProperty.Register("BackgroundBaseButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BackgroundBaseButtons
        {
            get { return (Brush)GetValue(BackgroundBaseButtonsProperty); }
            set { SetValue(BackgroundBaseButtonsProperty, value); }
        }

        #endregion

        #region ForegroundBaseButtons

        public static readonly DependencyProperty ForegroundBaseButtonsProperty =
            DependencyProperty.Register("ForegroundBaseButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush ForegroundBaseButtons
        {
            get { return (Brush)GetValue(ForegroundBaseButtonsProperty); }
            set { SetValue(ForegroundBaseButtonsProperty, value); }
        }

        #endregion

        #region BorderBrushBaseButtons

        public static readonly DependencyProperty BorderBrushBaseButtonsProperty =
            DependencyProperty.Register("BorderBrushBaseButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BorderBrushBaseButtons
        {
            get { return (Brush)GetValue(BorderBrushBaseButtonsProperty); }
            set { SetValue(BorderBrushBaseButtonsProperty, value); }
        }

        #endregion

        #region BorderThicknessBaseButtons

        public static readonly DependencyProperty BorderThicknessBaseButtonsProperty =
            DependencyProperty.Register("BorderThicknessBaseButtons",
            typeof(double),
            typeof(Calculator_Control),
            new PropertyMetadata(5D));
        public double BorderThicknessBaseButtons
        {
            get { return (double)GetValue(BorderThicknessBaseButtonsProperty); }
            set { SetValue(BorderThicknessBaseButtonsProperty, value); }
        }

        #endregion

        #region BackgroundScientificButtons

        public static readonly DependencyProperty BackgroundScientificButtonsProperty =
            DependencyProperty.Register("BackgroundScientificButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BackgroundScientificButtons
        {
            get { return (Brush)GetValue(BackgroundScientificButtonsProperty); }
            set { SetValue(BackgroundScientificButtonsProperty, value); }
        }

        #endregion

        #region ForegroundScientificButtons

        public static readonly DependencyProperty ForegroundScientificButtonsProperty =
            DependencyProperty.Register("ForegroundScientificButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush ForegroundScientificButtons
        {
            get { return (Brush)GetValue(ForegroundScientificButtonsProperty); }
            set { SetValue(ForegroundScientificButtonsProperty, value); }
        }

        #endregion

        #region BorderBrushScientificButtons

        public static readonly DependencyProperty BorderBrushScientificButtonsProperty =
            DependencyProperty.Register("BorderBrushScientificButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BorderBrushScientificButtons
        {
            get { return (Brush)GetValue(BorderBrushScientificButtonsProperty); }
            set { SetValue(BorderBrushScientificButtonsProperty, value); }
        }

        #endregion

        #region BorderThicknessScientificButtons

        public static readonly DependencyProperty BorderThicknessScientificButtonsProperty =
            DependencyProperty.Register("BorderThicknessScientificButtons",
            typeof(double),
            typeof(Calculator_Control),
            new PropertyMetadata(5D));
        public double BorderThicknessScientificButtons
        {
            get { return (double)GetValue(BorderThicknessScientificButtonsProperty); }
            set { SetValue(BorderThicknessScientificButtonsProperty, value); }
        }

        #endregion

        #region BackgroundOperatorsButtons

        public static readonly DependencyProperty BackgroundOperatorsButtonsProperty =
            DependencyProperty.Register("BackgroundOperatorsButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BackgroundOperatorsButtons
        {
            get { return (Brush)GetValue(BackgroundOperatorsButtonsProperty); }
            set { SetValue(BackgroundOperatorsButtonsProperty, value); }
        }

        #endregion

        #region ForegroundOperatorsButtons

        public static readonly DependencyProperty ForegroundOperatorsButtonsProperty =
            DependencyProperty.Register("ForegroundOperatorsButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush ForegroundOperatorsButtons
        {
            get { return (Brush)GetValue(ForegroundOperatorsButtonsProperty); }
            set { SetValue(ForegroundOperatorsButtonsProperty, value); }
        }

        #endregion

        #region BorderBrushOperatorsButtons

        public static readonly DependencyProperty BorderBrushOperatorsButtonsProperty =
            DependencyProperty.Register("BorderBrushOperatorsButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BorderBrushOperatorsButtons
        {
            get { return (Brush)GetValue(BorderBrushOperatorsButtonsProperty); }
            set { SetValue(BorderBrushOperatorsButtonsProperty, value); }
        }

        #endregion

        #region BorderThicknessOperatorsButtons

        public static readonly DependencyProperty BorderThicknessOperatorsButtonsProperty =
            DependencyProperty.Register("BorderThicknessOperatorsButtons",
            typeof(double),
            typeof(Calculator_Control),
            new PropertyMetadata(5D));
        public double BorderThicknessOperatorsButtons
        {
            get { return (double)GetValue(BorderThicknessOperatorsButtonsProperty); }
            set { SetValue(BorderThicknessOperatorsButtonsProperty, value); }
        }

        #endregion

        #region BackgroundNumericalsButtons

        public static readonly DependencyProperty BackgroundNumericalsButtonsProperty =
            DependencyProperty.Register("BackgroundNumericalsButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BackgroundNumericalsButtons
        {
            get { return (Brush)GetValue(BackgroundNumericalsButtonsProperty); }
            set { SetValue(BackgroundNumericalsButtonsProperty, value); }
        }

        #endregion

        #region ForegroundNumericalsButtons

        public static readonly DependencyProperty ForegroundNumericalsButtonsProperty =
            DependencyProperty.Register("ForegroundNumericalsButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush ForegroundNumericalsButtons
        {
            get { return (Brush)GetValue(ForegroundNumericalsButtonsProperty); }
            set { SetValue(ForegroundNumericalsButtonsProperty, value); }
        }

        #endregion

        #region BorderBrushNumericalsButtons

        public static readonly DependencyProperty BorderBrushNumericalsButtonsProperty =
            DependencyProperty.Register("BorderBrushNumericalsButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BorderBrushNumericalsButtons
        {
            get { return (Brush)GetValue(BorderBrushNumericalsButtonsProperty); }
            set { SetValue(BorderBrushNumericalsButtonsProperty, value); }
        }

        #endregion

        #region BorderThicknessNumericalsButtons

        public static readonly DependencyProperty BorderThicknessNumericalsButtonsProperty =
            DependencyProperty.Register("BorderThicknessNumericalsButtons",
            typeof(double),
            typeof(Calculator_Control),
            new PropertyMetadata(5D));
        public double BorderThicknessNumericalsButtons
        {
            get { return (double)GetValue(BorderThicknessNumericalsButtonsProperty); }
            set { SetValue(BorderThicknessNumericalsButtonsProperty, value); }
        }

        #endregion

        #region BackgroundMemoryButtons

        public static readonly DependencyProperty BackgroundMemoryButtonsProperty =
            DependencyProperty.Register("BackgroundMemoryButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BackgroundMemoryButtons
        {
            get { return (Brush)GetValue(BackgroundMemoryButtonsProperty); }
            set { SetValue(BackgroundMemoryButtonsProperty, value); }
        }

        #endregion

        #region ForegroundMemoryButtons

        public static readonly DependencyProperty ForegroundMemoryButtonsProperty =
            DependencyProperty.Register("ForegroundMemoryButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush ForegroundMemoryButtons
        {
            get { return (Brush)GetValue(ForegroundMemoryButtonsProperty); }
            set { SetValue(ForegroundMemoryButtonsProperty, value); }
        }

        #endregion

        #region BorderBrushMemoryButtons

        public static readonly DependencyProperty BorderBrushMemoryButtonsProperty =
            DependencyProperty.Register("BorderBrushMemoryButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BorderBrushMemoryButtons
        {
            get { return (Brush)GetValue(BorderBrushMemoryButtonsProperty); }
            set { SetValue(BorderBrushMemoryButtonsProperty, value); }
        }

        #endregion

        #region BorderThicknessMemoryButtons

        public static readonly DependencyProperty BorderThicknessMemoryButtonsProperty =
            DependencyProperty.Register("BorderThicknessMemoryButtons",
            typeof(double),
            typeof(Calculator_Control),
            new PropertyMetadata(5D));
        public double BorderThicknessMemoryButtons
        {
            get { return (double)GetValue(BorderThicknessMemoryButtonsProperty); }
            set { SetValue(BorderThicknessMemoryButtonsProperty, value); }
        }

        #endregion

        #region BackgroundTrigonometryButtons

        public static readonly DependencyProperty BackgroundTrigonometryButtonsProperty =
            DependencyProperty.Register("BackgroundTrigonometryButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BackgroundTrigonometryButtons
        {
            get { return (Brush)GetValue(BackgroundTrigonometryButtonsProperty); }
            set { SetValue(BackgroundTrigonometryButtonsProperty, value); }
        }

        #endregion

        #region ForegroundTrigonometryButtons

        public static readonly DependencyProperty ForegroundTrigonometryButtonsProperty =
            DependencyProperty.Register("ForegroundTrigonometryButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush ForegroundTrigonometryButtons
        {
            get { return (Brush)GetValue(ForegroundTrigonometryButtonsProperty); }
            set { SetValue(ForegroundTrigonometryButtonsProperty, value); }
        }

        #endregion

        #region BorderBrushTrigonometryButtons

        public static readonly DependencyProperty BorderBrushTrigonometryButtonsProperty =
            DependencyProperty.Register("BorderBrushTrigonometryButtons",
            typeof(Brush),
            typeof(Calculator_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush BorderBrushTrigonometryButtons
        {
            get { return (Brush)GetValue(BorderBrushTrigonometryButtonsProperty); }
            set { SetValue(BorderBrushTrigonometryButtonsProperty, value); }
        }

        #endregion

        #region BorderThicknessTrigonometryButtons

        public static readonly DependencyProperty BorderThicknessTrigonometryButtonsProperty =
            DependencyProperty.Register("BorderThicknessTrigonometryButtons",
            typeof(double),
            typeof(Calculator_Control),
            new PropertyMetadata(5D));
        public double BorderThicknessTrigonometryButtons
        {
            get { return (double)GetValue(BorderThicknessTrigonometryButtonsProperty); }
            set { SetValue(BorderThicknessTrigonometryButtonsProperty, value); }
        }

        #endregion

        #endregion

        public static readonly DependencyProperty GetThemesListProperty =
    DependencyProperty.Register("GetThemesList",
                                typeof(List<ThemeObj>),
                                typeof(MenuSide_Control),
                                new PropertyMetadata(OnAvailableItemsChanged));
        public List<ThemeObj> GetThemesList
        {
            get { return (List<ThemeObj>)GetValue(GetThemesListProperty); }
            set { SetValue(GetThemesListProperty, value); }
        }

        public static void OnAvailableItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            mListTheme = (List<ThemeObj>)e.NewValue;
            mListThemeName = mListTheme.Select(p => p.ThemeName).Distinct().ToList();
        }

        public static readonly DependencyProperty CurrentModeProperty =
    DependencyProperty.Register("CurrentMode",
                                typeof(CalculatorMode),
                                typeof(MenuSide_Control),
                                new PropertyMetadata(CalculatorMode.Standard));
        public CalculatorMode CurrentMode
        {
            get { return (CalculatorMode)GetValue(CurrentModeProperty); }
            set { SetValue(CurrentModeProperty, value); }
        }

        public MenuSide_Control()
        {
            InitializeComponent();
            CurrentMode = CalculatorMode.Standard;
        }

        /// <summary>
        /// Adding the new item menu list to the MenuItem parent
        /// </summary>
        public bool SetMenuItems()
        {
            bool lIsOk = false;

            mListThemeName?.ForEach(pThemeName =>
            {
                MenuItem lMenuItem = new MenuItem();
                lMenuItem.Click += MenuTheme_Click;
                lMenuItem.Header = $"_{pThemeName}";
                MenuThemes.Items.Add(pThemeName);
                lIsOk = true;
            });

            return lIsOk;
        }

        private void MenuTheme_Click(object sender, RoutedEventArgs e)
        {
            SetTheme((e.OriginalSource as MenuItem).Header.ToString());
        }

        public void SetTheme(string pThemeName = "Default")
        {
            mListTheme?.Where(p => p.ThemeName == pThemeName).ToList().ForEach(p =>
            {
                switch (p.ParameterName)
                {
                    case "MainCalculatorBackground":
                        MainCalculatorBackground = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "MainCalculatorBorderBrush":
                        MainCalculatorBorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "MainCalculatorBorderThickness":
                        MainCalculatorBorderThickness = Double.Parse(p.ParameterValueStr, NumberStyles.Any, CultureInfo.InvariantCulture); break;
                    case "Background2ndeButton":
                        Background2ndeButton = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "Foreground2ndeButton":
                        Foreground2ndeButton = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "BorderBrush2ndeButton":
                        BorderBrush2ndeButton = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "BorderThickness2ndeButton":
                        BorderThickness2ndeButton = Double.Parse(p.ParameterValueStr, NumberStyles.Any, CultureInfo.InvariantCulture); break;
                    case "BackgroundBaseButtons":
                        BackgroundBaseButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "ForegroundBaseButtons":
                        ForegroundBaseButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "BorderBrushBaseButtons":
                        BorderBrushBaseButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "BorderThicknessBaseButtons":
                        BorderThicknessBaseButtons = Double.Parse(p.ParameterValueStr, NumberStyles.Any, CultureInfo.InvariantCulture); break;
                    case "BackgroundScientificButtons":
                        BackgroundScientificButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "ForegroundScientificButtons":
                        ForegroundScientificButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "BorderBrushScientificButtons":
                        BorderBrushScientificButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "BorderThicknessScientificButtons":
                        BorderThicknessScientificButtons = Double.Parse(p.ParameterValueStr, NumberStyles.Any, CultureInfo.InvariantCulture); break;
                    case "BackgroundOperatorsButtons":
                        BackgroundOperatorsButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "ForegroundOperatorsButtons":
                        ForegroundOperatorsButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "BorderBrushOperatorsButtons":
                        BorderBrushOperatorsButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "BorderThicknessOperatorsButtons":
                        BorderThicknessOperatorsButtons = Double.Parse(p.ParameterValueStr, NumberStyles.Any, CultureInfo.InvariantCulture); break;
                    case "BackgroundNumericalsButtons":
                        BackgroundNumericalsButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "ForegroundNumericalsButtons":
                        ForegroundNumericalsButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "BorderBrushNumericalsButtons":
                        BorderBrushNumericalsButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "BorderThicknessNumericalsButtons":
                        BorderThicknessNumericalsButtons = Double.Parse(p.ParameterValueStr, NumberStyles.Any, CultureInfo.InvariantCulture); break;
                    case "BackgroundMemoryButtons":
                        BackgroundMemoryButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "ForegroundMemoryButtons":
                        ForegroundMemoryButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "BorderBrushMemoryButtons":
                        BorderBrushMemoryButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "BorderThicknessMemoryButtons":
                        BorderThicknessMemoryButtons = Double.Parse(p.ParameterValueStr, NumberStyles.Any, CultureInfo.InvariantCulture); break;
                    case "BackgroundTrigonometryButtons":
                        BackgroundTrigonometryButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "ForegroundTrigonometryButtons":
                        ForegroundTrigonometryButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "BorderBrushTrigonometryButtons":
                        BorderBrushTrigonometryButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterValueStr); break;
                    case "BorderThicknessTrigonometryButtons":
                        BorderThicknessTrigonometryButtons = Double.Parse(p.ParameterValueStr, NumberStyles.Any, CultureInfo.InvariantCulture); break;
                }
            });
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string lItemName = (e.Source as MenuItem).Header.ToString();
            switch (lItemName)
            {
                case "_Standard":
                    CurrentMode = CalculatorMode.Standard;
                    break;
                case "_Scientific":
                    CurrentMode = CalculatorMode.Scientific;
                    break;
                case "_Binary":
                    CurrentMode = CalculatorMode.Binary;
                    break;
                case "_Graphs":
                    CurrentMode = CalculatorMode.Graphs;
                    break;
                case "_Calendar":
                    CurrentMode = CalculatorMode.Calendar;
                    break;
                case "_Area":
                    CurrentMode = CalculatorMode.Area;
                    break;
                case "_Electronic":
                    CurrentMode = CalculatorMode.Electronic;
                    break;
                case "_Energy":
                    CurrentMode = CalculatorMode.Energy;
                    break;
                case "_Flow":
                    CurrentMode = CalculatorMode.Flow;
                    break;
                case "_Force":
                    CurrentMode = CalculatorMode.Force;
                    break;
                case "_Length":
                    CurrentMode = CalculatorMode.Length;
                    break;
                case "_Power":
                    CurrentMode = CalculatorMode.Power;
                    break;
                case "_Pressure":
                    CurrentMode = CalculatorMode.Pressure;
                    break;
                case "_Speed":
                    CurrentMode = CalculatorMode.Speed;
                    break;
                case "_Temperature":
                    CurrentMode = CalculatorMode.Temperature;
                    break;
                case "_Time":
                    CurrentMode = CalculatorMode.Time;
                    break;
                case "_Volume":
                    CurrentMode = CalculatorMode.Volume;
                    break;
                case "_Weight":
                    CurrentMode = CalculatorMode.Weight;
                    break;
                case "_Data":
                    CurrentMode = CalculatorMode.Data;
                    break;
                case "_Angle":
                    CurrentMode = CalculatorMode.Angle;
                    break;
                case "_Currency":
                    CurrentMode = CalculatorMode.Currency;
                    break;
                case "_Options":
                    CurrentMode = CalculatorMode.Options;
                    break;
                case "_Exit":
                    Application.Current.Shutdown();
                    break;
                default:
                    CurrentMode = CalculatorMode.Standard;
                    break;
            }

            UIMenuSelected.Content = CurrentMode.ToString();
        }
    }
}
