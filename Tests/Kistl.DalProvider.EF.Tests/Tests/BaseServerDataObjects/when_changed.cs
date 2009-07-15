using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;

using NUnit.Framework;

namespace Kistl.DalProvider.EF.Tests.BaseServerDataObjects
{
    [TestFixture]
    public class when_changed
        : Kistl.API.AbstractConsumerTests.PersistenceObjects.when_changed
    {
        protected override IKistlContext GetContext()
        {
            return KistlContext.GetContext();
        }
    }
}
