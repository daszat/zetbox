using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Kistl.API.Mocks
{
    public class TestApplicationContext : ApplicationContext
    {
        public TestApplicationContext()
            : base(HostType.None, "")
        {
            this.SetAssemblies(Assembly.GetAssembly(this.GetType()).FullName);
        }

        internal void SetAssemblies(string p)
        {
            InterfaceAssembly = ImplementationAssembly = p;
        }
    }
}
