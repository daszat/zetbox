using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Kistl.Client.GUI;
using Kistl.Client.Presentables.Calendar;

namespace Kistl.Client.WPF.View.Calendar
{
    /// <summary>
    /// Interaction logic for Calendar.xaml
    /// </summary>
    /// <remarks>Based on http://www.codeproject.com/KB/WPF/WPFOutlookCalendar.aspx </remarks>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class WeekCalendar : UserControl, IHasViewModel<WeekCalendarViewModel>
    {
        public WeekCalendar()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(WeekCalendar_Loaded);
        }

        void WeekCalendar_Loaded(object sender, RoutedEventArgs e)
        {
            scrollDayEntries.ScrollToVerticalOffset(scrollDayEntries.ScrollableHeight / 2.0);
        }

        #region IHasViewModel<WeekCalendarViewModel> Members

        public WeekCalendarViewModel ViewModel
        {
            get { return (WeekCalendarViewModel)DataContext; }
        }

        #endregion
    }
}
