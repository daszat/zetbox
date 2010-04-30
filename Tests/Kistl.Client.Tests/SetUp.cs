using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Kistl
{
    [SetUpFixture]
    public class SetUp : Kistl.API.AbstractConsumerTests.AbstractSetup
    {
        protected override string GetConfigFile()
        {
            return "Kistl.Client.Tests.Config.xml";
        }

        protected override Kistl.API.HostType GetHostType()
        {
            return Kistl.API.HostType.Client;
        }
    }
}
