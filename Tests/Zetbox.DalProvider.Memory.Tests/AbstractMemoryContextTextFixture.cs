
namespace Zetbox.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;
    using Zetbox.API;

    public class AbstractMemoryContextTextFixture 
        : Zetbox.API.AbstractConsumerTests.AbstractTestFixture
    {
        protected IZetboxContext GetMemoryContext()
        {
            return scope.Resolve<BaseMemoryContext>();
        }
    }
}
