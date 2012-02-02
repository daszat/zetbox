namespace Kistl.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.Client.Presentables;

    public class CalendarItemViewModel : Kistl.Client.Presentables.ViewModel
    {
        public new delegate CalendarItemViewModel Factory(IKistlContext dataCtx, ViewModel parent, IAppointmentViewModel obj);

        public CalendarItemViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IAppointmentViewModel obj)
            : base(dependencies, dataCtx, parent)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            this.SlotWidth = this.OverlappingWidth = 1.0;
            this.ObjectViewModel = obj;
        }

        void parent_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
                _DayCalendar = value;
                _DayCalendar.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(parent_PropertyChanged);
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
                }
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


        public string FromToText
        {
            get
            {
                return string.Format("{0} - {1}", From.ToShortTimeString(), Until.ToShortTimeString());
            }
        }

        public System.Drawing.PointF Position
        {
            get
            {
                return new System.Drawing.PointF((float)(OverlappingIndex * SlotWidth * ActualWidth) - 1.0f, (float)From.TimeOfDay.TotalHours * 44.0f - 1.0f);
            }
        }

        public int OverlappingIndex { get; set; }
        public double OverlappingWidth { get; set; }
        public double SlotWidth { get; set; }

        public double Width
        {
            get
            {
                return ActualWidth * OverlappingWidth;
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
                return DayCalendar != null ? DayCalendar.ActualWidth - 5 : 0;
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
