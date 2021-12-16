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
    using System.Drawing;
    using Zetbox.API;

    public sealed class TimeSlotItemViewModel : INotifyPropertyChanged
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
            this.Day = day.Date;
            this.Hour = hour;
            this.Minute = minute;
            this._background = GetBackground();
            this._borderThickness = GetBorderThickness();
        }

        private DateTime _day;
        private int _hour;
        private int _minute;

        public DateTime Day
        {
            get
            {
                return _day;
            }
            set
            {
                var realValue = value.Date;
                if (_day != realValue)
                {
                    _day = realValue;
                    Background = GetBackground();

                    OnPropertyChanged("Day");
                    OnPropertyChanged("DateTime");
                }
            }
        }

        public int Hour
        {
            get
            {
                return _hour;
            }
            set
            {
                if (_hour != value)
                {
                    _hour = value;
                    Background = GetBackground();

                    OnPropertyChanged("Hour");
                    OnPropertyChanged("DateTime");
                }
            }
        }

        public int Minute
        {
            get
            {
                return _minute;
            }
            set
            {
                if (_minute != value)
                {
                    _minute = value;
                    BorderThickness = GetBorderThickness();

                    OnPropertyChanged("Minute");
                    OnPropertyChanged("DateTime");
                }
            }
        }

        private string _color;
        public string Color
        {
            get
            {
                return _color;
            }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    OnPropertyChanged("Color");
                    Background = GetBackground();
                }
            }
        }

        private string _background;
        public string Background
        {
            get
            {
                return _background;
            }
            private set
            {
                if (_background != value)
                {
                    _background = value;
                    OnPropertyChanged("Background");
                }
            }
        }

        private string GetBackground()
        {
            try
            {
                if (Day.DayOfWeek == DayOfWeek.Saturday || Day.DayOfWeek == DayOfWeek.Sunday || Hour < 8 || Hour > 16)
                {
                    if (string.IsNullOrWhiteSpace(_color)) return "#E0FFFF";
                    return ColorTranslator.ToHtml(ColorTranslator.FromHtml(_color).Lighter(0.6));
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(_color)) return "#FFFFFF";
                    return ColorTranslator.ToHtml(ColorTranslator.FromHtml(_color).Lighter(0.8));
                }
            }
            catch
            {
                return "#FFFFFF";
            }
        }

        public DateTime DateTime
        {
            get
            {
                return Day.AddHours(Hour).AddMinutes(Minute);
            }
        }

        private System.Drawing.RectangleF _borderThickness;
        public System.Drawing.RectangleF BorderThickness
        {
            get
            {
                return _borderThickness;
            }
            private set
            {
                if (_borderThickness != value)
                {
                    _borderThickness = value;
                    OnPropertyChanged("BorderThickness");
                }
            }
        }

        private System.Drawing.RectangleF GetBorderThickness()
        {
            return new System.Drawing.RectangleF(0.0f, 0.0f, 1.0f, Minute == 0 ? 0.5f : 1.0f);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            var temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(property));
            }
        }

        public string NewEventLabel
        {
            get
            {
                return CalendarResources.NewEventLabel;
            }
        }
    }
}
