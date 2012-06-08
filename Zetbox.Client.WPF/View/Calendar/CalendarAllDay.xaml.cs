using System;
using System.Collections.Generic;
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
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables.Calendar;

namespace Zetbox.Client.WPF.View.Calendar
{
    /// <summary>
    /// Interaction logic for CalendarAllDay.xaml
    /// </summary>
    public partial class CalendarAllDay : UserControl, IHasViewModel<DayCalendarViewModel>
    {
        public CalendarAllDay()
        {
            InitializeComponent();
        }

        private void CalendarItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement)
            {
                var fe = (FrameworkElement)sender;
                ViewModel.WeekCalendar.SelectedItem = ((CalendarItemViewModel)fe.DataContext).ObjectViewModel;
            }
        }

        #region IHasViewModel<DayCalendarViewModel> Members

        public DayCalendarViewModel ViewModel
        {
            get { return (DayCalendarViewModel)DataContext; }
        }

        #endregion
    }
}
