
namespace Kistl.DalProvider.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.DalProvider.Base;

    public abstract class CompoundObjectClientImpl
        : CompoundObjectDefaultImpl
    {
        protected CompoundObjectClientImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }
    }
}
