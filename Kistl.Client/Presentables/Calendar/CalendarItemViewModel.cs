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
        public new delegate CalendarItemViewModel Factory(IKistlContext dataCtx, ViewModel parent, DataObjectViewModel obj, Action<DataObjectViewModel, CalendarItemViewModel> update);

        public CalendarItemViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, DataObjectViewModel obj, Action<DataObjectViewModel, CalendarItemViewModel> update)
            : base(dependencies, dataCtx, parent)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (update == null) throw new ArgumentNullException("update");

            this.OverlappingWidth = 1.0;
            this.ObjectViewModel = obj;
            this._update = update;

            update(obj, this);

            ObjectViewModel.PropertyChanged += obj_PropertyChanged;
        }

        private Action<DataObjectViewModel, CalendarItemViewModel> _update;

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

        void obj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _update(ObjectViewModel, this);
            OnPropertyChanged("Width");
            OnPropertyChanged("Height");
            OnPropertyChanged("Position");
            if (DayCalendar != null) DayCalendar.WeekCalendar.UpdateItems();
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

        public DataObjectViewModel ObjectViewModel { get; private set; }

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

        private DateTime _To;
        public DateTime To
        {
            get
            {
                return _To;
            }
            set
            {
                if (_To != value)
                {
                    _To = value;
                    OnPropertyChanged("To");
                }
            }
        }


        private string _Text;
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                if (_Text != value)
                {
                    _Text = value;
                    OnPropertyChanged("Text");
                }
            }
        }


        private string _Color;
        public string Color
        {
            get
            {
                return _Color;
            }
            set
            {
                if (_Color != value)
                {
                    _Color = value;
                    OnPropertyChanged("Color");
                }
            }
        }


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
