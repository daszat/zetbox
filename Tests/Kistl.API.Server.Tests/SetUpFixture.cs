
namespace Kistl.API.Server.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API.Common;
    using Kistl.API.Configuration;
    using Kistl.API.Server.Mocks;
    using NUnit.Framework;
    
    [SetUpFixture]
    public class SetUpFixture : AbstractConsumerTests.AbstractSetUpFixture
    {
        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);

            builder.RegisterModule(new Kistl.API.ApiModule());
            builder.RegisterModule(new Kistl.API.Server.ServerApiModule());

            builder
                .RegisterType<MetaDataResolverMock>()
                .As<IMetaDataResolver>()
                .InstancePerDependency();

            builder.Register(c => new KistlContextMock(c.Resolve<IMetaDataResolver>(), null, c.Resolve<KistlConfig>(), c.Resolve<Func<IFrozenContext>>(), c.Resolve<InterfaceType.Factory>()))
                .As<IKistlContext>()
                .As<IFrozenContext>()
                .As<IReadOnlyKistlContext>()
                .InstancePerDependency();
        }

        protected override string GetConfigFile()
        {
            return "Kistl.API.Server.Tests.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Server;
        }
    }
}
