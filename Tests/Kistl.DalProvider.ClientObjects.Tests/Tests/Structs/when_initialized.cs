using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;

using NUnit.Framework;

namespace Kistl.DalProvider.ClientObjects.Tests.Structs
{
    [TestFixture]
    [Ignore("Needs mocked IKistlService and IKistlServiceStreams")]
    public class when_initialized
        : Kistl.API.AbstractConsumerTests.Structs.when_initialized
    {

        public override IKistlContext GetContext()
        {
            return KistlContext.GetContext();
        }

    }
}
