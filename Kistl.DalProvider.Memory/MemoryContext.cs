
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
            throw new NotImplementedException();
        }

        public override ImplementationType ToImplementationType(InterfaceType t)
        {
            return GetImplementationType(Type.GetType(t.Type.Name + Kistl.API.Helper.ImplementationSuffix + "," + Kistl.API.Helper.MemoryAssembly));
        }
    }
}
