
namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;
    using NUnit.Framework;

    internal class ApiAssemblyConfiguration : IAssemblyConfiguration
    {
        #region IAssemblyConfiguration Members

        public string InterfaceAssemblyName
        {
            get { return typeof(Kistl.API.Mocks.TestDataObject).Assembly.FullName; }
        }

        public IEnumerable<string> AllImplementationAssemblyNames
        {
            get { return new[] { typeof(Kistl.API.Mocks.TestDataObject).Assembly.FullName }; }
        }
        #endregion
    }

    [SetUpFixture]
    public class SetUpFixture : AbstractConsumerTests.AbstractSetUpFixture
    {
        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);

            // Register missing Modules. HostType is None!
            builder.RegisterModule(new ApiModule());

            builder
                .RegisterType<ApiAssemblyConfiguration>()
                .As<IAssemblyConfiguration>()
                .SingleInstance();

            builder
                .RegisterType<Mocks.TestKistlContext>()
                .As<IKistlContext>()
                .InstancePerDependency();
        }

        protected override string GetConfigFile()
        {
            return "Kistl.API.Tests.Config.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.None;
        }

        protected override void SetUp(IContainer container)
        {
            base.SetUp(container);
            FrozenContext.RegisterTypeTransformations(container.Resolve<ITypeTransformations>());
        }
    }
}
