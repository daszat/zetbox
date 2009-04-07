using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;

using NUnit.Framework;

namespace Kistl.DalProvider.EF.Tests.Structs
{

    [TestFixture]
    [Ignore("To be implemented")]
    public class when_changing_a_struct_member
        : Kistl.API.AbstractConsumerTests.Structs.when_changing_a_struct_member
    {

        public override IKistlContext GetContext()
        {
            return KistlContext.GetContext();
        }
    }

}
