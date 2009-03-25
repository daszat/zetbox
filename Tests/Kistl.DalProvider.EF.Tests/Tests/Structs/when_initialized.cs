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
    public class when_initialized
        : Kistl.API.AbstractConsumerTests.Structs.when_initialized
    {

        public override IKistlContext GetContext()
        {
            return KistlContext.GetContext();
        }
    }

}
