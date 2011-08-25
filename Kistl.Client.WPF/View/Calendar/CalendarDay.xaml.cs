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
using Kistl.Client.Presentables;
using Kistl.Client.Presentables.Calendar;

namespace Kistl.Client.WPF.View.Calendar
{
    /// <summary>
    /// Interaction logic for CalendarDay.xaml
    /// </summary>
    /// <remarks>Based on http://www.codeproject.com/KB/WPF/WPFOutlookCalendar.aspx </remarks>
    public partial class CalendarDay : UserControl, IHasViewModel<DayCalendarViewModel>
    {
        public CalendarDay()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();

            panelCalendarDay.SizeChanged += new SizeChangedEventHandler(panelCalendarDay_SizeChanged);
            DataContextChanged += new DependencyPropertyChangedEventHandler(CalendarDay_DataContextChanged);
        }

        void CalendarDay_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.ActualWidth = panelCalendarDay.ActualWidth;
            }            
        }

        void panelCalendarDay_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.ActualWidth = panelCalendarDay.ActualWidth;
            }
        }

        void timeslot_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement)
            {
                var mdl = ((TimeSlotItemViewModel)((FrameworkElement)sender).DataContext);
                ViewModel.NewTermin(mdl.DateTime);
            }
        }

        private void CalendarItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement)
            {
                ViewModel.WeekCalendar.SelectedItem = ((CalendarItemViewModel)((FrameworkElement)sender).DataContext).ObjectViewModel;
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
