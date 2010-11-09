using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Client.Presentables.KistlBase;
using Kistl.API;
using Kistl.App.Base;
using Kistl.API.Configuration;

namespace Kistl.Client.Presentables.ModuleEditor
{
    public class TreeItemInstanceListViewModel : InstanceListViewModel
    {
        public new delegate TreeItemInstanceListViewModel Factory(IKistlContext dataCtx, ObjectClass type, IQueryable qry);

        public TreeItemInstanceListViewModel(
            IViewModelDependencies appCtx,
            KistlConfig config,
            IKistlContext dataCtx,
            ObjectClass type,
            IQueryable qry,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, config, dataCtx, type, qry, ctxFactory)
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
