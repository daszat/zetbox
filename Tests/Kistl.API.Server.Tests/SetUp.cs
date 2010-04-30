using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Server.Mocks;

using NUnit.Framework;
using Autofac;
using Kistl.API.Configuration;

namespace Kistl.API.Server.Tests
{
    internal class ServerApiAssemblyConfiguration : IAssemblyConfiguration
    {
        #region IAssemblyConfiguration Members

        public string InterfaceAssemblyName
        {
            get { return typeof(ServerApiAssemblyConfiguration).Assembly.FullName; }
        }

        public IEnumerable<string> AllImplementationAssemblyNames
        {
            get { return new[] { typeof(ServerApiAssemblyConfiguration).Assembly.FullName }; }
        }
        #endregion
    }

    [SetUpFixture]
    public class SetUp : AbstractConsumerTests.AbstractSetup
    {
        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);
            builder
                .RegisterType<ServerApiAssemblyConfiguration>()
                .As<IAssemblyConfiguration>()
                .SingleInstance();

            builder
                .RegisterType<MetaDataResolverMock>()
                .As<IMetaDataResolver>()
                .InstancePerDependency();

            builder.Register(c => new KistlContextMock(c.Resolve<IMetaDataResolver>(), null, c.Resolve<KistlConfig>(), c.Resolve<ITypeTransformations>()))
                .As<IKistlContext>()
                .As<IReadOnlyKistlContext>()
                .InstancePerDependency();
        }

        protected override string GetConfigFile()
        {
            return "Kistl.API.Server.Tests.Config.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Server;
        }
    }
}
