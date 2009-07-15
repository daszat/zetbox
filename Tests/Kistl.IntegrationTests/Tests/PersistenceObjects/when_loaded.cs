using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;

using NUnit.Framework;
using Kistl.App.Test;

namespace Kistl.IntegrationTests.PersistenceObjects
{
    [TestFixture]
    public class when_loaded
        : Kistl.API.AbstractConsumerTests.PersistenceObjects.when_loaded
    {
        protected override IKistlContext GetContext()
        {
            return KistlContext.GetContext();
        }
    }
}
