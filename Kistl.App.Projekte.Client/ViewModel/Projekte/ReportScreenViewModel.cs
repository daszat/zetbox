
namespace Kistl.App.Projekte.Client.ViewModel.Projekte
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.Client.Presentables.GUI;
    using Kistl.Client.Presentables;
    using Kistl.API;
    using Kistl.App.GUI;

    [ViewModelDescriptor]
    public class ReportScreenViewModel : NavigationReportScreenViewModel
    {
        public new delegate ReportScreenViewModel Factory(IKistlContext dataCtx, ViewModel parent, NavigationScreen screen);

        public ReportScreenViewModel(IViewModelDependencies appCtx, Func<IKistlContext> ctxFactory,
            IKistlContext dataCtx, ViewModel parent, NavigationScreen screen, IFileOpener fileOpener)
            : base(appCtx, ctxFactory, dataCtx, parent, screen, fileOpener)
        {
        }

        public class Report
        {
            public string Name { get; set; }
        }

        protected override object LoadStatistic(DateTime from, DateTime until)
        {
            return new Report() { Name = "Hello World" };
        }
    }
}
