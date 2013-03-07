namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;

    [ViewModelDescriptor]
    public class NextEventsSummaryViewModel : ViewModel
    {
        public new delegate NextEventsSummaryViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public NextEventsSummaryViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        public override string Name
        {
            get { return "NextEventsSummaryViewModel"; }
        }
    }
}
