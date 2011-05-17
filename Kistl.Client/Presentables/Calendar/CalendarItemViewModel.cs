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
        public new delegate CalendarItemViewModel Factory(IKistlContext dataCtx, DateTime from, DateTime to, string text, string color, DataObjectViewModel obj);

        public CalendarItemViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, DateTime from, DateTime to, string text, string color, DataObjectViewModel obj)
            : base(dependencies, dataCtx)
        {
            this.OverlappingWidth = 1.0;
            this.ObjectViewModel = obj;

            this.From = from;
            this.To = to;
            this.Text = text;
            this.Color = color;

            ObjectViewModel.PropertyChanged += obj_PropertyChanged;
        }

        void parent_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "ActualWidth":
                    OnPropertyChanged("ActualWidth");
                    OnPropertyChanged("Width");
                    OnPropertyChanged("Position");
                    break;
            }
        }

        void obj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // TODO: Bruteforce method
            DayCalendar.WeekCalendar.Refresh();
        }

        private DayCalendarViewModel _DayCalendar;
        internal DayCalendarViewModel DayCalendar
        {
            get { return _DayCalendar; }
            set
            {
                _DayCalendar = value;
                _DayCalendar.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(parent_PropertyChanged);
            }
        }

        public DataObjectViewModel ObjectViewModel { get; private set; }

        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public string Text { get; private set; }
        public string Color { get; private set; }

        public string FromToText
        {
            get
            {
                return string.Format("{0} - {1}", From.ToShortTimeString(), To.ToShortTimeString());
            }
        }

        public System.Drawing.PointF Position
        {
            get
            {
                return new System.Drawing.PointF((float)OverlappingIndex * (float)Width, (float)From.TimeOfDay.TotalHours * 44.0f);
            }
        }

        public int OverlappingIndex { get; set; }
        public double OverlappingWidth { get; set; }

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
                return (int)((To - From).TotalHours * 44.0);
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
