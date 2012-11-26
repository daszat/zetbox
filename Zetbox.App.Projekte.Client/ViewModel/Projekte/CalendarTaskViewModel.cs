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
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.Calendar;
    using ControlKinds = Zetbox.NamedObjects.Gui.ControlKinds;

    [ViewModelDescriptor]
    public class CalendarTaskViewModel : WeekCalendarViewModel
    {
        public new delegate CalendarTaskViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public CalendarTaskViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent, null)
        {
        }

        protected override Func<DateTime, DateTime, IEnumerable<IAppointmentViewModel>> GetSource()
        {
            return GetData;
        }

        #region Properties
        public override string Name
        {
            get { return "Project Calendar"; }
        }

        #endregion

        #region Calendar
        private IEnumerable<IAppointmentViewModel> GetData(DateTime from, DateTime to)
        {
            using (Logging.Client.InfoTraceMethodCall("CalendarTaskViewModel.GetData()"))
            {
                var result = new List<IAppointmentViewModel>();
                FetchTasks(from, to, result);
                FetchOffDays(from, to, result);
                return result;
            }
        }

        private void FetchTasks(DateTime from, DateTime to, List<IAppointmentViewModel> result)
        {
            result.AddRange(DataContext
                .GetQuery<Task>()
                .Where(t => (t.DatumVon >= from && t.DatumVon <= to) || (t.DatumBis >= from && t.DatumBis <= to) || (t.DatumVon <= from && t.DatumBis >= to))
                .ToList()
                .Select(t =>
                {
                    var vmdl = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this, t);
                    // vmdl.RequestedKind = muhblah;
                    return vmdl;
                })
                .Cast<IAppointmentViewModel>());
        }

        private void FetchOffDays(DateTime from, DateTime to, List<IAppointmentViewModel> result)
        {
            var rules = DataContext.GetQuery<Zetbox.App.Calendar.YearlyCalendarRule>().Where(r => r.IsWorkingDay == false)
                .ToList();
            var dt = from;
            while (dt <= to)
            {
                var localDt = dt;
                result.AddRange(
                    rules.Where(r => r.AppliesTo(dt))
                    .Select(r =>
                    {
                        return ViewModelFactory.CreateViewModel<CalendarRuleInstanceViewModel.Factory>()
                        .Invoke(
                            DataContext,
                            this,
                            r,
                            localDt);
                    })
                    .Cast<IAppointmentViewModel>()
                );
                dt = dt.AddDays(1);
            }
        }
        #endregion
    }
}
