#if OBSOLETE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.Client.Presentables
{
    public class LauncherModel
        : ViewModel
    {
        public LauncherModel(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
        }

        public override string Name
        {
            get { return "Launcher"; }
        }
    }
}
#endif