using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_Calculator
{
    public static class ExtendedStreamBuilder
    {
        private static char[] items;

        public static bool Contains(this StringBuilder pSb, char[] pTab)
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
    }
}
