
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

        public MemoryContext(ITypeTransformations typeTrans)
            : base(typeTrans)
        {
        }

        public override IQueryable<IPersistenceObject> GetPersistenceObjectQuery(InterfaceType ifType)
        {
            return (this.objects[ifType] ?? emptylist).AsQueryable().OfType<IPersistenceObject>();
        }

        public override int SubmitChanges()
        {
            throw new NotSupportedException();
        }

        protected override object CreateUnattachedInstance(InterfaceType ifType)
        {
            // TODO: replace with generated switch factory
            return Activator.CreateInstance(this.ToImplementationType(ifType).Type);
        }

        public override ImplementationType ToImplementationType(InterfaceType t)
        {
            // TODO: replace with generated switch factory
            return GetImplementationType(Type.GetType(t.Type.FullName + Kistl.API.Helper.ImplementationSuffix + "Memory," + MemoryProvider.GeneratedAssemblyName));
        }
    }
}
