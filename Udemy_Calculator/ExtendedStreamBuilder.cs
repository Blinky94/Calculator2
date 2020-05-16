using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Udemy_Calculator
{

    //    Regex               Meaning
    //      .       Matches any single character.
    //      ?       Matches the preceding element once or not at all.
    //      +   	Matches the preceding element once or more times.
    //      *   	Matches the preceding element zero or more times.
    //      ^   	Matches the starting position within the string.
    //      $   	Matches the ending position within the string.
    //      |   	Alternation operator.
    //    [abc]     Matches a or b, or c.
    //    [a-c]     Range; matches a or b, or c.
    //    [^abc]    Negation, matches everything except a, or b, or c.
    //      \s      Matches white space character.
    //      \w      Matches a word character; equivalent to[a - zA - Z_0 - 9]
    //      \b      Is an anchor which matches at a position that is called a word boundary. It allows to search for whole words.
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
        public static int[] IndexOfAnyChar(this StringBuilder pSb, char pChar)
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
        /// Get all the occurence index of a string in the Sb
        /// </summary>
        /// <param name="pSb"></param>
        /// <param name="pChar"></param>
        /// <returns></returns>
        public static int[] IndexOfAnyString(this StringBuilder pSb, String pStr)
        {
            List<int> lList = new List<int>();

            StringBuilder lStr = pSb;

            while (lStr.ContainsAny(pStr.ToCharArray()))
            {
                int lIndex = lStr.ToString().IndexOf(pStr);

                if (lIndex != -1)
                {
                    lList.Add(lIndex);
                    lStr.Replace(pStr, new string('#', pStr.Length), lIndex, pStr.Length);
                }
                else
                {
                    break;
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
            char[] mNewStr = new char[pLength];
            pSb.CopyTo(pStartIndex, mNewStr, 0, pLength);
            string lval = new string(mNewStr);

            return pSb.Replace(pSb.ToString(), lval);
        }

        /// <summary>
        /// Count the number of occurences contains in the current SB with each char in the array parameter
        /// Return the number of occurences
        /// </summary>
        /// <param name="pSb"></param>
        /// <param name="pChArray"></param>
        /// <returns></returns>
        public static int CountChar(this StringBuilder pSb, char[] pChArray)
        {
            int lCount = 0;

            foreach (var lChar in pChArray)
            {
                lCount += pSb.CountChar(lChar);
            }

            return lCount;
        }

        /// <summary>
        /// Count the number of occurences contains in the current SB with specific char in parameter
        /// Return the number of occurences
        /// </summary>
        /// <param name="pSb"></param>
        /// <param name="pCh"></param>
        /// <returns></returns>
        public static int CountChar(this StringBuilder pSb, char pCh)
        {
            int lCount = 0;

            foreach (var lChar in pSb.ToString())
            {
                if (lChar == pCh)
                {
                    lCount++;
                }
            }

            return lCount;
        }

        /// <summary>
        /// Count the number of occurences contains in the current SB with specific string in parameter
        /// Return the number of occurences
        /// </summary>
        /// <param name="pSb"></param>
        /// <param name="pCh"></param>
        /// <returns></returns>
        public static int CountOccurences(this StringBuilder pSb, string pStr)
        {
            return Regex.Matches(pSb.ToString(), pStr).Count;
        }
    }
}
