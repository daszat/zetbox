
namespace Zetbox.DalProvider.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Autofac;

    using Zetbox.API;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.API.Client;
    using Zetbox.API.Configuration;
    using Zetbox.App.Extensions;
    using Zetbox.DalProvider.Client.Mocks;

    using NUnit.Framework;

    [SetUpFixture]
    public class SetUpFixture 
        : AbstractSetUpFixture
    {

        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);
            builder.RegisterModule(new Zetbox.API.ApiModule());
            builder.RegisterModule(new Zetbox.API.Client.ClientApiModule());
            builder.RegisterModule(new Zetbox.API.Client.HttpClientModule());
            builder.RegisterModule(new Zetbox.Client.ClientModule());
            builder.RegisterModule(new Zetbox.DalProvider.Client.ClientProvider());
            builder.RegisterModule(new Zetbox.DalProvider.Memory.MemoryProvider());
            builder.RegisterModule(new Zetbox.Objects.InterfaceModule());
            builder.RegisterModule(new Zetbox.Objects.MemoryModule());

            builder
                .RegisterType<ProxyMock>()
                .As<IProxy>()
                .InstancePerLifetimeScope();
        }

        protected override string GetConfigFile()
        {
            return "Zetbox.DalProvider.ClientObjects.Tests.xml";
        }

        protected override Zetbox.API.HostType GetHostType()
        {
            return Zetbox.API.HostType.Client;
        }

    }
}
