namespace $rootnamespace$
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;

    [ViewModelDescriptor]
    public class $safeitemname$ : ViewModel
    {
        public new delegate $safeitemname$ Factory(IZetboxContext dataCtx, ViewModel parent);

        public $safeitemname$(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        public override string Name
        {
            get { return "$safeitemname$"; }
        }
    }
}
