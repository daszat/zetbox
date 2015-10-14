// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

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
        private readonly Func<ContextIsolationLevel, IZetboxContext> ctxFactory;
        private readonly IViewModelFactory mdlFactory;
        private readonly IFrozenContext frozenCtx;
        private readonly ZetboxConfig cfg;
        private readonly IPerfCounter perfCounter;

        public Launcher(Func<ContextIsolationLevel, IZetboxContext> ctxFactory, IViewModelFactory mdlFactory, IFrozenContext frozenCtx, ZetboxConfig cfg, IPerfCounter perfCounter)
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
                // install and exit
            }
            else if (args.Contains("-uninstallperfcounter"))
            {
                if (perfCounter != null) perfCounter.Install();
                // uninstall and exit
            }
            else
            {
                // Extract app guid
                Guid appGuid = Guid.Empty;
                foreach (var g in args)
                {
                    if (Guid.TryParse(g, out appGuid))
                        break;
                }

                if (appGuid != Guid.Empty)
                {
                    LaunchApplication(appGuid);
                }
                else if (cfg.Client.Application != null && cfg.Client.Application != Guid.Empty)
                {
                    LaunchApplication(cfg.Client.Application.Value);
                }
                else
                {
                    var ws = mdlFactory.CreateViewModel<WorkspaceViewModel.Factory>().Invoke(ctxFactory(ContextIsolationLevel.MergeQueryData), null);
                    ControlKind launcher = Zetbox.NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_LauncherKind.Find(frozenCtx);
                    mdlFactory.ShowModel(ws, launcher, true);
                }

                // other arguments
                if (args.Contains("-showdebugger"))
                {
                    var ctx = ctxFactory(ContextIsolationLevel.PreferContextCache);

                    var ctxDebugger = mdlFactory.CreateViewModel<ZetboxDebuggerAsViewModel.Factory>().Invoke(ctx, null);
                    mdlFactory.ShowModel(ctxDebugger, true);

                    var cacheDebugger = mdlFactory.CreateViewModel<CacheDebuggerViewModel.Factory>().Invoke(ctx, null);
                    mdlFactory.ShowModel(cacheDebugger, true);
                }
            }
        }

        private void LaunchApplication(Guid appGuid)
        {
            var app = frozenCtx.FindPersistenceObject<Zetbox.App.GUI.Application>(appGuid);
            var appMdl = mdlFactory.CreateViewModel<ApplicationViewModel.Factory>().Invoke(ctxFactory(ContextIsolationLevel.MergeQueryData), null, app);
            appMdl.OpenApplication(appMdl);
        }
    }
}
