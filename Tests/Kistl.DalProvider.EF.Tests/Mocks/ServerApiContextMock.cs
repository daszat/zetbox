using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API;
using Kistl.API.Configuration;
using Kistl.API.Server;

namespace Kistl.DalProvider.EF.Mocks
{
    class ServerApiContextMock : ServerApplicationContext
    {
        public ServerApiContextMock()
            : base()
        {
            InterfaceAssembly = Assembly.GetAssembly(typeof(Kistl.App.Base.Assembly)).FullName;
            ImplementationAssembly = Assembly.GetAssembly(typeof(Kistl.App.Base.Assembly__Implementation__)).FullName;
        }
    }
}
