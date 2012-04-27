
namespace Kistl.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;

    public sealed class FrozenMemoryContext
       : MemoryContext, IFrozenContext
    {
        public FrozenMemoryContext(InterfaceType.Factory iftFactory, Func<IFrozenContext> lazyCtx, MemoryImplementationType.MemoryFactory implTypeFactory)
            : base(iftFactory, lazyCtx, implTypeFactory)
        {
        }

        private bool _sealed = false;
        public override bool IsReadonly
        {
            get
            {
                return _sealed;
            }
        }

        internal void Seal()
        {
            _sealed = true;
        }
    }
}
