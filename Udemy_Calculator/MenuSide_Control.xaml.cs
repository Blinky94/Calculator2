using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Udemy_Calculator
{
    public enum CalculatorMode
    {
        /// <summary>
        /// Main modes
        /// </summary>
        Standard,
        Scientific,
        Binary,
        Graphs,
        Calendar,

        /// <summary>
        /// Conversions modes
        /// </summary>
        Area,
        Electronic,
        Energy,
        Flow,
        Force,
        Length,
        Power,
        Pressure,
        Speed,
        Temperature,
        Time,
        Volume,
        Weight,
        Data,
        Angle,
        Currency,

        /// <summary>
        /// Option mode for paraméters
        /// </summary>
        Options
    };

    /// <summary>
    /// Interaction logic for MenuSide.xaml
    /// </summary>
    public partial class MenuSide_Control : UserControl
    {
        #region MainCalculatorBackground

        public static readonly DependencyProperty MainCalculatorBackgroundProperty =
   DependencyProperty.Register("MainCalculatorBackground",
                               typeof(string),
                               typeof(MenuSide_Control),
                               new PropertyMetadata(""));
        public string MainCalculatorBackground
        {
            get { return (string)GetValue(MainCalculatorBackgroundProperty); }
            set { SetValue(MainCalculatorBackgroundProperty, value); }
        }

        #endregion

        #region MainCalculatorBorderBrush

        public static readonly DependencyProperty MainCalculatorBorderBrushProperty =
   DependencyProperty.Register("MainCalculatorBorderBrush",
                               typeof(string),
                               typeof(MenuSide_Control),
                               new PropertyMetadata(""));
        public string MainCalculatorBorderBrush
        {
            get { return (string)GetValue(MainCalculatorBorderBrushProperty); }
            set { SetValue(MainCalculatorBorderBrushProperty, value); }
        }

        #endregion

        #region BackGroundOthersButtons

        public static readonly DependencyProperty BackGroundOthersButtonsProperty =
DependencyProperty.Register("BackGroundOthersButtons",
                       typeof(string),
                       typeof(MenuSide_Control),
                       new PropertyMetadata(""));
        public string BackGroundOthersButtons
        {
            get { return (string)GetValue(BackGroundOthersButtonsProperty); }
            set { SetValue(BackGroundOthersButtonsProperty, value); }
        }

        #endregion

        #region ForegroundOthersButtons

        public static readonly DependencyProperty ForegroundOthersButtonsProperty =
DependencyProperty.Register("ForegroundOthersButtons",
               typeof(string),
               typeof(MenuSide_Control),
               new PropertyMetadata(""));
        public string ForegroundOthersButtons
        {
            get { return (string)GetValue(ForegroundOthersButtonsProperty); }
            set { SetValue(ForegroundOthersButtonsProperty, value); }
        }

        #endregion

        #region BackgroundOperatorsButtons

        public static readonly DependencyProperty BackgroundOperatorsButtonsProperty =
DependencyProperty.Register("BackgroundOperatorsButtons",
       typeof(string),
       typeof(MenuSide_Control),
       new PropertyMetadata(""));
        public string BackgroundOperatorsButtons
        {
            get { return (string)GetValue(BackgroundOperatorsButtonsProperty); }
            set { SetValue(BackgroundOperatorsButtonsProperty, value); }
        }

        #endregion

        #region ForegroundOperatorsButtons

        public static readonly DependencyProperty ForegroundOperatorsButtonsProperty =
DependencyProperty.Register("ForegroundOperatorsButtons",
typeof(string),
typeof(MenuSide_Control),
new PropertyMetadata(""));
        public string ForegroundOperatorsButtons
        {
            get { return (string)GetValue(ForegroundOperatorsButtonsProperty); }
            set { SetValue(ForegroundOperatorsButtonsProperty, value); }
        }

        #endregion

        #region BackgroundNumericalsButtons

        public static readonly DependencyProperty BackgroundNumericalsButtonsProperty =
DependencyProperty.Register("BackgroundNumericalsButtons",
typeof(string),
typeof(MenuSide_Control),
new PropertyMetadata(""));
        public string BackgroundNumericalsButtons
        {
            get { return (string)GetValue(BackgroundNumericalsButtonsProperty); }
            set { SetValue(BackgroundNumericalsButtonsProperty, value); }
        }

        #endregion

        #region ForegroundNumericalsButtons

        public static readonly DependencyProperty ForegroundNumericalsButtonsProperty =
DependencyProperty.Register("ForegroundNumericalsButtons",
typeof(string),
typeof(MenuSide_Control),
new PropertyMetadata(""));
        public string ForegroundNumericalsButtons
        {
            get { return (string)GetValue(ForegroundNumericalsButtonsProperty); }
            set { SetValue(ForegroundNumericalsButtonsProperty, value); }
        }

        #endregion

        #region BackgroundMemoryButtons

        public static readonly DependencyProperty BackgroundMemoryButtonsProperty =
DependencyProperty.Register("BackgroundMemoryButtons",
typeof(string),
typeof(MenuSide_Control),
new PropertyMetadata(""));
        public string BackgroundMemoryButtons
        {
            get { return (string)GetValue(BackgroundMemoryButtonsProperty); }
            set { SetValue(BackgroundMemoryButtonsProperty, value); }
        }

        #endregion

        #region ForegroundMemoryButtons

        public static readonly DependencyProperty ForegroundMemoryButtonsProperty =
DependencyProperty.Register("ForegroundMemoryButtons",
typeof(string),
typeof(MenuSide_Control),
new PropertyMetadata(""));
        public string ForegroundMemoryButtons
        {
            get { return (string)GetValue(ForegroundMemoryButtonsProperty); }
            set { SetValue(ForegroundMemoryButtonsProperty, value); }
        }

        #endregion

        #region BackgroundTrigonometryButtons

        public static readonly DependencyProperty BackgroundTrigonometryButtonsProperty =
DependencyProperty.Register("BackgroundTrigonometryButtons",
typeof(string),
typeof(MenuSide_Control),
new PropertyMetadata(""));
        public string BackgroundTrigonometryButtons
        {
            get { return (string)GetValue(BackgroundTrigonometryButtonsProperty); }
            set { SetValue(BackgroundTrigonometryButtonsProperty, value); }
        }

        #endregion

        #region ForegroundTrigonometryButtons

        public static readonly DependencyProperty ForegroundTrigonometryButtonsProperty =
DependencyProperty.Register("ForegroundTrigonometryButtons",
typeof(string),
typeof(MenuSide_Control),
new PropertyMetadata(""));
        public string ForegroundTrigonometryButtons
        {
            get { return (string)GetValue(ForegroundTrigonometryButtonsProperty); }
            set { SetValue(ForegroundTrigonometryButtonsProperty, value); }
        }

        #endregion

        private static List<ThemeObj> mListTheme;

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

        ///Set menuItem
        private MenuItem SetMenuItem(ItemCollection pItem, string pName, string pReverseStr, bool pNoIcon = false)
        {
            MenuItem lMenuItem = new MenuItem();
            lMenuItem.Click += MenuItem_Click;
            lMenuItem.Header = $"_{pName}";

            // Reverse text color ?
            if (pReverseStr == "")
            {
                lMenuItem.Foreground = new SolidColorBrush(Colors.Black);
                UIMenuSelected.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                lMenuItem.Foreground = new SolidColorBrush(Colors.White);
                UIMenuSelected.Foreground = new SolidColorBrush(Colors.White);
            }

            // Set Menuside background and menuItemBackground as the same color
            MenuSideControl.Background = (Brush)new BrushConverter().ConvertFrom(mMainBgColor);
            lMenuItem.Background = (Brush)new BrushConverter().ConvertFrom(mMainBgColor);

            if (!pNoIcon)
            {
                pName = pName.Replace(".", "");
                lMenuItem.Icon = new Image { Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/Images/" + $"{pName}_icon{pReverseStr}.png", UriKind.Absolute)) };
            }

            pItem.Add(lMenuItem);

            return lMenuItem;
        }

        ///Set reversed colored icons or normal colored icons (and foreground titles)
        private void MenuItem(string pReverseStr = "")
        {
            MenuSideControlItem.Header = new Image { Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/Images/" + $"Menu_icon{pReverseStr}.png", UriKind.Absolute)) };

            SetMenuItem(MenuSideControlItem.Items, "Standard", pReverseStr);
            SetMenuItem(MenuSideControlItem.Items, "Scientific", pReverseStr);
            SetMenuItem(MenuSideControlItem.Items, "Binary", pReverseStr);
            SetMenuItem(MenuSideControlItem.Items, "Graphs", pReverseStr);
            SetMenuItem(MenuSideControlItem.Items, "Calendar", pReverseStr);
            var lMenuItemConverter = SetMenuItem(MenuSideControlItem.Items, "Converter...", pReverseStr);
            SetMenuItem(lMenuItemConverter.Items, "Area", pReverseStr);
            SetMenuItem(lMenuItemConverter.Items, "Electronic", pReverseStr);
            SetMenuItem(lMenuItemConverter.Items, "Energy", pReverseStr);
            SetMenuItem(lMenuItemConverter.Items, "Flow", pReverseStr);
            SetMenuItem(lMenuItemConverter.Items, "Force", pReverseStr);
            SetMenuItem(lMenuItemConverter.Items, "Length", pReverseStr);
            SetMenuItem(lMenuItemConverter.Items, "Power", pReverseStr);
            SetMenuItem(lMenuItemConverter.Items, "Pressure", pReverseStr);
            SetMenuItem(lMenuItemConverter.Items, "Speed", pReverseStr);
            SetMenuItem(lMenuItemConverter.Items, "Temperature", pReverseStr);
            SetMenuItem(lMenuItemConverter.Items, "Time", pReverseStr);
            SetMenuItem(lMenuItemConverter.Items, "Volume", pReverseStr);
            SetMenuItem(lMenuItemConverter.Items, "Weight", pReverseStr);
            SetMenuItem(lMenuItemConverter.Items, "Data", pReverseStr);
            SetMenuItem(lMenuItemConverter.Items, "Angle", pReverseStr);
            SetMenuItem(lMenuItemConverter.Items, "Currency", pReverseStr);
            var lOptions = SetMenuItem(MenuSideControlItem.Items, "Options", pReverseStr);
            var lThemes = SetMenuItem(lOptions.Items, "Themes", pReverseStr);

            // Get generated themes list
            var lList = mListTheme?.Select(p => p.ThemeName).Distinct().ToList();
            lList.ForEach(pThemeName => { SetMenuItem(lThemes.Items, pThemeName, pReverseStr, true); });
        }


        private string mMainBgColor;

        ///Main method to set the Menu side with corresponding Icons (reversed color if pIsReverse is true)
        public void SetMenuIcon(string pMainBgColor)
        {
            mMainBgColor = pMainBgColor;

            if (BackgroundNeedReversedColor())
            {
                MenuItem("_reverse");
            }
            else
            {
                MenuItem();
            }
        }

        ///Determine if the current background color need reversed foreground
        private bool BackgroundNeedReversedColor()
        {
            var lBackgroundColor = (Brush)new BrushConverter().ConvertFrom(mMainBgColor);

            SolidColorBrush lNewBrush = (SolidColorBrush)lBackgroundColor;

            var r = (int)lNewBrush.Color.R;
            var g = (int)lNewBrush.Color.G;
            var b = (int)lNewBrush.Color.B;

            //dark < 382 < light
            return r + g + b < 382;
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
                default:
                    CurrentMode = CalculatorMode.Standard;
                    break;
            }

            UIMenuSelected.Content = CurrentMode.ToString();

            SetTheme(lItemName);
        }

        public void SetTheme(string pThemeName = "Default")
        {
            List<ThemeObj> lList = new List<ThemeObj>();

            foreach (var lTheme in mListTheme)
            {
                if (lTheme.ThemeName == pThemeName)
                {
                    lList.Add(new ThemeObj() { ParameterName = lTheme.ParameterName, ParameterValue = lTheme.ParameterValue });
                }
            }

            foreach (var lItem in lList)
            {
                switch (lItem.ParameterName)
                {
                    case "MainCalculatorBackground":
                        MainCalculatorBackground = lItem.ParameterValue; break;
                    case "MainCalculatorBorderBrush":
                        MainCalculatorBorderBrush = lItem.ParameterValue; break;
                    case "BackGroundOthersButtons":
                        BackGroundOthersButtons = lItem.ParameterValue; break;
                    case "ForegroundOthersButtons":
                        ForegroundOthersButtons = lItem.ParameterValue; break;
                    case "BackgroundOperatorsButtons":
                        BackgroundOperatorsButtons = lItem.ParameterValue; break;
                    case "ForegroundOperatorsButtons":
                        ForegroundOperatorsButtons = lItem.ParameterValue; break;
                    case "BackgroundNumericalsButtons":
                        BackgroundNumericalsButtons = lItem.ParameterValue; break;
                    case "ForegroundNumericalsButtons":
                        ForegroundNumericalsButtons = lItem.ParameterValue; break;
                    case "BackgroundMemoryButtons":
                        BackgroundMemoryButtons = lItem.ParameterValue; break;
                    case "ForegroundMemoryButtons":
                        ForegroundMemoryButtons = lItem.ParameterValue; break;
                    case "BackgroundTrigonometryButtons":
                        BackgroundTrigonometryButtons = lItem.ParameterValue; break;
                    case "ForegroundTrigonometryButtons":
                        ForegroundTrigonometryButtons = lItem.ParameterValue; break;
                }
            }
        }
    }
}
