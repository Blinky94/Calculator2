using System.Windows;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            menuSide.UIMenuSelected.Content = CalculatorMode.Standard.ToString();
        }
    }
}
