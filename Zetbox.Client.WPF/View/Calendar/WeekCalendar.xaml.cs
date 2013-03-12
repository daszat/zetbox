// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
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
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables.Calendar;
using Zetbox.Client.WPF.Toolkit;

namespace Zetbox.Client.WPF.View.Calendar
{
    /// <summary>
    /// Interaction logic for Calendar.xaml
    /// </summary>
    /// <remarks>Based on http://www.codeproject.com/KB/WPF/WPFOutlookCalendar.aspx </remarks>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
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
            get { return (WeekCalendarViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }

        #endregion
    }
}
