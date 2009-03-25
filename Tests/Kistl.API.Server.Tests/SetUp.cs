using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Server.Mocks;

using NUnit.Framework;

namespace Kistl.API.Server.Tests
{
    [SetUpFixture]
    public class SetUp
    {
        [SetUp]
        public void Init()
        {
            var testCtx = new ServerApiContextMock();
        }
    }
}
