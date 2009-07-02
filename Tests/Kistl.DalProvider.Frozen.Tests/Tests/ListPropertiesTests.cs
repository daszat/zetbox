using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.AbstractConsumerTests;
using NUnit.Framework;

namespace Kistl.DalProvider.Frozen.Tests
{
    [Ignore("Needs Frozen value list property")]
    public class ListPropertiesTests : AbstractReadonlyListPropertiesTests
    {
        protected override Kistl.API.IKistlContext GetContext()
        {
            //return new Kistl.App.FrozenContextImplementation();
            throw new NotImplementedException();
        }
    }
}
