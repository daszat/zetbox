namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Calendar;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;

    [ViewModelDescriptor]
    public class EventViewModel : DataObjectViewModel
    {
        public new delegate EventViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Event evt);

        public EventViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Event evt)
            : base(appCtx, dataCtx, parent, evt)
        {
            this.Event = evt;
        }

        public Event Event { get; private set; }

        protected override void OnObjectPropertyChanged(string propName)
        {
            base.OnObjectPropertyChanged(propName);
            switch(propName)
            {
                case "Calendar":
                    if (_calendarViewModel != null)
                    {
                        _calendarViewModel.PropertyChanged -= _calendarViewModel_PropertyChanged;
                        _calendarViewModel = null;
                        OnPropertyChanged("CalendarViewModel");
                    }
                    break;
            }
        }

        protected override void OnPropertyModelsByNameCreated()
        {
            base.OnPropertyModelsByNameCreated();

            var startDateVmdl = (NullableDateTimePropertyViewModel)PropertyModelsByName["StartDate"];
            startDateVmdl.InputAccepted += (s, e) =>
            {
                if (e.NewValue.HasValue && e.OldValue.HasValue)
                {
                    Event.EndDate = Event.EndDate + (e.NewValue.Value - e.OldValue.Value);
                }
            };
        }

        public override string Name
        {
            get { return Event.Summary; }
        }

        private CalendarViewModel _calendarViewModel;
        public CalendarViewModel CalendarViewModel
        {
            get
            {
                if (_calendarViewModel == null)
                {
                    _calendarViewModel = (CalendarViewModel)DataObjectViewModel.Fetch(ViewModelFactory, DataContext, Parent, Event.Calendar);
                    _calendarViewModel.PropertyChanged += _calendarViewModel_PropertyChanged;
                }
                return _calendarViewModel;
            }
        }

        void _calendarViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "Color":
                    if(string.IsNullOrWhiteSpace(_color))
                        OnPropertyChanged("Color");
                    break;
            }
        }

        private string _color;
        public string Color
        {
            get
            {
                return string.IsNullOrWhiteSpace(_color) ? CalendarViewModel.Color : _color;
            }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    OnPropertyChanged("Color");
                }
            }
        }
    }
}
