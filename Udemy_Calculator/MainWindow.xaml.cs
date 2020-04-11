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

        private string mFormula = string.Empty;
        private string mOperand = "+-*/";
        private string mNumbers = "123456789";

        private void CleanFormula()
        {
            mFormula = "0";
            formulaLabel.Content = mFormula;
        }

        private void FormulaLabelDisplay()
        {
            formulaLabel.Content = mFormula.Replace(',', '.');
            if(mFormula.FirstOrDefault() == '0' && mFormula[1] != '.')
            {
                formulaLabel.Content = mFormula.Remove(0, 1);
            }
        }

        private void MakeFormula(string pNum)
        {
            // Règle 1 si mFormula == 0 et pNum = {1-9} => remplacer le 0 par le chiffre
            if(mFormula == "0" && mNumbers.Contains(pNum))
                mFormula = pNum;
            // Règle 2 si Formula == 0 et pNUm = . et si pas d'autre . => laisser le 0 et ajouter le .
            else if (mFormula == "0" && pNum == "." && !mFormula.Contains("."))
                mFormula = $"{mFormula}{pNum}";
            // Règle 3 si Formula == 0 et pNum = *+-/ => laisser le 0 et ajouter le symbole
            else if (mFormula == "0" && mOperand.Contains(pNum))
                mFormula = $"{mFormula}{pNum}";
            // Règle 4 si mFormula != 0 ajouter le chiffre
            else
                mFormula = $"{mFormula}{pNum}";
        }

        private void ResultLabelDisplay(string pNum)
        {
            resultLabel.Content = (resultLabel.Content.ToString() == "0" && pNum != ".")
                || mOperand.Contains(mFormula.LastOrDefault()) ? resultLabel.Content = pNum : resultLabel.Content += pNum;

            resultLabel.Content = resultLabel.Content.ToString().Replace(',', '.');

        }

        private void ComputeNumber(string pNum)
        {
            ResultLabelDisplay(pNum);
            MakeFormula(pNum);
            FormulaLabelDisplay();
        }

        private void numberButton_Click(object sender, RoutedEventArgs e)
        {
            string lNumber = (e.Source as Button).Content.ToString();
            ComputeNumber(lNumber);
        }

        private void pointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLabel.Content.ToString().Contains('.'))
            {
                ComputeNumber(".");
            }
        }

        private void aCButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            CleanFormula();
        }

        private bool StringToDouble(string pContent, out double pNum)
        {
            //pContent = pContent.Replace(',', '.');
            bool lConverted = double.TryParse(pContent, NumberStyles.Number, CultureInfo.InvariantCulture, out pNum);
            return lConverted;
        }

        private void RemoveLastNum(string pNum, Action<string> pAction)
        {
            pAction(pNum);
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
            double lNum;
            resultLabel.Content = resultLabel.Content.ToString().Replace(',', '.');
            if (StringToDouble(resultLabel.Content.ToString(), out lNum))
            {
                RemoveLastNum(lNum.ToString(), (string pNumStr) =>
                {
                    mFormula = mFormula.Remove(mFormula.Length - pNumStr.Length);
                });

                RemoveLastNum(lNum.ToString(), (string pNumStr) =>
                {
                    resultLabel.Content = resultLabel.Content.ToString().Remove(resultLabel.Content.ToString().Length - pNumStr.Length);
                });

                lNum *= lTransformCoef;
                string lResultStr = lNum.ToString().Replace(',', '.');
                ComputeNumber(lResultStr);
            }
        }

        private void ComputeOperator(string pOp)
        {
            string lResultLabel = resultLabel.Content.ToString();

            if (lResultLabel != string.Empty || !mOperand.Contains(lResultLabel.Last()) || !mOperand.Contains(mFormula.Last()))
            {
                MakeFormula(pOp);
            }
        }

        private void operatorButton_Click(object sender, RoutedEventArgs e)
        {
            string lOperator = (e.Source as Button).Content.ToString();
            lOperator = lOperator == "X" ? "*" : lOperator;
            ComputeOperator(lOperator);
        }

        private void equalButton_Click(object sender, RoutedEventArgs e)
        {
            if (mFormula != string.Empty && !mOperand.Contains(mFormula.Last()))
            {
                resultLabel.Content = EvaluateExpression();
                CleanFormula();
                string lResultStr = resultLabel.Content.ToString().Replace(',', '.');
                MakeFormula(lResultStr);
                FormulaLabelDisplay();
            }
        }

        private double EvaluateExpression()
        {
            return Convert.ToDouble(new DataTable().Compute(mFormula.Replace(',', '.'), null));
        }
    }
}
