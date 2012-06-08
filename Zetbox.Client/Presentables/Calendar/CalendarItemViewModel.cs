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
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;

    public class CalendarItemViewModel : ViewModel
    {
        public new delegate CalendarItemViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IAppointmentViewModel obj);

        public CalendarItemViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IAppointmentViewModel obj)
            : base(dependencies, dataCtx, parent)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            this.SlotWidth = this.OverlappingWidth = 1.0;
            this.ObjectViewModel = obj;
            this.ObjectViewModel.PropertyChanged += AppointmentViewModelChanged;
        }

        private void AppointmentViewModelChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "From":
                case "Until":
                    // must be handled by Calendar
                    break;
                case "Subject":
                    OnPropertyChanged("Name");
                    OnPropertyChanged("Text");
                    break;
                case "Color":
                    OnPropertyChanged("Color");
                    break;
                case "Location":
                case "Body":
                case "RequestedCalendarKind":
                    // not used
                    break;
            }
        }

        private void DayCalendarPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "ActualWidth":
                    OnPropertyChanged("ActualWidth");
                    OnPropertyChanged("Width");
                    OnPropertyChanged("Position");
                    break;
            }
        }

        private DayCalendarViewModel _DayCalendar;
        internal DayCalendarViewModel DayCalendar
        {
            get { return _DayCalendar; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (_DayCalendar != null)
                    _DayCalendar.PropertyChanged -= DayCalendarPropertyChanged;
                _DayCalendar = value;
                _DayCalendar.PropertyChanged += DayCalendarPropertyChanged;
            }
        }

        public IAppointmentViewModel ObjectViewModel { get; private set; }

        private DateTime _From;
        public DateTime From
        {
            get
            {
                return _From;
            }
            set
            {
                if (_From != value)
                {
                    _From = value;
                    OnPropertyChanged("From");
                    OnPropertyChanged("FromToText");
                }
            }
        }

        private DateTime _until;
        public DateTime Until
        {
            get
            {
                return _until;
            }
            set
            {
                if (_until != value)
                {
                    _until = value;
                    OnPropertyChanged("Until");
                    OnPropertyChanged("FromToText");
                }
            }
        }

        public string FromToText
        {
            get
            {
                return string.Format("{0} - {1}", From.ToShortTimeString(), Until.ToShortTimeString());
            }
        }

        private bool _isAllDay = false;
        public bool IsAllDay
        {
            get
            {
                return _isAllDay;
            }
            set
            {
                if (_isAllDay != value)
                {
                    _isAllDay = value;
                    OnPropertyChanged("IsAllDay");
                }
            }
        }

        public string Text
        {
            get
            {
                return ObjectViewModel.Subject;
            }
        }

        public string Color
        {
            get
            {
                return ObjectViewModel.Color;
            }
        }

        public PointF Position
        {
            get
            {
                return new PointF((float)(OverlappingIndex * SlotWidth * ActualWidth) - 1.0f, (float)From.TimeOfDay.TotalHours * 44.0f - 1.0f);
            }
        }

        public int OverlappingIndex { get; set; }
        public double OverlappingWidth { get; set; }
        public double SlotWidth { get; set; }

        public double Width
        {
            get
            {
                return (ActualWidth * OverlappingWidth) + 1.0d; // Adjust for border width on meeting items, which should be only drawn once
            }
        }

        public int Height
        {
            get
            {
                var length = (Until - From).TotalHours;
                if (length < 0.5) length = 0.5; // display at least half a line
                return (int)(length * 44.0) + 1;
            }
        }

        public override string Name
        {
            get { return Text; }
        }

        public double ActualWidth
        {
            get
            {
                return DayCalendar != null ? DayCalendar.ActualWidth - 6 : 0;
            }
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }
    }
}
