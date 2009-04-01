using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Client.Mocks;

using NUnit.Framework;

namespace Kistl.API.Client.Tests
{
    [SetUpFixture]
    public class SetUp
    {
        [SetUp]
        public void Init()
        {
            var testCtx = new ClientApplicationContextMock();
            Proxy.SetProxy(new TestProxy());
        }
    }
}
