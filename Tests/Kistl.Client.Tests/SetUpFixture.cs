using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Autofac;
using Kistl.API;

namespace Kistl
{
    [SetUpFixture]
    public class SetUpFixture : Kistl.API.AbstractConsumerTests.AbstractSetUpFixture
    {
        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);

            builder.RegisterType<Kistl.Client.Mocks.KistlMockFactory>()
                .As<Kistl.Client.Mocks.KistlMockFactory>()
                .InstancePerLifetimeScope();

            builder.Register(c => Kistl.Client.Mocks.KistlMockFactory.CreateContext())
                .As<IKistlContext>()
                .InstancePerDependency();
        }

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
