using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.Presentables.ModuleEditor
{
    public class WorkspaceModel : PresentableModel
    {
        public WorkspaceModel(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            CurrentModule = dataCtx.GetQuery<Module>().FirstOrDefault();
        }

        public WorkspaceModel(IGuiApplicationContext appCtx, IKistlContext dataCtx, Module showMdl)
            : base(appCtx, dataCtx)
        {
            if (showMdl == null) throw new ArgumentNullException("showMdl");
            CurrentModule = dataCtx.Find<Module>(showMdl.ID);
        }

        public Module CurrentModule { get; set; }

        public override string Name
        {
            get { return "Module Editor Workspace"; }
        }
    }
}
