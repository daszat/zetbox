
namespace Kistl.API.AbstractConsumerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public sealed class MockImplementationTypeChecker : IImplementationTypeChecker
    {
        public bool IsImplementationType(Type t)
        {
            return true;
        }
    }
}
