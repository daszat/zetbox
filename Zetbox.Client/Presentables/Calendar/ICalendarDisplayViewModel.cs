using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetbox.Client.Presentables.Calendar
{
    public interface ICalendarDisplayViewModel
    {
        Task Refresh();
        EventViewModel SelectedItem { get; set; }
        CalendarViewModel SelectedCalendar { get; set; }
    }

    public class NewEventArgs : EventArgs
    {
        public NewEventArgs(DateTime dt, bool isAllDay)
        {
            Date = dt;
            IsAllDay = isAllDay;
        }
        public DateTime Date { get; private set; }
        public bool IsAllDay { get; private set; }
    }
    public class OpenEventArgs : EventArgs
    {
        public OpenEventArgs(EventViewModel evt)
        {
            Event = evt;
        }
        public EventViewModel Event { get; private set; }
    }
}
