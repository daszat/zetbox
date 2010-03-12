using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Kistl.API.Client;
using Kistl.API;

namespace Kistl.IntegrationTests.Blobs
{
    [TestFixture]
    public class when_using
            : Kistl.API.AbstractConsumerTests.Blobs.when_using
    {
        public override IKistlContext GetContext()
        {
            return KistlContext.GetContext();
        }
    }
}
