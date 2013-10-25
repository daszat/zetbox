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
        public new delegate CalendarViewModel Factory(IZetboxContext dataCtx, ViewModel parent, CalendarBook calendar);

        public CalendarViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, CalendarBook calendar)
            : base(appCtx, dataCtx, parent, calendar)
        {
            this.Calendar = calendar;
        }

        public CalendarBook Calendar { get; private set; }

        protected override void OnObjectPropertyChanged(string propName)
        {
            base.OnObjectPropertyChanged(propName);

            switch(propName)
            {
                case "Name":
                    OnPropertyChanged("Name");
                    break;
                case "Owner":
                case "Writers":
                case "GroupWriters":
                    OnPropertyChanged("CanWrite");
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

        private bool? _canWrite;
        public bool CanWrite
        {
            get
            {
                if (_canWrite == null)
                {
                    var myID = CurrentIdentity != null ? CurrentIdentity.ID : 0;
                    _canWrite = (Calendar.Owner != null && Calendar.Owner.ID == CurrentIdentity.ID)
                             || (Calendar.Writers.Any(w => w.ID == myID))
                             || (Calendar.GroupWriters.Any(grp => grp.Member.Any(w => w.ID == myID)));
                }

                return _canWrite.Value;
            }
        }
    }
}
