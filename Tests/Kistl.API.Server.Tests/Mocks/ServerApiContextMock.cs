using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API.Configuration;

namespace Kistl.API.Server.Mocks
{
    class ServerApiContextMock : ServerApiContext
    {
        public ServerApiContextMock()
            : base(KistlConfig.FromFile("Kistl.API.Server.Tests.Config.xml"))
        {
            ImplementationAssembly = InterfaceAssembly = Assembly.GetAssembly(this.GetType()).FullName;
        }

        public override void LoadFrozenActions(IReadOnlyKistlContext ctx)
        {
            throw new NotImplementedException();
        }
    }
}
