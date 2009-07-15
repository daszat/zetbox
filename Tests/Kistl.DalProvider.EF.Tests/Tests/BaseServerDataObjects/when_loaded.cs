using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;

using NUnit.Framework;

namespace Kistl.DalProvider.EF.Tests.BaseServerDataObjects
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
