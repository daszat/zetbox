namespace Kistl.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class LedgerItemViewModel
    {
        public LedgerItemViewModel()
        {
        }

        public LedgerItemViewModel(int hour)
        {
            this.Hour = hour;
            this.Minute = 0;
        }

        public int Hour { get; set; }
        public int Minute { get; set; }

        public string HourText
        {
            get
            {
                return Hour.ToString("00");
            }
        }
        public string MinuteText
        {
            get
            {
                return Minute.ToString("00");
            }
        }
    }
}
