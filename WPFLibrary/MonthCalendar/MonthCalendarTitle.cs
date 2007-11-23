using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Samples.KMoore.WPFSamples.DateControls
{
    public class MonthCalendarTitle : Control
    {
        /// <summary>
        /// Static Constructor
        /// </summary>
        static MonthCalendarTitle()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MonthCalendarTitle), new FrameworkPropertyMetadata(typeof(MonthCalendarTitle)));
        }
    }
}