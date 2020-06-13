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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private StandardCalculator mStandardCalcultator;
        private Calculator mStandardCalcultator;
        private CalculatorMode eCurrentMode;

        public MainWindow()
        {
            InitializeComponent();
            //mStandardCalcultator = new StandardCalculator();
            mStandardCalcultator = new Calculator();
            eCurrentMode = CalculatorMode.Standard;

            mStandardCalcultator.SetValue(Grid.RowProperty, 1);
            mStandardCalcultator.SetValue(Grid.ColumnProperty, 0);
            MainGrid.Children.Add(mStandardCalcultator);
            UICalculatorSelectionLabel.Content = eCurrentMode.ToString();
            UICalculatorSelectionLabel.HorizontalContentAlignment = HorizontalAlignment.Left;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string lItemName = (e.Source as MenuItem).Header.ToString();
            switch (lItemName)
            {
                case "_Standard":
                    eCurrentMode = CalculatorMode.Standard;
                    break;
                case "_Scientific":
                    eCurrentMode = CalculatorMode.Scientific;
                    break;
                case "_Binary":
                    eCurrentMode = CalculatorMode.Binary;
                    break;
                case "_Calendar":
                    eCurrentMode = CalculatorMode.Calendar;
                    break;
                case "_Area":
                    eCurrentMode = CalculatorMode.Area;
                    break;
                case "_Electronic":
                    eCurrentMode = CalculatorMode.Electronic;
                    break;
                case "_Energy":
                    eCurrentMode = CalculatorMode.Energy;
                    break;
                case "_Flow":
                    eCurrentMode = CalculatorMode.Flow;
                    break;
                case "_Force":
                    eCurrentMode = CalculatorMode.Force;
                    break;
                case "_Length":
                    eCurrentMode = CalculatorMode.Length;
                    break;
                case "_Power":
                    eCurrentMode = CalculatorMode.Power;
                    break;
                case "_Pressure":
                    eCurrentMode = CalculatorMode.Pressure;
                    break;
                case "_Speed":
                    eCurrentMode = CalculatorMode.Speed;
                    break;
                case "_Temperature":
                    eCurrentMode = CalculatorMode.Temperature;
                    break;
                case "_Time":
                    eCurrentMode = CalculatorMode.Time;
                    break;
                case "_Volume":
                    eCurrentMode = CalculatorMode.Volume;
                    break;
                case "_Weight":
                    eCurrentMode = CalculatorMode.Weight;
                    break;
                case "_Data":
                    eCurrentMode = CalculatorMode.Data;
                    break;
                case "_Angle":
                    eCurrentMode = CalculatorMode.Angle;
                    break;
                case "_Currency":
                    eCurrentMode = CalculatorMode.Currency;
                    break;
                case "_Options":
                    eCurrentMode = CalculatorMode.Options;
                    break;
                default:
                    eCurrentMode = CalculatorMode.Standard;
                    break;
            }

            UICalculatorSelectionLabel.Content = eCurrentMode.ToString();
        }
    }
}
