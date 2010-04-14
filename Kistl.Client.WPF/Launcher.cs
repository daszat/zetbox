
namespace Kistl.Client.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API.Client;
    using Kistl.App.GUI;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.ObjectBrowser;
    using Kistl.API;

    public class Launcher
    {
        private readonly IGuiApplicationContext appCtx;
        private readonly IKistlContext ctx;
        private readonly Func<IKistlContext> ctxFactory;
        public Launcher(IGuiApplicationContext appCtx, IKistlContext ctx, Func<IKistlContext> ctxFactory)
        {
            this.appCtx = appCtx;
            this.ctx = ctx;
            this.ctxFactory = ctxFactory;
        }

        public void Show(string[] args)
        {
            if (args == null) { throw new ArgumentNullException("args"); }

            var ctxDebugger = appCtx.Factory.CreateViewModel<KistlDebuggerAsModel.Factory>().Invoke(ctxFactory());
            appCtx.Factory.ShowModel(ctxDebugger, true);

            var cacheDebugger = appCtx.Factory.CreateViewModel<CacheDebuggerViewModel.Factory>().Invoke(ctxFactory());
            appCtx.Factory.ShowModel(cacheDebugger, true);

            bool _timeRecorder = args.Contains("-timerecorder");

            App.FixupDatabase();

            ViewModel initialWorkspace;
            if (_timeRecorder)
            {
                initialWorkspace = appCtx.Factory.CreateViewModel<Kistl.Client.Presentables.TimeRecords.WorkEffortRecorderModel.Factory>().Invoke(ctxFactory.Invoke());
            }
            else
            {
                initialWorkspace = appCtx.Factory.CreateViewModel<WorkspaceViewModel.Factory>().Invoke(ctxFactory.Invoke());
            }

            LauncherKind launcher = ctx.Create<LauncherKind>();
            appCtx.Factory.ShowModel(initialWorkspace, launcher, true);
        }
    }
}
