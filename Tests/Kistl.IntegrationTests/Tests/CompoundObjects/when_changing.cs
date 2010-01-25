using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Test;

using NUnit.Framework;

namespace Kistl.IntegrationTests.CompoundObjects
{
    [TestFixture]
    public class when_changing
        : Kistl.API.AbstractConsumerTests.CompoundObjects.when_changing
    {
        public override IKistlContext GetContext()
        {
            return KistlContext.GetContext();
        }
    }
}
