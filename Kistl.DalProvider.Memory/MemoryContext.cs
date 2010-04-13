using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.DalProvider.Memory
{
    public class MemoryContext
        : BaseMemoryContext
    {
        public MemoryContext()
            : base(System.Reflection.Assembly.ReflectionOnlyLoad(Kistl.API.Helper.InterfaceAssembly),
                System.Reflection.Assembly.ReflectionOnlyLoad(ServerProvider.GeneratedAssemblyName))
        {
        }

        public override int SubmitChanges()
        {
            throw new NotSupportedException();
        }

        protected override object CreateUnattachedInstance(InterfaceType ifType)
        {
            throw new NotImplementedException();
        }
    }
}
