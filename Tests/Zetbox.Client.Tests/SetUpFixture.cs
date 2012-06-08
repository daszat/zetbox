using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Autofac;
using Zetbox.API;
using Zetbox.API.Client;
using Zetbox.Client.Mocks;

namespace Zetbox
{
    [SetUpFixture]
    public class SetUpFixture : Zetbox.API.AbstractConsumerTests.AbstractSetUpFixture
    {
        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);

            builder.RegisterType<ZetboxMockFactory>()
                .As<Zetbox.Client.Mocks.ZetboxMockFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TestViewModelFactory>()
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterType<NullIdentityResolver>()
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterType<TestProxy>()
                .As<IProxy>()
                .InstancePerDependency();
        }

        protected override string GetConfigFile()
        {
            return "Zetbox.Client.Tests.xml";
        }

        protected override Zetbox.API.HostType GetHostType()
        {
            return Zetbox.API.HostType.Client;
        }
    }
}
