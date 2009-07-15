using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;

using NUnit.Framework;

namespace Kistl.DalProvider.ClientObjects.Tests.PersistenceObjects
{
    [TestFixture]
    [Ignore("Needs mocked IKistlService and IKistlServiceStreams")]
    public class when_changed
        : Kistl.API.AbstractConsumerTests.PersistenceObjects.when_changed
    {

        protected override IKistlContext GetContext()
        {
            return KistlContext.GetContext();
        }

    }

}
