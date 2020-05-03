using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_Calculator
{
    public static class ExtendedStreamBuilder
    {
        /// <summary>
        /// Check if Sb contains specific string
        /// </summary>
        /// <param name="pSb"></param>
        /// <param name="pTab"></param>
        /// <returns></returns>
        public static bool ContainsAny(this StringBuilder pSb, char[] pTab)
        {
            bool lIsSbContains = false;

            for (int i = 0; i < pTab.Length; i++)
            {
                if (pSb.ToString().Contains(pTab[i]))
                {
                    lIsSbContains = true;
                }
            }

            return lIsSbContains;
        }

        /// <summary>
        /// Check if Sb contains specific char
        /// </summary>
        /// <param name="pSb"></param>
        /// <param name="pTab"></param>
        /// <returns></returns>
        public static bool Contains(this StringBuilder pSb, char pChar)
        {
            return pSb.ToString().Contains(pChar);
        }

        /// <summary>
        /// Get the first occurence index of a char in the Sb
        /// </summary>
        /// <param name="pSb"></param>
        /// <param name="pChar"></param>
        /// <returns></returns>
        public static int IndexOf(this StringBuilder pSb, char pChar)
        {
            return pSb.ToString().IndexOf(pChar);
        }

        /// <summary>
        /// Get all the occurence index of a char in the Sb
        /// </summary>
        /// <param name="pSb"></param>
        /// <param name="pChar"></param>
        /// <returns></returns>
        public static int[] IndexOfAny(this StringBuilder pSb, char pChar)
        {
            List<int> lList = new List<int>();

            for (int i = 0; i < pSb.Length; i++)
            {
                if (pSb[i] == pChar)
                {
                    lList.Add(i);
                }
            }

            return lList.ToArray();
        }

        /// <summary>
        /// Get specific chunk of a formula from start index and length, in the current Sb
        /// </summary>
        /// <param name="pSb"></param>
        /// <param name="pStartIndex"></param>
        /// <param name="pLength"></param>
        /// <returns></returns>
        public static StringBuilder GetChunk(this StringBuilder pSb, int pStartIndex, int pLength)
        {
            pLength++;
            char[] mNewStr = new char[pLength];
            pSb.CopyTo(pStartIndex, mNewStr, 0, pLength);
            string lval = new string(mNewStr);

            return pSb.Replace(pSb.ToString(), lval);
        }
    }
}
