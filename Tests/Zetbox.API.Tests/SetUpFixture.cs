
namespace Zetbox.API
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
        protected override void SetupBuilder(ContainerBuilder builder)
        {
            base.SetupBuilder(builder);

            // Register missing Modules. HostType is None!
            builder.RegisterModule(new ApiModule());

            builder
                .RegisterType<Mocks.TestZetboxContext>()
                .As<IZetboxContext>()
                .InstancePerDependency();
        }

        protected override string GetConfigFile()
        {
            return "Zetbox.API.Tests.xml";
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
