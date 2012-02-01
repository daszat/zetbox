using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Client.Presentables.Calendar
{
    public interface IAppointmentViewModel
    {
        string Subject { get; }
        /// <summary>
        /// Start date and time of the appointment. This has to be a specific point in time.
        /// </summary>
        DateTime From { get; }
        /// <summary>
        /// End date and time of the appointment. This has to be a specific point in time. 
        /// All day appointments are not handled specialy. 
        /// All day appointments has to be from midnight to midnight next day. eg. one day appointment: 1.1.2012 00:00 until 2.1.2012 00:00.
        /// </summary>
        DateTime Until { get; }
        string Location { get; }
        string Body { get; }

        /// <summary>
        /// Color (HTML style) of the appointment. If null or empty, the item will be transperent. Use WeekCalendarViewModel.DefaultColor as a default.
        /// </summary>
        string Color { get; }

        event EventHandler Changed;
    }
}
