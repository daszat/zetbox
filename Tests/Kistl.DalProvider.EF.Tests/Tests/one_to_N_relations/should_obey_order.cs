using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;

using NUnit.Framework;

namespace Kistl.DalProvider.EF.Tests.one_to_N_relations
{

    [TestFixture]
    public class should_obey_order 
        : Kistl.API.AbstractConsumerTests.one_to_N_relations.should_obey_order
    {
        protected override IKistlContext GetContext()
        {
            return Kistl.API.Server.KistlContext.GetContext();
        }
    }

}
