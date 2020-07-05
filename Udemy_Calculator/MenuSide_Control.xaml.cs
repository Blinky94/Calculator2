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

        public static readonly DependencyProperty GetThemesList2Property =
    DependencyProperty.Register("GetThemesList",
                                typeof(List<ThemeObj>),
                                typeof(MenuSide_Control),
                                new PropertyMetadata(OnAvailableItemsChanged));
        public List<ThemeObj> GetThemesList
        {
            get { return (List<ThemeObj>)GetValue(GetThemesList2Property); }
            set { SetValue(GetThemesList2Property, value); }
        }

        public static void OnAvailableItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            mListTheme = (List<ThemeObj>)e.NewValue;
            mListThemeName = mListTheme.Select(p => p.ParentThemeName).Distinct().ToList();
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

            MenuItem llCustomItem = new MenuItem();
            llCustomItem.Click += CustomItem_Click;
            llCustomItem.Header = "_Custom...";
            MenuThemes.Items.Add(llCustomItem);

            return lIsOk;
        }

        CustomWindow mUICustomWindow;
        private void CustomItem_Click(object sender, RoutedEventArgs e)
        {
            mUICustomWindow = new CustomWindow();
            mUICustomWindow.AddRadioButtonsList(mListTheme);
            mUICustomWindow.UIThemeName.Content = mThemeSelected;
            mUICustomWindow.Show();
        }

        private void MenuTheme_Click(object sender, RoutedEventArgs e)
        {
            GetThemes((e.OriginalSource as MenuItem).Header.ToString());
        }

        private string mThemeSelected;
        public void GetThemes(string pThemeName = "Default")
        {
            mListTheme?.Where(p => p.ParentThemeName == pThemeName).ToList().ForEach(p =>
            {
                mThemeSelected = pThemeName;
                switch (p.ParameterName)
                {
                    case "MainCalculatorBackground":
                        ((MainWindow)Application.Current.MainWindow).MainCalculator.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "MainCalculatorBorderBrush":
                        ((MainWindow)Application.Current.MainWindow).MainCalculator.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "MainCalculatorBorderThickness":
                        ((MainWindow)Application.Current.MainWindow).MainCalculator.BorderThickness = new Thickness(Double.Parse(p.ParameterStringValue, NumberStyles.Any, CultureInfo.InvariantCulture)); break;
                    case "Background2ndeButton":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.UISecondFuncButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "Foreground2ndeButton":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.UISecondFuncButton.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "BorderBrush2ndeButton":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.UISecondFuncButton.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "BorderThickness2ndeButton":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.UISecondFuncButton.BorderThickness = new Thickness(Double.Parse(p.ParameterStringValue, NumberStyles.Any, CultureInfo.InvariantCulture)); break;
                    case "BackgroundBaseButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.BackgroundBaseButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "ForegroundBaseButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.ForegroundBaseButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "BorderBrushBaseButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.BorderBrushBaseButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "BorderThicknessBaseButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.BorderThicknessBaseButtons = Double.Parse(p.ParameterStringValue, NumberStyles.Any, CultureInfo.InvariantCulture); break;
                    case "BackgroundScientificButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.BackgroundScientificButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "ForegroundScientificButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.ForegroundScientificButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "BorderBrushScientificButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.BorderBrushScientificButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "BorderThicknessScientificButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.BorderThicknessScientificButtons = Double.Parse(p.ParameterStringValue, NumberStyles.Any, CultureInfo.InvariantCulture); break;
                    case "BackgroundOperatorsButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.BackgroundOperatorsButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "ForegroundOperatorsButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.ForegroundOperatorsButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "BorderBrushOperatorsButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.BorderBrushOperatorsButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "BorderThicknessOperatorsButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.BorderThicknessOperatorsButtons = Double.Parse(p.ParameterStringValue, NumberStyles.Any, CultureInfo.InvariantCulture); break;
                    case "BackgroundNumericalsButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.BackgroundNumericalsButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "ForegroundNumericalsButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.ForegroundNumericalsButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "BorderBrushNumericalsButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.BorderBrushNumericalsButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "BorderThicknessNumericalsButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.BorderThicknessNumericalsButtons = Double.Parse(p.ParameterStringValue, NumberStyles.Any, CultureInfo.InvariantCulture); break;
                    case "BackgroundMemoryButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.BackgroundMemoryButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "ForegroundMemoryButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.ForegroundMemoryButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "BorderBrushMemoryButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.BorderBrushMemoryButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "BorderThicknessMemoryButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.BorderThicknessMemoryButtons = Double.Parse(p.ParameterStringValue, NumberStyles.Any, CultureInfo.InvariantCulture); break;
                    case "BackgroundTrigonometryButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.BackgroundTrigonometryButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "ForegroundTrigonometryButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.ForegroundTrigonometryButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "BorderBrushTrigonometryButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.BorderBrushTrigonometryButtons = (SolidColorBrush)new BrushConverter().ConvertFromString(p.ParameterStringValue); break;
                    case "BorderThicknessTrigonometryButtons":
                        ((MainWindow)Application.Current.MainWindow).UICalculator.BorderThicknessTrigonometryButtons = Double.Parse(p.ParameterStringValue, NumberStyles.Any, CultureInfo.InvariantCulture); break;
                }
            });

            ((MainWindow)Application.Current.MainWindow).UICalculator.InitializeComponent();
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
