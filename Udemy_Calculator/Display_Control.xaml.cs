using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Udemy_Calculator
{
    /// <summary>
    /// Interaction logic for Dislpay.xaml
    /// </summary>
    public partial class Display_Control : UserControl
    {
        #region UIDisplayBorderBrush

        public static readonly DependencyProperty UIDisplayBorderBrushProperty =
            DependencyProperty.Register("UIDisplayBorderBrush",
            typeof(Brush),
            typeof(Display_Control),
            new PropertyMetadata(Brushes.Transparent));
        public Brush UIDisplayBorderBrush
        {
            get { return (Brush)GetValue(UIDisplayBorderBrushProperty); }
            set { SetValue(UIDisplayBorderBrushProperty, value); }
        }

        #endregion

        public Display_Control()
        {
            InitializeComponent();
        }
    }
}
