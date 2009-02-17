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
            : base(KistlConfig.FromFile("DefaultConfig_API.Server.Tests.xml"))
        {
            SetCustomActionsManager(new CustomActionsManagerAPITest());
            ImplementationAssembly = InterfaceAssembly = Assembly.GetAssembly(typeof(Kistl.App.Base.Assembly__Implementation__)).FullName;
        }
    }
}
