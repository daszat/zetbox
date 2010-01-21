using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;

using NUnit.Framework;

namespace Kistl.DalProvider.EF.Tests.CompoundObjects
{

    [TestFixture]
    public class when_changing_a_CompoundObject_member
        : Kistl.API.AbstractConsumerTests.CompoundObjects.when_changing_a_CompoundObject_member
    {
        public override IKistlContext GetContext()
        {
            return KistlContext.GetContext();
        }
    }
}
