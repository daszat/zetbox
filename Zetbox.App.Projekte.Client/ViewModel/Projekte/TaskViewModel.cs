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
