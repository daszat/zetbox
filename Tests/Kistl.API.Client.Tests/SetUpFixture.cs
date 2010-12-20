
namespace Kistl.API.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;
    using Kistl.API.Client.Mocks;
    using Kistl.API.Configuration;
    using NUnit.Framework;

    [SetUpFixture]
    public class SetUpFixture : AbstractConsumerTests.AbstractSetUpFixture
    {
        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);

            builder.RegisterModule(new Kistl.API.ApiModule());
            builder.RegisterModule(new Kistl.API.Client.ClientApiModule());
            builder.RegisterModule(new Kistl.DalProvider.Memory.MemoryProvider());
        }

        protected override string GetConfigFile()
        {
            return "Kistl.API.Client.Tests.Config{0}.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Client;
        }
    }
}
