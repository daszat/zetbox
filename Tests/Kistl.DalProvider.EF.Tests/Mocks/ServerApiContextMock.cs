using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API.Configuration;
using Kistl.API.Server;

namespace Kistl.DalProvider.EF.Mocks
{
    class ServerApiContextMock : ServerApiContext
    {
        public ServerApiContextMock()
            : base(KistlConfig.FromFile("Kistl.DalProvider.EF.Tests.Config.xml"))
        {
            SetCustomActionsManager(new CustomActionsManagerAPITest());
            InterfaceAssembly = Assembly.GetAssembly(typeof(Kistl.App.Base.Assembly)).FullName;
            ImplementationAssembly = Assembly.GetAssembly(typeof(Kistl.App.Base.Assembly__Implementation__)).FullName;
        }
    }
}
