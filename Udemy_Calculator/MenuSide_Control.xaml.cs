using System;
using System.Windows;
using System.Windows.Controls;

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

            bool lIsReverse = BackgroundNeedReversedColor();
            SetMenuIcon(lIsReverse);
        }

        ///Set menuItem
        private MenuItem SetMenuItem(ItemCollection pItem, string pName, string pReverseStr)
        {
            MenuItem lMenuItem = new MenuItem();
            lMenuItem.Click += MenuItem_Click;
            lMenuItem.Header = $"_{pName}";

            // Reverse text color ?
            if (pReverseStr == "")
            {
                lMenuItem.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
                UIMenuSelected.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
            }
            else
            {
                lMenuItem.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
                UIMenuSelected.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            }

            pName = pName.Replace(".", "");
            lMenuItem.Icon = new Image { Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/Images/" + $"{pName}_icon{pReverseStr}.png", UriKind.Absolute)) };

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
            SetMenuItem(MenuSideControlItem.Items, "Options", pReverseStr);
        }

        ///Set Menu Icon (reversed color if pIsReverse is true
        private void SetMenuIcon(bool pIsReverse)
        {
            if (pIsReverse)
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
            var lBackgroundColor = MenuSideControlItem.Background;

            System.Windows.Media.SolidColorBrush lNewBrush = (System.Windows.Media.SolidColorBrush)lBackgroundColor;

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
        }
    }
}
