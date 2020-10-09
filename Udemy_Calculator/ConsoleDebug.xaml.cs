using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Udemy_Calculator
{
    /// <summary>
    /// Category of logs
    /// </summary>
    public enum LogCategory { Info, Warning, Error, Output, Technical }

    public partial class ConsoleDebug : Window
    {     
        public ConsoleDebug()
        {
            InitializeComponent();

            TraceLogs.IsConsoleDebugVisible = true;
            GlobalUsage.ListLogDebug.ListChanged += new ListChangedEventHandler(LogList_ListChanged);
        }

        /// <summary>
        /// Event to take in charge changed of the dynamic logs list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogList_ListChanged(object sender, ListChangedEventArgs e)
        {
           SetNewParagraph();
        }

        /// <summary>
        /// Set new paragraph to the console debug, with the right color output
        /// </summary>
        /// <param name="pMessage"></param>
        /// <param name="pColor"></param>
        private void SetNewParagraph()
        {
            var lList = ReturnListCategoriesFromChecked();
            if (lList != null)
            {
                ConsoleDebugListView.ItemsSource = lList;
            }
            ConsoleDebugScrollViewer.ScrollToEnd();
        }

        /// <summary>
        /// Clearing all elements in the log debug list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearConsole_Click(object sender, RoutedEventArgs e)
        {
            var lList = ReturnListCategoriesFromChecked();
            if (lList != null)
            {
                GlobalUsage.ListLogDebug.ToList().ForEach(p =>
                {
                    if (lList.Contains(p))
                    {
                        GlobalUsage.ListLogDebug.Remove(p);
                    }
                });
            }
        }

        /// <summary>
        /// Closing (hidding) the current window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseConsole_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            TraceLogs.IsConsoleDebugVisible = false;
        }

        #region Moving the Console Debug Window

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
        /// Return the logdebug list from boxes selected
        /// </summary>
        /// <param name="pCheckBoxType"></param>
        /// <returns></returns>
        private List<LogDebug> ReturnListCategoriesFromChecked()
        {
            return GlobalUsage.ListLogDebug.Where(p => GridCheckBoxes.Children.Cast<CheckBox>().Where(c => (bool)c.IsChecked).Select(d => (int)Enum.Parse(typeof(LogCategory), d.Content.ToString(), true)).ToList().Contains(p.DetailCategory)).ToList();
        }

        /// <summary>
        /// Refreshing the console debug listview with categories selected checkboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_DisplayMessageFromChecked(object sender, RoutedEventArgs e)
        {
            var lList = ReturnListCategoriesFromChecked();
            if (lList != null)
            {
                ConsoleDebugListView.ItemsSource = lList;
            }

            ConsoleDebugScrollViewer.ScrollToEnd();
        }

        /// <summary>
        /// Saving debug logs selected to a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_SaveClick(object sender, RoutedEventArgs e)
        {
            var lList = ReturnListCategoriesFromChecked();
            if (lList != null)
            {
                GlobalUsage.SaveToFile(lList);
            }
        }

        /// <summary>
        /// Event to allow the wheel of the mouse to be mouved on the console debug screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsoleDebugListView_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }
    }
}
