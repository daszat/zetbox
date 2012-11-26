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

namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Zetbox.App.GUI;

    public interface IAppointmentViewModel : INotifyPropertyChanged
    {
        string Subject { get; }

        string SubjectAsync { get; }

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
        string Color { get; set; }

        ControlKind RequestedCalendarKind { get; }
    }
}
