using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.Client.Presentables.Calendar
{
    public interface ICalendarDisplayViewModel
    {
        void Refresh();
        EventViewModel SelectedItem { get; set; }
    }
}
