using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Client.Mocks;

using NUnit.Framework;
using Autofac;

namespace Kistl.API.Client.Tests
{
    [SetUpFixture]
    public class SetUp : AbstractConsumerTests.AbstractSetup
    {
        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);
            builder.RegisterType<TestProxy>()
                .As<IProxy>()
                .InstancePerDependency();
        }

        protected override string GetConfigFile()
        {
            return "Kistl.API.Client.Tests.Config.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Client;
        }
    }
}
