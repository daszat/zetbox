using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API.Configuration;

namespace Kistl.API.Server.Mocks
{
    class ServerApiContextMock : ServerApplicationContext
    {
        public ServerApiContextMock()
            : base()
        {
            ImplementationAssembly = InterfaceAssembly = Assembly.GetAssembly(this.GetType()).FullName;
        }
    }
}
