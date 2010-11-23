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
    public class FileViewModel : DataObjectViewModel
    {
        public new delegate FileViewModel Factory(IKistlContext dataCtx, IDataObject obj);

        public FileViewModel(
            IViewModelDependencies appCtx, KistlConfig config, IKistlContext dataCtx,
            File obj)
            : base(appCtx, config, dataCtx, obj)
        {
            this.File = obj;
        }

        public File File { get; private set; }
    }
}
