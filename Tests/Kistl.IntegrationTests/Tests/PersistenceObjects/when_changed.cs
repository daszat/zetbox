using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;

using NUnit.Framework;

namespace Kistl.IntegrationTests.PersistenceObjects
{

    [TestFixture]
    public class when_changed
        : Kistl.API.AbstractConsumerTests.PersistenceObjects.when_changed
    {

        public override IKistlContext GetContext()
        {
            return KistlContext.GetContext();
        }

    }

}
