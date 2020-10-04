using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Udemy_Calculator
{

    /// <summary>
    /// Category of logs
    /// </summary>
    public enum LogCategory { Info, Warning, Error, Output, Technical }

    /// <summary>
    /// Log tracing object to fill with string part for the message
    /// and LogCategory
    /// </summary>
    public class LogTrace
    {
        public LogTrace()
        {
            //Empty
        }

        public LogTrace(string pMessage, LogCategory pCat = LogCategory.Info)
        {
            Message = pMessage;
            Category = pCat;
        }

        public string Message { get; set; }
        public LogCategory Category { get; set; }
    }

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
        /// Buffering all logs into this list
        /// </summary>
        internal static BindingList<LogTrace> BufferLogList { get; set; } = new BindingList<LogTrace>();

        /// <summary>
        /// Special list allowing to raise event handler when changing
        /// </summary>
        internal static BindingList<LogTrace> LogList { get; set; } = new BindingList<LogTrace>();

        /// <summary>
        /// Concat log entries with date/hours/min/sec to the logList
        /// </summary>
        /// <param name="pMessage"></param>
        /// <param name="pCategory"></param>
        private static void LogEnqueue(string pMessage, LogCategory pCategory = LogCategory.Info)
        {
            if (IsConsoleDebugVisible)
            {
                string lStr = $"{GlobalUsage.GenerateTimeNow()}: {pCategory} - {pMessage}";
                LogList.Add(new LogTrace(lStr, pCategory));
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

    public partial class ConsoleDebug : Window
    {
        public ConsoleDebug()
        {
            InitializeComponent();

            TraceLogs.IsConsoleDebugVisible = true;

            TraceLogs.LogList.AllowNew = true;
            TraceLogs.LogList.AllowRemove = true;
            TraceLogs.LogList.AllowEdit = false;
            TraceLogs.LogList.RaiseListChangedEvents = true;
            TraceLogs.LogList.ListChanged += new ListChangedEventHandler(LogList_ListChanged);

            TraceLogs.AddInfo("Initializing the console finish... OK");
            TraceLogs.AddWarning("Testing logs warning... OK");
            TraceLogs.AddError("Testing logs error... OK");
            TraceLogs.AddOutput("Testing logs output... OK");
            TraceLogs.AddTechnical("Testing logs technical... OK");
        }

        /// <summary>
        /// Adding the new paragraph to the debug console output, depending of the category checked
        /// </summary>
        /// <param name="pTrace"></param>
        private void AddToParagraph(LogTrace pTrace)
        {
            if ((bool)CheckBoxInfo.IsChecked && pTrace?.Category == LogCategory.Info)
            {
                SetNewParagraph(pTrace.Message, GetColorFromCategory(pTrace.Category));
            }

            if ((bool)CheckBoxWarning.IsChecked && pTrace?.Category == LogCategory.Warning)
            {
                SetNewParagraph(pTrace.Message, GetColorFromCategory(pTrace.Category));
            }

            if ((bool)CheckBoxTechnical.IsChecked && pTrace?.Category == LogCategory.Technical)
            {
                SetNewParagraph(pTrace.Message, GetColorFromCategory(pTrace.Category));
            }

            if ((bool)CheckBoxError.IsChecked && pTrace?.Category == LogCategory.Error)
            {
                SetNewParagraph(pTrace.Message, GetColorFromCategory(pTrace.Category));
            }

            if ((bool)CheckBoxOutput.IsChecked && pTrace?.Category == LogCategory.Output)
            {
                SetNewParagraph(pTrace.Message, GetColorFromCategory(pTrace.Category));
            }
        }

        /// <summary>
        /// Event to take in charge changed of the dynamic logs list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (sender is BindingList<LogTrace> lList && lList.Count > 0)
            {
                AddToParagraph(lList.Last());
            }
        }

        /// <summary>
        /// Set new paragraph to the console debug, with the right color output
        /// </summary>
        /// <param name="pMessage"></param>
        /// <param name="pColor"></param>
        private void SetNewParagraph(string pMessage, Brush pColor)
        {
            //string lInline = new string('-', 100) + "\n";
            var paragraph = new Paragraph();
            //paragraph.Inlines.Add(new Run(lInline));
            paragraph.Inlines.Add(new Run(pMessage));
            paragraph.Foreground = pColor;
            UIRichTextBoxConsoleDebug.Document.Blocks.Add(paragraph);

            UIRichTextBoxConsoleDebug.Focus();
            UIRichTextBoxConsoleDebug.ScrollToEnd();
        }

        /// <summary>
        /// Return the color corresponds from the category
        /// </summary>
        /// <param name="pLogCat"></param>
        /// <returns></returns>
        private static Brush GetColorFromCategory(LogCategory pLogCat)
        {
            switch (pLogCat)
            {
                case LogCategory.Info:
                    return Brushes.White;

                case LogCategory.Warning:
                    return Brushes.Orange;

                case LogCategory.Error:
                    return Brushes.Red;

                case LogCategory.Output:
                    return Brushes.LightGreen;

                case LogCategory.Technical:
                    return Brushes.Yellow;
                default:
                    return Brushes.Yellow;
            }
        }

        private void ClearConsole_Click(object sender, RoutedEventArgs e)
        {
            UIRichTextBoxConsoleDebug.Document.Blocks.Clear();
            TraceLogs.BufferLogList.Clear();
            TraceLogs.LogList.Clear();
        }

        private void CloseConsole_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            TraceLogs.IsConsoleDebugVisible = false;
        }

        #region Moving the Calculator

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            this.Opacity = 0.75F;
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            this.Opacity = 1F;
            base.OnMouseLeftButtonUp(e);
        }

        #endregion

        /// <summary>
        /// Fonction to return the list categories checked
        /// </summary>
        /// <param name="pCheckBoxType"></param>
        /// <returns></returns>
        private List<LogCategory> ReturnListCategoriesChecked()
        {
            List<LogCategory> lListCategoriesChecked = new List<LogCategory>();

            // Get all the checkbox checked
            foreach (var lCheckBoxCategory in GridCheckBoxes.Children.Cast<CheckBox>())
            {
                if (lCheckBoxCategory.IsChecked == true)
                {
                    switch (lCheckBoxCategory.Content.ToString())
                    {
                        case "Info":
                            lListCategoriesChecked.Add(LogCategory.Info);
                            break;
                        case "Warning":
                            lListCategoriesChecked.Add(LogCategory.Warning);
                            break;
                        case "Error":
                            lListCategoriesChecked.Add(LogCategory.Error);
                            break;
                        case "Output":
                            lListCategoriesChecked.Add(LogCategory.Output);
                            break;
                        case "Technical":
                            lListCategoriesChecked.Add(LogCategory.Technical);
                            break;
                    }
                }
            }

            return lListCategoriesChecked;
        }

        /// <summary>
        /// Fill the buffer log list to keep it in memory
        /// </summary>
        private static void FillBufferLogList()
        {
            for (int i = 0; i < TraceLogs.LogList.Count; i++)
            {
                if (!TraceLogs.BufferLogList.Contains(TraceLogs.LogList[i]))
                {
                    TraceLogs.BufferLogList.Add(TraceLogs.LogList[i]);
                }
            }
        }

        private void CheckBox_DisplayMessageFromChecked(object sender, RoutedEventArgs e)
        {
            List<LogCategory> lCategoryChecked = ReturnListCategoriesChecked();

            FillBufferLogList();

            TraceLogs.LogList.Clear();
            UIRichTextBoxConsoleDebug.Document.Blocks.Clear();

            // Display messages categories checked 
            for (int i = 0; i < TraceLogs.BufferLogList.Count; i++)
            {
                if (lCategoryChecked.Contains(TraceLogs.BufferLogList[i].Category))
                {
                    TraceLogs.LogList.Add(TraceLogs.BufferLogList[i]);
                }
            }
        }

        private void Button_SaveClick(object sender, RoutedEventArgs e)
        {
            TraceLogs.AddInfo($"{GlobalUsage.GetCurrentMethodName}: Saving Console debug logs");

            GlobalUsage.SaveToFile(UIRichTextBoxConsoleDebug);
        }
    }
}
