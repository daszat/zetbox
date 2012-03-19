namespace $rootnamespace$
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.GUI;
    using Kistl.App.GUI;

    [ViewModelDescriptor]
    public class $safeitemname$ : NavigationScreenViewModel
    {
        public new delegate $safeitemname$ Factory(IKistlContext dataCtx, NavigationScreen screen);

        private readonly Func<IKistlContext> _ctxFactory;

        public $safeitemname$(IViewModelDependencies appCtx, Func<IKistlContext> ctxFactory,
            IKistlContext dataCtx, NavigationScreen screen)
            : base(appCtx, dataCtx, screen)
        {
            _ctxFactory = ctxFactory;
        }
    }
}
