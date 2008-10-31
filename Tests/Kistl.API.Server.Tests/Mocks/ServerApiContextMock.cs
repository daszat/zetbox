using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Kistl.API.Server.Mocks
{
    class ServerApiContextMock : ServerApiContext
    {
        public ServerApiContextMock()
            : base("DefaultConfig_API.Server.Tests.xml")
        {
            SetCustomActionsManager(new CustomActionsManagerAPITest());
            ImplementationAssembly = InterfaceAssembly = Assembly.GetAssembly(this.GetType()).FullName;
        }
    }
}
