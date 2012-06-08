namespace Kistl.App.Projekte.Client.ViewModel.Projekte
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.GUI;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.Calendar;
    using ControlKinds = Kistl.NamedObjects.Gui.ControlKinds;

    [ViewModelDescriptor]
    public class CalendarTaskViewModel : WeekCalendarViewModel
    {
        public new delegate CalendarTaskViewModel Factory(IKistlContext dataCtx, ViewModel parent);

        public CalendarTaskViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent, null)
        {
            base.Source = GetData;
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
            var rules = DataContext.GetQuery<Kistl.App.Calendar.YearlyCalendarRule>().Where(r => r.IsWorkingDay == false)
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
