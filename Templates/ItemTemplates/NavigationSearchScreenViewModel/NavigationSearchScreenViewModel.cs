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
    public class $safeitemname$ : NavigationSearchScreenViewModel
    {
        public new delegate $safeitemname$ Factory(IZetboxContext dataCtx, ViewModel parent, NavigationSearchScreen screen);

        public $safeitemname$(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, NavigationSearchScreen screen)
            : base(appCtx, dataCtx, parent, screen)
        {
        }
    }
}
