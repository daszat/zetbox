
namespace Kistl.Client.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.App.GUI;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.ObjectBrowser;
    using Kistl.Client.Presentables.KistlBase;

    public class Launcher
    {
        private readonly IKistlContext ctx;
        private readonly Func<IKistlContext> ctxFactory;
        private readonly IViewModelFactory mdlFactory;
        private readonly IFrozenContext frozenCtx;

        public Launcher(IKistlContext ctx, Func<IKistlContext> ctxFactory, IViewModelFactory mdlFactory, IFrozenContext frozenCtx)
        {
            this.ctx = ctx;
            this.frozenCtx = frozenCtx;
            this.ctxFactory = ctxFactory;
            this.mdlFactory = mdlFactory;
        }

        public void Show(string[] args)
        {
            if (args == null) { throw new ArgumentNullException("args"); }

            App.FixupDatabase(ctxFactory);

            if (args.Length > 0)
            {
                var appGuid = new Guid(args[0]);
                var app = frozenCtx.FindPersistenceObject<Kistl.App.GUI.Application>(appGuid);
                var appMdl = mdlFactory.CreateViewModel<ApplicationViewModel.Factory>().Invoke(ctxFactory.Invoke(), app);
                appMdl.OpenApplication(appMdl);
            }
            else
            {
                var ws = mdlFactory.CreateViewModel<WorkspaceViewModel.Factory>().Invoke(ctxFactory.Invoke());
                ControlKind launcher = frozenCtx.FindPersistenceObject<ControlKind>(NamedObjects.ControlKind_Kistl_App_GUI_LauncherKind);
                mdlFactory.ShowModel(ws, launcher, true);
            }


            //var ctxDebugger = mdlFactory.CreateViewModel<KistlDebuggerAsViewModel.Factory>().Invoke(ctxFactory());
            //mdlFactory.ShowModel(ctxDebugger, true);

            //var cacheDebugger = mdlFactory.CreateViewModel<CacheDebuggerViewModel.Factory>().Invoke(ctxFactory());
            //mdlFactory.ShowModel(cacheDebugger, true);
        }
    }
}
