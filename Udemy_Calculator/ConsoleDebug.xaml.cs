using System;
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
    public enum LogCategory { Info, Warning, Error, Output }

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

        private static BindingList<LogTrace> mLogList = new BindingList<LogTrace>();
        public static BindingList<LogTrace> LogList
        {
            get
            {
                return mLogList;
            }
            private set
            {
                mLogList = value;
            }
        }

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
        }

        private void LogList_ListChanged(object sender, ListChangedEventArgs e)
        {
            for (int i = 0; i < TraceLogs.LogList.Count; i++)
            {
                SetNewParagraph(TraceLogs.LogList[i].Message, GetColorFromCategory(TraceLogs.LogList[i].Category));
                TraceLogs.LogList.RemoveAt(i);
            }
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
    }
}
