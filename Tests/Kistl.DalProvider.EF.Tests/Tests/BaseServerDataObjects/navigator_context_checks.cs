using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Kistl.API.Server;
using Kistl.API;

namespace Kistl.DalProvider.EF.Tests.BaseServerDataObjects
{
    [TestFixture]
    public class navigator_context_checks
        : Kistl.API.AbstractConsumerTests.PersistenceObjects.navigator_context_checks
    {
        protected override IKistlContext GetContext()
        {
            return KistlContext.GetContext();
        }
    }
}
