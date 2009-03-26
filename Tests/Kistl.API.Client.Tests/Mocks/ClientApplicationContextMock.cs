using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API.Configuration;

namespace Kistl.API.Client.Mocks
{
    public class ClientApplicationContextMock : ClientApiContext
    {
        public ClientApplicationContextMock()
            : base(KistlConfig.FromFile("Kistl.API.Client.Tests.Config.xml"))
        {
            SetCustomActionsManager(new CustomActionsManagerAPITest());
            ImplementationAssembly = InterfaceAssembly = Assembly.GetAssembly(this.GetType()).FullName;
        }
    }
}
