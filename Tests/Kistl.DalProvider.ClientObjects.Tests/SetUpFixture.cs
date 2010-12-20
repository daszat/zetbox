
namespace Kistl.DalProvider.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Autofac;

    using Kistl.API;
    using Kistl.API.AbstractConsumerTests;
    using Kistl.API.Client;
    using Kistl.API.Configuration;
    using Kistl.App.Extensions;
    using Kistl.DalProvider.Client.Mocks;

    using NUnit.Framework;

    [SetUpFixture]
    public class SetUpFixture 
        : AbstractSetUpFixture
    {

        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);
            builder.RegisterModule(new Kistl.API.ApiModule());
            builder.RegisterModule(new Kistl.API.Client.ClientApiModule());
            builder.RegisterModule(new Kistl.Client.ClientModule());
            builder.RegisterModule(new Kistl.DalProvider.Client.ClientProvider());
            builder.RegisterModule(new Kistl.DalProvider.Memory.MemoryProvider());
            builder.RegisterModule(new Kistl.Objects.InterfaceModule());
            builder.RegisterModule(new Kistl.Objects.MemoryModule());

            builder
                .Register(c => new ProxyMock(c.Resolve<InterfaceType.Factory>()))
                .As<IProxy>()
                .InstancePerDependency();
        }

        protected override string GetConfigFile()
        {
            return "Kistl.DalProvider.ClientObjects.Tests.Config{0}.xml";
        }

        protected override Kistl.API.HostType GetHostType()
        {
            return Kistl.API.HostType.Client;
        }

    }
}
