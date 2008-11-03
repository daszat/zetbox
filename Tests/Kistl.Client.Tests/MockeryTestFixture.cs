using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NMock2;
using NUnit.Framework;

namespace Kistl.Tests
{
    public class MockeryTestFixture
    {
        protected Mockery m;

        [SetUp]
        public void SetUp()
        {
            m = new Mockery();
        }

        [TearDown]
        public void TearDown()
        {
            m.Dispose();
        }
    }
}
