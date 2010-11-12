
namespace Kistl.DalProvider.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    public abstract class CompoundObjectClientImpl
        : BaseCompoundObject
    {
        protected CompoundObjectClientImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }
    }
}
