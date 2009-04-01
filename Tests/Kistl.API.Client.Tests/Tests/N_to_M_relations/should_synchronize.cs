using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;

using NUnit.Framework;

namespace Kistl.API.Client.Tests.N_to_M_relations
{

    [TestFixture]
    public class should_synchronize
        : Kistl.API.AbstractConsumerTests.N_to_M_relations.should_synchronize
    {
        protected override IKistlContext GetContext()
        {
            return Kistl.API.Client.KistlContext.GetContext();
        }

        [SetUp]
        public void InitTestObjects2()
        {
            Kistl.API.Client.Mocks.ClientApplicationContextMock.CurrentMock.SetInterfaceAssembly_Objects();
        }

        [TearDown]
        public void DisposeTestObjects2()
        {
            Kistl.API.Client.Mocks.ClientApplicationContextMock.CurrentMock.SetInterfaceAssembly_This();
        }
    }
}
