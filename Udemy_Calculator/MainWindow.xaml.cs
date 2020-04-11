using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
        }

        private double mLastNumber;
        private SelectedOperator mSelectedOperator;

        private void numberButton_Click(object sender, RoutedEventArgs e)
        {
            string lNumber = (e.Source as Button).Content.ToString();
            resultLabel.Content = lNumber;
        }

        private void pointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLabel.Content.ToString().Contains('.'))
            {

            }
        }

        private void aCButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
        }

        private double TransformCoef(string pButtonPressed)
        {
            switch (pButtonPressed)
            {
                case "+/1":
                    return -1;

                case "%":
                    return 0.01;
            }

            return default;
        }

        private void transformNumberButton_Click(object sender, RoutedEventArgs e)
        {
            double lTransformCoef = TransformCoef((e.Source as Button).Content.ToString());
            double lResult;

            if(double.TryParse(resultLabel.Content.ToString(), out lResult))
            {
                resultLabel.Content = lResult * lTransformCoef;
            }
        }

        private void operatorButton_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(resultLabel.Content.ToString(), out mLastNumber))
            {
                resultLabel.Content = (e.Source as Button).Content.ToString();
            }

            if(sender == plusButton)
            {
                mSelectedOperator = SelectedOperator.Addition;
            }
            else if(sender == minusButton)
            {
                mSelectedOperator = SelectedOperator.Substraction;
            }
            else if(sender == multiplyButton)
            {
                mSelectedOperator = SelectedOperator.Multiplication;
            }
            else if(sender == divideButton)
            {
                mSelectedOperator = SelectedOperator.Division;
            }          
        }

        private void equalButton_Click(object sender, RoutedEventArgs e)
        {
            double lNewNumber;
            if(double.TryParse(resultLabel.Content.ToString(), out lNewNumber))
            {
                switch (mSelectedOperator)
                {
                    case SelectedOperator.Addition:
                       resultLabel.Content = SimpleMath.Add(mLastNumber, lNewNumber);
                        break;
                    case SelectedOperator.Substraction:
                        resultLabel.Content = SimpleMath.Sub(mLastNumber, lNewNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        resultLabel.Content = SimpleMath.Mul(mLastNumber, lNewNumber);
                        break;
                    case SelectedOperator.Division:
                        resultLabel.Content = SimpleMath.Div(mLastNumber, lNewNumber);
                        break;
                }
            }
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Substraction,
        Multiplication,
        Division
    }

    public static class SimpleMath
    {
        public static double Add(double p1, double p2)
        {
            return p1 + p2;
        }

        public static double Sub(double p1, double p2)
        {
            return p1 - p2;
        }

        public static double Mul(double p1, double p2)
        {
            return p1 * p2;
        }

        public static double Div(double p1, double p2)
        {
            return p1 / p2;
        }
    }
}
