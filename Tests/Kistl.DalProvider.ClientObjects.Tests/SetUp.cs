using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;


using NUnit.Framework;
using Kistl.API.Client;
using Kistl.DalProvider.ClientObjects.Mocks;
using Kistl.API.AbstractConsumerTests;
using Autofac;

namespace Kistl.DalProvider.ClientObjects.Tests
{
    [SetUpFixture]
    public class SetUp : AbstractSetup
    {

        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);
            builder.RegisterType<ProxyMock>()
                .As<IProxy>()
                .InstancePerDependency();
        }

        protected override string GetConfigFile()
        {
            return "Kistl.DalProvider.ClientObjects.Tests.Config.xml";
        }

        protected override Kistl.API.HostType GetHostType()
        {
            return Kistl.API.HostType.Client;
        }

    }
}
