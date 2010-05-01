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
using Kistl.API;
using Kistl.API.Configuration;
using Kistl.App.Extensions;

namespace Kistl.DalProvider.ClientObjects.Tests
{
    [SetUpFixture]
    public class SetUpFixture : AbstractSetUpFixture
    {

        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);
            builder.Register(c => new ProxyMock(c.Resolve<ITypeTransformations>()))
                .As<IProxy>()
                .InstancePerDependency();
        }

        protected override void SetUp(IContainer container)
        {
            base.SetUp(container);
            FrozenContext.RegisterTypeTransformations(container.Resolve<ITypeTransformations>());

            // initialise custom actions manager
            var cams = container.Resolve<BaseCustomActionsManager>();
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
