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
    public class CalendarViewModel : DataObjectViewModel
    {
        public new delegate CalendarViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Calendar calendar);

        public CalendarViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Calendar calendar)
            : base(appCtx, dataCtx, parent, calendar)
        {
            this.Calendar = calendar;
        }

        public Calendar Calendar { get; private set; }

        protected override void OnObjectPropertyChanged(string propName)
        {
            base.OnObjectPropertyChanged(propName);

            switch(propName)
            {
                case "Name":
                    OnPropertyChanged("Name");
                    break;
            }
        }

        public override string Name
        {
            get { return Calendar.Name; }
        }

        private string _color;
        public string Color
        {
            get
            {
                return string.IsNullOrWhiteSpace(_color) ? WeekCalendarViewModel.DefaultColor : _color;
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
