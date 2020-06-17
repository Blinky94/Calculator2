using System.Windows;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void UpdateUIDisplayHandler(string pContent);
        public event UpdateUIDisplayHandler UIDisplayValueEvent;

        public MainWindow()
        {
            InitializeComponent();
            UIMenuSide.UIMenuSelected.Content = CalculatorMode.Standard.ToString();
            UIDisplay.UIDisplayCalculus.Content = "0";

            UIDisplayValueEvent += new UpdateUIDisplayHandler(ModifyUIDisplay);
            UICalculator.CalculusDisplayDelegate = UIDisplayValueEvent;
        }

        public void ModifyUIDisplay(string pContent)
        {
            UIDisplay.UIDisplayCalculus.Content = pContent;
        }
    }
}
