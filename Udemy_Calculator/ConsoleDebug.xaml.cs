using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
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
        private static bool mIsConsoleDebugVisible;

        /// <summary>
        /// Get or set the visibility of the console debug window
        /// </summary>
        public static bool IsConsoleDebugVisible
        {
            get
            {
                return mIsConsoleDebugVisible;
            }
            set
            {
                mIsConsoleDebugVisible = value;
            }
        }

        private static BindingList<LogTrace> mBufferLogList = new BindingList<LogTrace>();

        /// <summary>
        /// Buffering all logs into this list
        /// </summary>
        public static BindingList<LogTrace> BufferLogList
        {
            get
            {
                return mBufferLogList;
            }
            set
            {
                mBufferLogList = value;
            }
        }

        private static BindingList<LogTrace> mLogList = new BindingList<LogTrace>();

        /// <summary>
        /// Special list allowing to raise event handler when changing
        /// </summary>
        public static BindingList<LogTrace> LogList
        {
            get
            {
                return mLogList;
            }
            set
            {
                mLogList = value;
            }
        }

        /// <summary>
        /// Concat log entries with date/hours/min/sec to the logList
        /// </summary>
        /// <param name="pMessage"></param>
        /// <param name="pCat"></param>
        private static void LogEnqueue(string pMessage, LogCategory pCat = LogCategory.Info)
        {
            if (IsConsoleDebugVisible)
            {
                string lStr = $"{DateTime.Now}: {pMessage}";
                LogList.Add(new LogTrace(lStr, pCat));
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
            BindingList<LogTrace> lList = sender as BindingList<LogTrace>;

            if (lList != null && lList.Count > 0)
                AddToParagraph(lList.Last());
        }

        /// <summary>
        /// Set new paragraph to the console debug, with the right color output
        /// </summary>
        /// <param name="pMessage"></param>
        /// <param name="pColor"></param>
        private void SetNewParagraph(string pMessage, Brush pColor)
        {
            string lInline = new string('-', 150) + "\n";
            var paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(lInline));
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
                    return Brushes.LightBlue;

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


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox lCheckBox = sender as System.Windows.Controls.CheckBox;

            LogCategory lLCat = LogCategory.Info;

            switch (lCheckBox.Content.ToString())
            {
                case "Info":
                    lLCat = LogCategory.Info;
                    break;
                case "Warning":
                    lLCat = LogCategory.Warning;
                    break;
                case "Error":
                    lLCat = LogCategory.Error;
                    break;
                case "Output":
                    lLCat = LogCategory.Output;
                    break;
                case "Technical":
                    lLCat = LogCategory.Technical;
                    break;
            }

            for (int i = 0; i < TraceLogs.LogList.Count; i++)
            {
                TraceLogs.BufferLogList.Add(TraceLogs.LogList[i]);
            }

            TraceLogs.LogList.Clear();

            TraceLogs.BufferLogList = new BindingList<LogTrace>(TraceLogs.BufferLogList.Distinct().ToList());

            UIRichTextBoxConsoleDebug.Document.Blocks.Clear();

            for (int i = 0; i < TraceLogs.BufferLogList.Count; i++)
            {
                if (TraceLogs.BufferLogList[i].Category != lLCat)
                {
                    TraceLogs.LogList.Add(TraceLogs.BufferLogList[i]);
                }
            }
        }
    }
}
