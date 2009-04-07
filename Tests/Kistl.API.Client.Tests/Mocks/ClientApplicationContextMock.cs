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
        public static ClientApplicationContextMock CurrentMock { get; private set; }

        public ClientApplicationContextMock()
            : base(KistlConfig.FromFile("Kistl.API.Client.Tests.Config.xml"))
        {
            SetCustomActionsManager(new CustomActionsManagerAPITest());

            ImplementationAssembly = Assembly.GetAssembly(this.GetType()).FullName;
            SetInterfaceAssembly_This();

            CurrentMock = this;
        }

        public void SetInterfaceAssembly_Objects()
        {
            InterfaceAssembly = "Kistl.Objects";
        }

        public void SetInterfaceAssembly_This()
        {
            InterfaceAssembly = Assembly.GetAssembly(this.GetType()).FullName;
        }

    }
}
