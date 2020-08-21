using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator
{
    public enum CalculatorMode { Standard, Scientific, Binary, Graphs, Calendar, Area, Electronic, Energy, Flow, Force, Length, Power, Pressure, Speed, Temperature, Time, Volume, Weight, Data, Angle, Currency, Options };

    /// <summary>
    /// Interaction logic for MenuSide.xaml
    /// </summary>
    public partial class MenuSide_Control : UserControl
    {
        private static List<string> mListThemeName;

        public static readonly DependencyProperty GetThemesList2Property =
    DependencyProperty.Register("GetThemesList",
                                typeof(List<ThemeElements>),
                                typeof(MenuSide_Control),
                                new PropertyMetadata(OnAvailableItemsChanged));
        public List<ThemeElements> SetThemesList
        {
            get { return (List<ThemeElements>)GetValue(GetThemesList2Property); }
            set { SetValue(GetThemesList2Property, value); }
        }

        public static void OnAvailableItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CommonTheme.CompleteListThemes = (List<ThemeElements>)e.NewValue;
            mListThemeName = CommonTheme.CompleteListThemes.Select(p => p.ParentThemeName).Distinct().ToList();
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

        ///// <summary>
        ///// Set a menu item
        ///// </summary>
        ///// <param name="pHeader"></param>
        ///// <param name="pHandler"></param>
        //private void SetMenuItem(string pHeader, RoutedEventHandler pHandler)
        //{
        //    MenuItem lMenuItem = new MenuItem();
        //    lMenuItem.Click += pHandler;
        //    lMenuItem.Header = pHeader;
        //    MenuThemes.Items.Add(lMenuItem);
        //}

        ///// <summary>
        ///// Set new items in the menuside
        ///// </summary>
        //public void SetMenuItems()
        //{
        //    //Set every itemsfrom xml file
        //    mListThemeName?.ForEach(pThemeName =>
        //    {
        //        SetMenuItem($"_{pThemeName}", MenuTheme_Click);
        //    });

        //    // Set the custom themes item
        //    SetMenuItem("_Custom...", CustomItem_Click);
        //}

        private void MenuTheme_Click(object sender, RoutedEventArgs e)
        {
            CommonTheme.ThemeSelectedName = (e.OriginalSource as MenuItem).Header.ToString();
            CommonTheme.SetThemesProperties();
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
                    // Open option window
                    OptionWindow.GetInstance().Show();
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
