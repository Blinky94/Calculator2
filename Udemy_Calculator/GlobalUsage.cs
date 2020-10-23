using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Udemy_Calculator
{
    /// <summary>
    /// Font enumeration size
    /// </summary>
    public enum SizeOfFont
    {
        ExtraTiny = 5,
        Tiny = 7,
        UltraSmall = 9,
        ExtraSmall = 11,
        Small = 13,
        Normal = 15,
        Big = 17,
        ExtraBig = 20,
        UltraBig = 25,
        Huge = 30,
        ExtraHuge = 35
    }

    public static class GlobalUsage
    {
        /// <summary>
        /// Get or set the list of chunk used in a formula
        /// </summary>
        public static BindingList<ChunkTable> ListChunks { get; set; } = new BindingList<ChunkTable>();

        /// <summary>
        /// Get or set the list of debugging logs
        /// </summary>
        public static BindingList<LogDebug> ListLogDebug { get; set; } = new BindingList<LogDebug>();

        private const string DatabaseName = "Calculator.db";

        private static readonly string mCurrentDirectory = Environment.CurrentDirectory;
        private static string mFolderPath = mCurrentDirectory;
        public static string FolderPath { get => mFolderPath; set => mFolderPath = value; }

        public static string DatabasePath
        {
            get
            {
                return Path.Combine(FolderPath, DatabaseName);
            }
        }

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
            get
            {
                return CurrentClassName();
            }
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

        /// <summary>
        /// Save List of log debug content to file
        /// Optional: precising the fileName
        /// </summary>
        /// <param name="pControl"></param>
        /// <param name="pFileName"></param>
        public static void SaveToFile(List<LogDebug> pList, string pFileName = "")
        {
            if (pList != null)
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

                    StringBuilder list = new StringBuilder();

                    foreach (LogDebug lItem in pList)
                    {
                        list.AppendLine(lItem.ToString());
                    }

                    File.WriteAllText(dialog.FileName, list.ToString(), Encoding.UTF8);

                    TraceLogs.AddWarning($"{GlobalUsage.GetCurrentMethodName}: {dialog.FileName} has been saved !");
                }
            }
        }

        /// <summary>
        /// Converting string value to FontWeight
        /// </summary>
        /// <param name="pFontWtStr"></param>
        /// <returns>FontWeight</returns>
        internal static FontWeight? ConvertStringToFontWeight(string pFontWtStr)
        {
            FontWeight? lFontWt;

            switch (pFontWtStr)
            {
                case "Thin":
                    lFontWt = FontWeights.Thin;
                    break;
                case "ExtraLight":
                    lFontWt = FontWeights.ExtraLight;
                    break;
                case "UltraLight":
                    lFontWt = FontWeights.UltraLight;
                    break;
                case "Light":
                    lFontWt = FontWeights.Light;
                    break;
                case "Normal":
                    lFontWt = FontWeights.Normal;
                    break;
                case "Regular":
                    lFontWt = FontWeights.Regular;
                    break;
                case "Medium":
                    lFontWt = FontWeights.Medium;
                    break;
                case "DemiBold":
                    lFontWt = FontWeights.DemiBold;
                    break;
                case "SemiBold":
                    lFontWt = FontWeights.SemiBold;
                    break;
                case "Bold":
                    lFontWt = FontWeights.Bold;
                    break;
                case "ExtraBold":
                    lFontWt = FontWeights.ExtraBold;
                    break;
                case "UltraBold":
                    lFontWt = FontWeights.UltraBold;
                    break;
                case "Black":
                    lFontWt = FontWeights.Black;
                    break;
                case "Heavy":
                    lFontWt = FontWeights.Heavy;
                    break;
                case "ExtraBlack":
                    lFontWt = FontWeights.ExtraBlack;
                    break;
                case "UltraBlack":
                    lFontWt = FontWeights.UltraBlack;
                    break;
                default:
                    lFontWt = null;
                    break;
            }
            return lFontWt;
        }
    }
}
