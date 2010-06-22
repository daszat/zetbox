
namespace Kistl.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using Kistl.API;

    public class MemoryContext
        : BaseMemoryContext
    {
        private static readonly List<IPersistenceObject> emptylist = new List<IPersistenceObject>(0);
        private readonly Func<IReadOnlyKistlContext> _lazyCtx;
             
        public MemoryContext(ITypeTransformations typeTrans, Func<IReadOnlyKistlContext> lazyCtx)
            : base(typeTrans)
        {
            _lazyCtx = lazyCtx;
        }

        public override int SubmitChanges()
        {
            throw new NotSupportedException();
        }

        protected override object CreateUnattachedInstance(InterfaceType ifType)
        {
            // TODO: replace with generated switch factory
            return Activator.CreateInstance(this.ToImplementationType(ifType).Type, _lazyCtx);
        }

        public override ImplementationType ToImplementationType(InterfaceType t)
        {
            // TODO: replace with generated switch factory
            return GetImplementationType(Type.GetType(t.Type.FullName + Kistl.API.Helper.ImplementationSuffix + "Memory," + MemoryProvider.GeneratedAssemblyName));
        }
    }
}
