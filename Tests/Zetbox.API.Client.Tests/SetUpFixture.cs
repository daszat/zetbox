
namespace Zetbox.API.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;
    using Zetbox.API.Client.Mocks;
    using Zetbox.API.Configuration;
    using NUnit.Framework;

    [SetUpFixture]
    public class SetUpFixture : AbstractConsumerTests.AbstractSetUpFixture
    {
        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);

            builder.RegisterModule(new Zetbox.API.ApiModule());
            builder.RegisterModule(new Zetbox.API.Client.ClientApiModule());
            builder.RegisterModule(new Zetbox.API.Client.HttpClientModule());
            builder.RegisterModule(new Zetbox.DalProvider.Memory.MemoryProvider());
        }

        protected override string GetConfigFile()
        {
            return "Zetbox.API.Client.Tests.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Client;
        }
    }
}
