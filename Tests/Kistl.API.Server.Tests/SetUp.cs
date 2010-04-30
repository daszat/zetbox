using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Server.Mocks;

using NUnit.Framework;

namespace Kistl.API.Server.Tests
{
    [SetUpFixture]
    public class SetUp : AbstractConsumerTests.AbstractSetup
    {
        [SetUp]
        public void Init()
        {
        }

        protected override string GetConfigFile()
        {
            return "Kistl.API.Server.Tests.Config.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Server;
        }
    }
}
