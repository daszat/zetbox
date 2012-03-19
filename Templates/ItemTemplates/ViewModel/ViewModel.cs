namespace $rootnamespace$
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.Client.Presentables;
    using Kistl.API;

    [ViewModelDescriptor]
    public class $safeitemname$ : ViewModel
    {
        public new delegate $safeitemname$ Factory(IKistlContext dataCtx, ViewModel parent);

        public $safeitemname$(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        public override string Name
        {
            get { return "$safeitemname$"; }
        }
    }
}
