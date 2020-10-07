using System.Collections.Generic;
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

    public partial class ConsoleDebug : Window
    {
        public ConsoleDebug()
        {
            InitializeComponent();

            TraceLogs.IsConsoleDebugVisible = true;
        }

        /// <summary>
        /// Adding the new paragraph to the debug console output, depending of the category checked
        /// </summary>
        /// <param name="pTrace"></param>
        private void AddToParagraph(LogDebugTable pTrace)
        {
            if ((bool)CheckBoxInfo.IsChecked && pTrace?.DetailCategory == (int)LogCategory.Info)
            {
                SetNewParagraph(pTrace.DetailText, GetColorFromCategory((LogCategory)pTrace.DetailCategory));
            }

            if ((bool)CheckBoxWarning.IsChecked && pTrace?.DetailCategory == (int)LogCategory.Warning)
            {
                SetNewParagraph(pTrace.DetailText, GetColorFromCategory((LogCategory)pTrace.DetailCategory));
            }

            if ((bool)CheckBoxTechnical.IsChecked && pTrace?.DetailCategory == (int)LogCategory.Technical)
            {
                SetNewParagraph(pTrace.DetailText, GetColorFromCategory((LogCategory)pTrace.DetailCategory));
            }

            if ((bool)CheckBoxError.IsChecked && pTrace?.DetailCategory == (int)LogCategory.Error)
            {
                SetNewParagraph(pTrace.DetailText, GetColorFromCategory((LogCategory)pTrace.DetailCategory));
            }

            if ((bool)CheckBoxOutput.IsChecked && pTrace?.DetailCategory == (int)LogCategory.Output)
            {
                SetNewParagraph(pTrace.DetailText, GetColorFromCategory((LogCategory)pTrace.DetailCategory));
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

        private void CheckBox_DisplayMessageFromChecked(object sender, RoutedEventArgs e)
        {
            List<LogCategory> lCategoryChecked = ReturnListCategoriesChecked();

            UIRichTextBoxConsoleDebug.Document.Blocks.Clear();

            // Interogating debug table from database here
        }

        private void Button_SaveClick(object sender, RoutedEventArgs e)
        {
            TraceLogs.AddInfo($"{GlobalUsage.GetCurrentMethodName}: Saving Console debug logs");

            GlobalUsage.SaveToFile(UIRichTextBoxConsoleDebug);
        }
    }
}
