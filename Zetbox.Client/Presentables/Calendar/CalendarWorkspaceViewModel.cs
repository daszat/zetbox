namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;

    [ViewModelDescriptor]
    public class CalendarWorkspaceViewModel : WindowViewModel
    {
        public new delegate CalendarWorkspaceViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public CalendarWorkspaceViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        public override string Name
        {
            get { return "Calendar Workspace"; }
        }

        #region Calendar
        private WeekCalendarViewModel _WeekCalender = null;
        public WeekCalendarViewModel WeekCalender
        {
            get
            {
                if (_WeekCalender == null)
                {
                    _WeekCalender = ViewModelFactory.CreateViewModel<WeekCalendarViewModel.Factory>()
                        .Invoke(DataContext, this, null);
                    _WeekCalender.NewItemCreating += (dt, e) =>
                    {
                    };
                    _WeekCalender.Refresh();
                }
                return _WeekCalender;
            }
        }
        #endregion
    }
}
