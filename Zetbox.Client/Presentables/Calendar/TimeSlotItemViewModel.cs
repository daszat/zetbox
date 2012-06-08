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
    using System.Linq;
    using System.Text;
    using System.Windows.Media;

    public class TimeSlotItemViewModel
    {
        public TimeSlotItemViewModel()
            : this(DateTime.Today, 0, 0)
        {
        }

        public TimeSlotItemViewModel(DateTime day, int hour)
            : this(day, hour, 0)
        {
        }

        public TimeSlotItemViewModel(DateTime day, int hour, int minute)
        {
            this.Day = day;
            this.Hour = hour;
            this.Minute = minute;
        }

        public int Hour { get; set; }
        public int Minute { get; set; }
        public DateTime Day { get; set; }

        public string Background
        {
            get
            {
                return Day.DayOfWeek == DayOfWeek.Saturday || Day.DayOfWeek == DayOfWeek.Sunday || Hour < 8 || Hour > 16 ? "#E0FFFF" : "White";
            }
        }

        public DateTime DateTime
        {
            get
            {
                return Day.AddHours(Hour).AddMinutes(Minute);
            }
        }

        public System.Drawing.RectangleF BorderThickness 
        {
            get
            {
                return new System.Drawing.RectangleF(0.0f, 0.0f, 1.0f, Minute == 0 ? 0.5f : 1.0f);
            }
        }
    }
}
