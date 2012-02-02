namespace Kistl.App.Projekte.Client.ViewModel.Projekte
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.Client.Presentables;
    using Kistl.API;
    using Kistl.Client.Presentables.Calendar;
    using Kistl.App.GUI;

    [ViewModelDescriptor]
    public class TaskViewModel : DataObjectViewModel, IAppointmentViewModel
    {
        public new delegate TaskViewModel Factory(IKistlContext dataCtx, ViewModel parent, Task obj);

        public TaskViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, Task obj)
            : base(appCtx, dataCtx, parent, obj)
        {
        }

        public Task Task
        {
            get
            {
                return (Task)base.Object;
            }
        }

        public override string Name
        {
            get { return Task.Name; }
        }

        string IAppointmentViewModel.Subject
        {
            get { return Name; }
        }

        DateTime IAppointmentViewModel.From
        {
            get { return Task.DatumVon ?? DateTime.Today; }
        }

        DateTime IAppointmentViewModel.Until
        {
            get { return Task.DatumBis ?? DateTime.Today; }
        }

        string IAppointmentViewModel.Location
        {
            get { return string.Empty; }
        }

        string IAppointmentViewModel.Body
        {
            get { return string.Empty; }
        }

        private string _color;
        string IAppointmentViewModel.Color
        {
            get { return !string.IsNullOrEmpty(_color) ? _color : WeekCalendarViewModel.DefaultColor; }
            set { _color = value; OnPropertyChanged("Color"); }
        }

        ControlKind IAppointmentViewModel.RequestedCalendarKind
        {
            get { return null; } // default
        }

        private EventHandler _Changed;
        event EventHandler IAppointmentViewModel.Changed
        {
            add { _Changed += value; }
            remove { _Changed -= value; }
        }

        protected override void OnObjectPropertyChanged(string propName)
        {
            var temp = _Changed;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }
    }
}
