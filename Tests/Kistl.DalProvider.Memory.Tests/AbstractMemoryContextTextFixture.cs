
namespace Kistl.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;
    using Kistl.API;

    public class AbstractMemoryContextTextFixture 
        : Kistl.API.AbstractConsumerTests.AbstractTestFixture
    {
        protected IKistlContext GetMemoryContext()
        {
            return scope.Resolve<BaseMemoryContext>();
        }
    }
}
