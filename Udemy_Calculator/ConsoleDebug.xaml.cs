using System;
using System.Windows;
using System.Windows.Documents;

namespace Udemy_Calculator
{
    public partial class ConsoleDebug : Window
    {
        public ConsoleDebug()
        {
            InitializeComponent();
        }

        public void GetMessages(string lMessage)
        {
            var paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(string.Format(lMessage, Environment.TickCount)));
            UIRichTextBoxConsoleDebug.Document.Blocks.Add(paragraph);

            UIRichTextBoxConsoleDebug.Focus();
            UIRichTextBoxConsoleDebug.ScrollToEnd();
        }
    }
}
