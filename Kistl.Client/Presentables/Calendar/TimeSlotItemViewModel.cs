namespace Kistl.Client.Presentables.Calendar
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
