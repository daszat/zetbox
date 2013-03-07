namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;

    [ViewModelDescriptor]
    public class NewEventDialogViewModel : WindowViewModel
    {
        public new delegate NewEventDialogViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public NewEventDialogViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        public override string Name
        {
            get { return "NewEventDialogViewModel"; }
        }
    }
}
