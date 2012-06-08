namespace Kistl.App.Projekte.Client.ViewModel.Projekte
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.GUI;
    using Kistl.Client.Presentables;

    [ViewModelDescriptor]
    public class CalendarWorkspaceViewModel : Kistl.Client.Presentables.ObjectEditor.WorkspaceViewModel
    {
        public new delegate CalendarWorkspaceViewModel Factory(IKistlContext dataCtx, ViewModel parent);

        public CalendarWorkspaceViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
            base.ShowModel(CalendarViewModel);
        }

        private CalendarTaskViewModel _calendarViewModel;
        public CalendarTaskViewModel CalendarViewModel
        {
            get
            {
                if (_calendarViewModel == null)
                {
                    _calendarViewModel = ViewModelFactory.CreateViewModel<CalendarTaskViewModel.Factory>().Invoke(DataContext, this);
                }
                return _calendarViewModel;
            }
        }
    }
}
