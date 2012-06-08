
namespace Zetbox.API.Server.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server.Mocks;
    using NUnit.Framework;
    
    [SetUpFixture]
    public class SetUpFixture : AbstractConsumerTests.AbstractSetUpFixture
    {
        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);

            builder.RegisterModule(new Zetbox.API.ApiModule());
            builder.RegisterModule(new Zetbox.API.Server.ServerApiModule());

            builder
                .RegisterType<MetaDataResolverMock>()
                .As<IMetaDataResolver>()
                .InstancePerDependency();

            builder.Register(c => new ZetboxContextMock(c.Resolve<IMetaDataResolver>(), null, c.Resolve<ZetboxConfig>(), c.Resolve<Func<IFrozenContext>>(), c.Resolve<InterfaceType.Factory>()))
                .As<IZetboxContext>()
                .As<IFrozenContext>()
                .As<IReadOnlyZetboxContext>()
                .InstancePerDependency();
        }

        protected override string GetConfigFile()
        {
            return "Zetbox.API.Server.Tests.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Server;
        }
    }
}
