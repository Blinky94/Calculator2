namespace Udemy_Calculator
{
    /// <summary>
    /// Static class to encapsulate logs
    /// </summary>
    public static class TraceLogs
    {
        /// <summary>
        /// Get or set the visibility of the console debug window
        /// </summary>
        public static bool IsConsoleDebugVisible { get; set; }

        /// <summary>
        /// Concat log entries with date/hours/min/sec to the logList
        /// </summary>
        /// <param name="pMessage"></param>
        /// <param name="pCategory"></param>
        private static void LogEnqueue(string pMessage, LogCategory pCategory = LogCategory.Info)
        {
            if (IsConsoleDebugVisible)
            {
                var lLogDebug = new LogDebugTable
                {
                    DetailDate = GlobalUsage.GenerateTimeNow(true),
                    DetailText = pMessage,
                    DetailCategory = (int)pCategory,
                    Formula_Id = GlobalUsage.CurrentFormulaId
                };
               
                GlobalUsage.ListLogDebug.Add(lLogDebug);
                //GlobalUsage.Insert(lLogDebug);
            }
        }

        /// <summary>
        /// Add the message as an information to the ConsoleDebug window
        /// </summary>
        /// <param name="pMessage"></param>
        public static void AddInfo(string pMessage)
        {
            LogEnqueue(pMessage, LogCategory.Info);
        }

        /// <summary>
        /// Add the message as a warning to the ConsoleDebug window
        /// </summary>
        public static void AddWarning(string pMessage)
        {
            LogEnqueue(pMessage, LogCategory.Warning);
        }

        /// <summary>
        /// Add the message as an error to the ConsoleDebug window
        /// </summary>
        public static void AddError(string pMessage)
        {
            LogEnqueue(pMessage, LogCategory.Error);
        }

        /// <summary>
        /// Add the message as an output to the ConsoleDebug window
        /// </summary>
        public static void AddOutput(string pMessage)
        {
            LogEnqueue(pMessage, LogCategory.Output);
        }

        /// <summary>
        /// Add the message as a technical to the ConsoleDebug window
        /// </summary>
        public static void AddTechnical(string pMessage)
        {
            LogEnqueue(pMessage, LogCategory.Technical);
        }
    }
}
