using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.AbstractConsumerTests;

using NUnit.Framework;

namespace Kistl.IntegrationTests
{
    [TestFixture]
    public class ListPropertiesTests
        : AbstractListPropertiesTests
    {
        protected override Kistl.API.IKistlContext GetContext()
        {
            return Kistl.API.Client.KistlContext.GetContext();
        }
    }
}
