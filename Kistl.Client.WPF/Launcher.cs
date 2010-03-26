
namespace Kistl.Client.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API.Client;
    using Kistl.App.GUI;
    using Kistl.Client.Presentables;

    public static class Launcher
    {
        public static void Execute(IGuiApplicationContext appCtx, string[] args)
        {
            if (appCtx == null) { throw new ArgumentNullException("appCtx", "Missing GUI Application Context"); }

            var debugger = appCtx.Factory.CreateSpecificModel<KistlDebuggerAsModel>(KistlContext.GetContext());
            appCtx.Factory.ShowModel(debugger, true);

            bool _timeRecorder = args.Contains("-timerecorder");

            App.FixupDatabase();

            PresentableModel initialWorkspace;
            if (_timeRecorder)
            {
                initialWorkspace = appCtx.Factory.CreateSpecificModel<Kistl.Client.Presentables.TimeRecords.WorkEffortRecorderModel>(KistlContext.GetContext());
            }
            else
            {
                initialWorkspace = appCtx.Factory.CreateSpecificModel<WorkspaceModel>(KistlContext.GetContext());
            }

            // ugh?
            LauncherKind launcher = appCtx.TransientContext.Create<LauncherKind>();
            appCtx.Factory.ShowModel(initialWorkspace, launcher, true);

        }
    }
}
