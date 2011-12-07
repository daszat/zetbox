
namespace Kistl.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.API.Client.PerfCounter;
    using Kistl.API.Configuration;
    using Kistl.App.GUI;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.KistlBase;
    using Kistl.Client.Presentables.ObjectBrowser;
    using Kistl.API.Utils;
    using Kistl.API.Common;

    public class Launcher
    {
        private readonly Func<IKistlContext> ctxFactory;
        private readonly IViewModelFactory mdlFactory;
        private readonly IFrozenContext frozenCtx;
        private readonly KistlConfig cfg;
        private readonly IPerfCounter perfCounter;

        public Launcher(Func<IKistlContext> ctxFactory, IViewModelFactory mdlFactory, IFrozenContext frozenCtx, KistlConfig cfg, IPerfCounter perfCounter)
        {
            this.frozenCtx = frozenCtx;
            this.ctxFactory = ctxFactory;
            this.mdlFactory = mdlFactory;
            this.cfg = cfg;
            this.perfCounter = perfCounter;
        }

        public void Show(string[] args)
        {
            if (args == null) { throw new ArgumentNullException("args"); }

            if (args.Contains("-installperfcounter"))
            {
                if (perfCounter != null) perfCounter.Install();
            }
            else if (args.Contains("-uninstallperfcounter"))
            {
                if (perfCounter != null) perfCounter.Install();
            }
            else if (args.Length > 0)
            {
                var appGuid = new Guid(args[0]);
                LaunchApplication(appGuid);
            }
            else if (cfg.Client.Application != null && cfg.Client.Application != Guid.Empty)
            {
                LaunchApplication(cfg.Client.Application.Value);
            }
            else
            {
                var ws = mdlFactory.CreateViewModel<WorkspaceViewModel.Factory>().Invoke(ctxFactory.Invoke(), null);
                ControlKind launcher = Kistl.NamedObjects.Gui.ControlKinds.Kistl_App_GUI_LauncherKind.Find(frozenCtx);
                mdlFactory.ShowModel(ws, launcher, true);
            }


            //var ctxDebugger = mdlFactory.CreateViewModel<KistlDebuggerAsViewModel.Factory>().Invoke(ctxFactory());
            //mdlFactory.ShowModel(ctxDebugger, true);

            //var cacheDebugger = mdlFactory.CreateViewModel<CacheDebuggerViewModel.Factory>().Invoke(ctxFactory());
            //mdlFactory.ShowModel(cacheDebugger, true);
        }

        private void LaunchApplication(Guid appGuid)
        {
            var app = frozenCtx.FindPersistenceObject<Kistl.App.GUI.Application>(appGuid);
            var appMdl = mdlFactory.CreateViewModel<ApplicationViewModel.Factory>().Invoke(ctxFactory.Invoke(), null, app);
            appMdl.OpenApplication(appMdl);
        }
    }
}
