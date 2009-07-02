using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Configuration;
using Kistl.App;

namespace Kistl.DalProvider.Frozen.Mocks
{
    public class FrozenApplicationContextMock 
        : ApplicationContext
    {
        public FrozenApplicationContextMock()
            : base(HostType.None, KistlConfig.FromFile("Kistl.DalProvider.Frozen.Tests.Config.xml"))
        {
            //this.SetAssemblies(typeof(FrozenContextImplementation).Assembly.FullName);
            throw new NotImplementedException();
        }

        internal void SetAssemblies(string p)
        {
            InterfaceAssembly = ImplementationAssembly = p;
        }
    }
}
