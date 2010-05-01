using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

using Autofac;

namespace Kistl.DalProvider.Memory
{
    public class AbstractMemoryContextTextFixture : Kistl.API.AbstractConsumerTests.AbstractTestFixture
    {
        protected IKistlContext GetMemoryContext()
        {
            return scope.Resolve<BaseMemoryContext>();
        }
    }
}
