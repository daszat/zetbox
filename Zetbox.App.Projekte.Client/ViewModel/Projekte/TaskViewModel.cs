
namespace Zetbox.App.Projekte.Client.ViewModel.Projekte
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.Calendar;

    [ViewModelDescriptor]
    public class TaskViewModel : DataObjectViewModel, IAppointmentViewModel
    {
        public new delegate TaskViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Task obj);

        public TaskViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Task obj)
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

        protected override void OnObjectPropertyChanged(string propName)
        {
            base.OnObjectPropertyChanged(propName);
            switch (propName)
            {
                case "Name":
                    OnPropertyChanged("Name");
                    OnPropertyChanged("Subject");
                    break;
                case "DatumVon":
                    OnPropertyChanged("From");
                    break;
                case "DatumBis":
                    OnPropertyChanged("Until");
                    break;
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
            get { return Task.DatumVon; }
        }

        DateTime IAppointmentViewModel.Until
        {
            get { return Task.DatumBis ?? Task.DatumVon.AddDays(1); }
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

    }
}
