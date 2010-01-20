
namespace Kistl.DalProvider.EF.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;
    using Kistl.API;

    // TODO: remove this hack and replace with proper IoC test setups
    internal static class KistlContext
    {
        internal static IContainer Container { get; set; }

        internal static IKistlContext GetContext()
        {
            return Container.Resolve<IKistlContext>();
        }
    }
}
