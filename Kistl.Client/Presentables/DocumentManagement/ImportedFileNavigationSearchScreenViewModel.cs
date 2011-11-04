namespace Kistl.Client.Presentables.DocumentManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Extensions;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.GUI;
    using Kistl.App.GUI;
    using at.dasz.DocumentManagement;

    [ViewModelDescriptor]
    public class ImportedFileNavigationSearchScreenViewModel : NavigationSearchScreenViewModel
    {
        public new delegate ImportedFileNavigationSearchScreenViewModel Factory(IKistlContext dataCtx, ViewModel parent, NavigationScreen screen);

        public ImportedFileNavigationSearchScreenViewModel(IViewModelDependencies appCtx, Func<IKistlContext> ctxFactory,
            IKistlContext dataCtx, ViewModel parent, NavigationScreen screen)
            : base(appCtx, dataCtx, ctxFactory, parent, screen)
        {
            base.Type = typeof(ImportedFile).GetObjectClass(FrozenContext);
            base.ListViewModel.ViewMethod = InstanceListViewMethod.Details;
            base.ListViewModel.AllowAddNew = false;
        }
    }
}
