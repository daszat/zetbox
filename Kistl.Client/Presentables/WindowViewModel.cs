using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.Client.Presentables
{
    public abstract class WindowViewModel : ViewModel
    {
        public new delegate WindowViewModel Factory(IKistlContext dataCtx);

        public WindowViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
        }

        public WindowViewModel(bool designMode)
            : base(designMode)
        {
        }

    }
}
