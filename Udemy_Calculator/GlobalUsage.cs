using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Udemy_Calculator
{
    public static class GlobalUsage
    {
        public static volatile List<FormulaTable> ListFormula = new List<FormulaTable>();
        public static volatile List<LogDebugTable> ListLogDebug = new List<LogDebugTable>();
        public static volatile List<ChunkTable> ListChunk = new List<ChunkTable>();

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

        public static int CurrentFormulaId { get; set; } = -1;

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

        private static readonly object mCollisionLock = new object();

        /// <summary>
        /// Inserting a new row into a specific table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pItem"></param>
        public static void Insert<T>(T pItem)
        {
            lock (mCollisionLock)
            {
                using (var lConnection = new SQLiteConnection(DatabasePath))
                {
                    lConnection.CreateTable<T>();
                    lConnection.Insert(pItem, typeof(T));
                }
            }
        }

        /// <summary>
        /// Set the sqlite configuration with pragma journal_mode=WAL
        /// </summary>
        public static void SetSqlitePragmaConfiguration()
        {
            using (var lConnection = new SQLiteConnection(DatabasePath))
            {
                var cmd = lConnection.CreateCommand("PRAGMA journal_mode=WAL", Array.Empty<object>());
                var result = cmd.ExecuteQuery<object>();
            }          
        }

        /// <summary>
        /// Updating existing row into a specific table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pItem"></param>
        public static void Update<T>(T pItem)
        {
            lock (mCollisionLock)
            {
                using (var lConnection = new SQLiteConnection(DatabasePath))
                {
                    lConnection.CreateTable<T>();
                    lConnection.Update(pItem, typeof(T));
                }
            }
        }

        /// <summary>
        /// Saving debug log to database
        /// </summary>
        public async static void SaveDebugLogToDatabase()
        {
            string lResult = string.Empty;

            ReplaceUnknownFKByRightOne();

            try
            {
                lResult = await Task.Run(() =>
                {
                    // Save to SQLite database chunk entries
                    for (int i = 0; i < ListLogDebug.Count; i++)
                    {
                        // Writing debug table object to Db
                        Insert(new LogDebugTable()
                        {
                            DetailDate = ListLogDebug[i].DetailDate,
                            DetailCategory = (int)ListLogDebug[i].DetailCategory,
                            DetailText = ListLogDebug[i].DetailText,
                            Formula_Id = ListLogDebug[i].Formula_Id
                        });

                        // Unstack the log debug inserted
                        ListLogDebug.RemoveAt(i);
                    }

                    return "Task completed";

                }).ConfigureAwait(true);
            }
            catch (Exception e)
            {
                lResult = e.Message;
            }
            finally
            {
                ListLogDebug.Clear();
                CurrentFormulaId = -1;
            }
        }

        /// <summary>
        /// Replacing all rows mark with -1 by the right FK
        /// </summary>
        private static void ReplaceUnknownFKByRightOne()
        {
            int lLast = ListLogDebug.Select(p => p.Formula_Id).LastOrDefault();

            ListLogDebug.Where(p => p.Formula_Id == -1).ToList().ForEach(p => { p.Formula_Id = lLast; });
        }
    }
}
