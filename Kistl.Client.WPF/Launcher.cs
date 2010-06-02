
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
        private readonly IKistlContext ctx;
        private readonly Func<IKistlContext> ctxFactory;
        private readonly IModelFactory mdlFactory;

        public Launcher(IKistlContext ctx, Func<IKistlContext> ctxFactory, IModelFactory mdlFactory)
        {
            this.ctx = ctx;
            this.ctxFactory = ctxFactory;
            this.mdlFactory = mdlFactory;
        }

        public void Show(string[] args)
        {
            if (args == null) { throw new ArgumentNullException("args"); }

            bool _timeRecorder = args.Contains("-timerecorder");

            App.FixupDatabase(ctxFactory);

            ViewModel initialWorkspace;
            if (_timeRecorder)
            {
                initialWorkspace = mdlFactory.CreateViewModel<Kistl.Client.Presentables.TimeRecords.WorkEffortRecorderModel.Factory>().Invoke(ctxFactory.Invoke());
            }
            else
            {
                initialWorkspace = mdlFactory.CreateViewModel<WorkspaceViewModel.Factory>().Invoke(ctxFactory.Invoke());
            }

            ControlKind launcher = ctx.FindPersistenceObject<ControlKind>(new Guid("90D5FF7F-0C82-4278-BB8D-49C240F6BC2C"));
            mdlFactory.ShowModel(initialWorkspace, launcher, true);

            var ctxDebugger = mdlFactory.CreateViewModel<KistlDebuggerAsModel.Factory>().Invoke(ctxFactory());
            mdlFactory.ShowModel(ctxDebugger, true);

            var cacheDebugger = mdlFactory.CreateViewModel<CacheDebuggerViewModel.Factory>().Invoke(ctxFactory());
            mdlFactory.ShowModel(cacheDebugger, true);
        }
    }
}
