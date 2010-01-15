using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API.Configuration;

namespace Kistl.API.Mocks
{
    public class TestApplicationContext : ApplicationContext
    {
        public TestApplicationContext(string configfilename)
            : base(HostType.None, KistlConfig.FromFile(configfilename))
        {
            this.SetAssemblies(Assembly.GetAssembly(this.GetType()).FullName);
        }

        internal void SetAssemblies(string p)
        {
            InterfaceAssembly = ImplementationAssembly = p;
        }

        public override void LoadFrozenActions(IReadOnlyKistlContext ctx)
        {
            throw new NotImplementedException();
        }
    }
}
