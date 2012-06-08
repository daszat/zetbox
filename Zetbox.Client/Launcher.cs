
namespace Zetbox.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Client.PerfCounter;
    using Zetbox.API.Configuration;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ZetboxBase;
    using Zetbox.Client.Presentables.ObjectBrowser;
    using Zetbox.API.Utils;
    using Zetbox.API.Common;

    public class Launcher
    {
        private readonly Func<ClientIsolationLevel, IZetboxContext> ctxFactory;
        private readonly IViewModelFactory mdlFactory;
        private readonly IFrozenContext frozenCtx;
        private readonly ZetboxConfig cfg;
        private readonly IPerfCounter perfCounter;

        public Launcher(Func<ClientIsolationLevel, IZetboxContext> ctxFactory, IViewModelFactory mdlFactory, IFrozenContext frozenCtx, ZetboxConfig cfg, IPerfCounter perfCounter)
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
                var ws = mdlFactory.CreateViewModel<WorkspaceViewModel.Factory>().Invoke(ctxFactory(ClientIsolationLevel.MergeServerData), null);
                ControlKind launcher = Zetbox.NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_LauncherKind.Find(frozenCtx);
                mdlFactory.ShowModel(ws, launcher, true);
            }


            //var ctxDebugger = mdlFactory.CreateViewModel<ZetboxDebuggerAsViewModel.Factory>().Invoke(ctxFactory());
            //mdlFactory.ShowModel(ctxDebugger, true);

            //var cacheDebugger = mdlFactory.CreateViewModel<CacheDebuggerViewModel.Factory>().Invoke(ctxFactory());
            //mdlFactory.ShowModel(cacheDebugger, true);
        }

        private void LaunchApplication(Guid appGuid)
        {
            var app = frozenCtx.FindPersistenceObject<Zetbox.App.GUI.Application>(appGuid);
            var appMdl = mdlFactory.CreateViewModel<ApplicationViewModel.Factory>().Invoke(ctxFactory(ClientIsolationLevel.MergeServerData), null, app);
            appMdl.OpenApplication(appMdl);
        }
    }
}
