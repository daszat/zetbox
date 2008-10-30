using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Kistl.API.Tests
{
    public class TestApplicationContext : ApplicationContext
    {
        public TestApplicationContext(HostType type, string configFilePath)
            : base(type, configFilePath)
        {

        }
    }

    [TestFixture]
    public partial class ApplicationContextTests
    {

        [Test]
        public void InitServer()
        {
            var testCtx = new TestApplicationContext(HostType.Server, "");

            Assert.IsNotNull(ApplicationContext.Current);
        }

    }
}
