using System;
using System.Windows;
using System.Windows.Controls;

namespace Udemy_Calculator.CalculatorControls
{
    /// <summary>
    /// Interaction logic for MemoryButtonsPanelControl.xaml
    /// </summary>
    public partial class MemoryButtonsPanelControl : UserControl
    {
        public MemoryButtonsPanelControl()
        {
            InitializeComponent();

            UIMCButton.OnMemoryButtonClicked += new RoutedEventHandler(UIMCButton_Click);
            UIMemoryPlusButton.OnMemoryButtonClicked += new RoutedEventHandler(UIMemoryPlusButton_Click);
            UIMemoryRecallButton.OnMemoryButtonClicked += new RoutedEventHandler(UIMemoryRecallButton_Click);
        }

        private void UIMemoryRecallButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UIMemoryPlusButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UIMCButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
