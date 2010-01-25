using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;

using NUnit.Framework;

namespace Kistl.DalProvider.ClientObjects.Tests.CompoundObjects
{
    [TestFixture]
    [Ignore("Needs mocked IKistlService and IKistlServiceStreams")]
    public class when_roundtrip
        : Kistl.API.AbstractConsumerTests.CompoundObjects.when_roundtrip
    {
        public override IKistlContext GetContext()
        {
            return KistlContext.GetContext();
        }
    }
}
