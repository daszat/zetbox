namespace $rootnamespace$
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.App.GUI;

    [ViewModelDescriptor]
    public class $safeitemname$ : NavigationScreenViewModel
    {
        public new delegate $safeitemname$ Factory(IZetboxContext dataCtx, NavigationScreen screen);

        private readonly Func<IZetboxContext> _ctxFactory;

        public $safeitemname$(IViewModelDependencies appCtx, Func<IZetboxContext> ctxFactory,
            IZetboxContext dataCtx, NavigationScreen screen)
            : base(appCtx, dataCtx, screen)
        {
            _ctxFactory = ctxFactory;
        }
    }
}
