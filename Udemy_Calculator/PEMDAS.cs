using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_Calculator
{
    public class PEMDAS
    {

        private string mFormula;

        public PEMDAS(string pFormula)
        {
            mFormula = pFormula;
        }

        private string ComputeFormula()
        {
            string lResult = string.Empty;

            StringBuilder lSb = new StringBuilder(mFormula);
            
            

            return lResult;
        }

        #region Parenthesis

        /// <summary>
        /// Counting the number of '(' and ')' to see if there are equivalent number
        /// </summary>
        /// <param name="pFormula"></param>
        /// <returns>if it is true</returns>
        public bool AreParenthesisAreEquivalentNumber()
        {
            int lOpenedParenthesis = mFormula.Count(c => c == '(');
            int lClosedParenthesis = mFormula.Count(c => c == ')');

            return lOpenedParenthesis == lClosedParenthesis;
        }

        #endregion

        #region Exponent



        #endregion

        #region Multiplication



        #endregion

        #region Division



        #endregion

        #region Addition



        #endregion

        #region Substraction



        #endregion
    }
}
