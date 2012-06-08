namespace Kistl.Client.Presentables.DocumentManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using at.dasz.DocumentManagement;
    using Kistl.API;
    using Kistl.API.Configuration;

    [ViewModelDescriptor]
    public class StaticFileViewModel : FileViewModel
    {
        public new delegate StaticFileViewModel Factory(IKistlContext dataCtx, ViewModel parent, IDataObject obj);

        public StaticFileViewModel(
            IViewModelDependencies appCtx, KistlConfig config, IKistlContext dataCtx, ViewModel parent,
            File obj)
            : base(appCtx, config, dataCtx, parent, obj)
        {
        }
    }
}
