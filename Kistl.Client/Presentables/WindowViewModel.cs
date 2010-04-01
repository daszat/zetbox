using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.Client.Presentables
{
    public abstract class WindowViewModel : ViewModel
    {
        public WindowViewModel(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
        }
    }
}
