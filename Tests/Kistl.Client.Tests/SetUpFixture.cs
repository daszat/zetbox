using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Autofac;
using Kistl.API;
using Kistl.API.Client;
using Kistl.Client.Mocks;

namespace Kistl
{
    [SetUpFixture]
    public class SetUpFixture : Kistl.API.AbstractConsumerTests.AbstractSetUpFixture
    {
        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);

            builder.RegisterType<KistlMockFactory>()
                .As<Kistl.Client.Mocks.KistlMockFactory>()
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
            return "Kistl.Client.Tests.xml";
        }

        protected override Kistl.API.HostType GetHostType()
        {
            return Kistl.API.HostType.Client;
        }
    }
}
