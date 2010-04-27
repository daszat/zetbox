using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API.Configuration;

namespace Kistl.API.Client.Mocks
{
    public class ClientApplicationContextMock : ClientApplicationContext
    {
        public static ClientApplicationContextMock CurrentMock { get; private set; }

        public ClientApplicationContextMock()
            : base()
        {
            ImplementationAssembly = Assembly.GetAssembly(this.GetType()).FullName;
            SetInterfaceAssembly_This();

            CurrentMock = this;
        }

        public void SetInterfaceAssembly_Objects()
        {
            InterfaceAssembly = Kistl.API.Helper.InterfaceAssembly;
        }

        public void SetInterfaceAssembly_This()
        {
            InterfaceAssembly = Assembly.GetAssembly(this.GetType()).FullName;
        }

        internal void ResetActionManager()
        {
            throw new NotImplementedException();
        }
    }
}
