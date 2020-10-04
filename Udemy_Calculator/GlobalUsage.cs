using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Udemy_Calculator
{
    public static class GlobalUsage
    {
        /// <summary>
        /// Get the calling method name
        /// </summary>
        public static string GetCurrentMethodName
        {
            get { return CurrentMethodName(); }
        }

        /// <summary>
        /// Get the calling class name
        /// </summary>
        public static string GetCurrentClassName
        {
            get { return CurrentClassName(); }
        }

        /// <summary>
        /// Getting the parent name calling method
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static string CurrentMethodName()
        {
            return new System.Diagnostics.StackTrace(2, false).GetFrame(0).GetMethod().Name;
        }

        /// <summary>
        /// Getting the parent name calling class
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static string CurrentClassName()
        {
            return new System.Diagnostics.StackTrace(2, false).GetFrame(0).GetMethod().DeclaringType.Name.ToString();
        }

        /// <summary>
        /// Generate the actual time 
        /// Optional with millisecond precisions (by default true)
        /// </summary>
        /// <returns></returns>
        public static string GenerateTimeNow(bool pWithMilliseconds = true)
        {
            string lResult = DateTime.Now.ToString("yyyyMMddHHmmss");

            if (pWithMilliseconds)
            {
                lResult += $".{DateTime.Now.Millisecond}";
            }
            return lResult;
        }

        /// <summary>
        /// Save RichTextBox content to file
        /// Optional: precising the fileName
        /// </summary>
        /// <param name="pControl"></param>
        /// <param name="pFileName"></param>
        public static void SaveToFile(RichTextBox pControl, string pFileName = "")
        {
            if (pControl != null)
            {
                using (System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog())
                {
                    dialog.Filter = "Text Files(*.txt)|*.txt|All(*.*)|*";

                    if (string.IsNullOrEmpty(pFileName))
                    {
                        pFileName = $"{GenerateTimeNow(false)}_{CurrentClassName()}";
                    }

                    dialog.FileName = pFileName;
                    dialog.ShowDialog();

                    string lRichText = new TextRange(pControl.Document.ContentStart, pControl.Document.ContentEnd).Text;

                    File.WriteAllText(dialog.FileName, lRichText, System.Text.Encoding.UTF8);
                }
            }

        }
    }
}
