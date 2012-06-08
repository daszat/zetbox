namespace Zetbox.App.Projekte.Client.ViewModel.Projekte
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables;

    [ViewModelDescriptor]
    public class CalendarWorkspaceViewModel : Zetbox.Client.Presentables.ObjectEditor.WorkspaceViewModel
    {
        public new delegate CalendarWorkspaceViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public CalendarWorkspaceViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
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
