namespace Zetbox.Client.Presentables.DocumentManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using at.dasz.DocumentManagement;
    using Zetbox.API;
    using Zetbox.API.Configuration;

    [ViewModelDescriptor]
    public class DocumentViewModel : FileViewModel
    {
        public new delegate DocumentViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj);

        public DocumentViewModel(
            IViewModelDependencies appCtx, ZetboxConfig config, IZetboxContext dataCtx, ViewModel parent,
            File obj)
            : base(appCtx, config, dataCtx, parent, obj)
        {
        }
    }
}
