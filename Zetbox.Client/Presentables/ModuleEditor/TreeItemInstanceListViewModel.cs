
namespace Zetbox.Client.Presentables.ModuleEditor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.App.Base;
    using Zetbox.Client.Presentables.ZetboxBase;

    public class TreeItemInstanceListViewModel : InstanceListViewModel
    {
        public new delegate TreeItemInstanceListViewModel Factory(IZetboxContext dataCtx, ViewModel parent/*, Func<IZetboxContext> workingCtxFactory // not needed, injected by AutoFac */, ObjectClass type, Func<IQueryable> qry);

        public TreeItemInstanceListViewModel(
            IViewModelDependencies appCtx,
            ZetboxConfig config,
            IFileOpener fileOpener,
            ITempFileService tmpService,
            IZetboxContext dataCtx, ViewModel parent,
            Func<IZetboxContext> workingCtxFactory,
            ObjectClass type,
            Func<IQueryable> qry)
            : base(appCtx, config, fileOpener, tmpService, dataCtx, parent, workingCtxFactory, type, qry)
        {
        }

        public override string Name
        {
            get
            {
                return base.DataType.Name;
            }
        }
    }
}
