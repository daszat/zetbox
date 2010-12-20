
namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;
    using NUnit.Framework;

    [SetUpFixture]
    public class SetUpFixture : AbstractConsumerTests.AbstractSetUpFixture
    {
        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);

            // Register missing Modules. HostType is None!
            builder.RegisterModule(new ApiModule());

            builder
                .RegisterType<Mocks.TestKistlContext>()
                .As<IKistlContext>()
                .InstancePerDependency();
        }

        protected override string GetConfigFile()
        {
            return "Kistl.API.Tests.Config{0}.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.None;
        }

        protected override void SetUp(IContainer container)
        {
            base.SetUp(container);
        }
    }
}
