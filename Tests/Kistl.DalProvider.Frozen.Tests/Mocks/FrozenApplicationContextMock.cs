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
        }

        internal void SetAssemblies(string p)
        {
            InterfaceAssembly = ImplementationAssembly = p;
        }

        public override void LoadFrozenActions(IKistlContext ctx)
        {
            throw new NotImplementedException();
        }
    }
}
