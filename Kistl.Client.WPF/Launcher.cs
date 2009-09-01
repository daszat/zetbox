
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
            bool _timeRecorder = args.Contains("-timerecorder");

            //var debugger = AppContext.Factory.CreateSpecificModel<KistlDebuggerAsModel>(KistlContext.GetContext());
            //AppContext.Factory.ShowModel(debugger, true);

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
