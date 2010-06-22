using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Client.Mocks;

using NUnit.Framework;
using Autofac;
using Kistl.API.Configuration;

namespace Kistl.API.Client.Tests
{
    internal class ClientApiAssemblyConfiguration : IAssemblyConfiguration
    {
        #region IAssemblyConfiguration Members

        public string InterfaceAssemblyName
        {
            get { return Kistl.API.Helper.InterfaceAssembly; }
        }

        public IEnumerable<string> AllImplementationAssemblyNames
        {
            get { return new[] { typeof(ClientApiAssemblyConfiguration).Assembly.FullName }; }
        }
        #endregion
    }

    [SetUpFixture]
    public class SetUpFixture : AbstractConsumerTests.AbstractSetUpFixture
    {
        protected override void SetupBuilder(Autofac.ContainerBuilder builder)
        {
            base.SetupBuilder(builder);
            builder.RegisterModule(new Kistl.API.ApiModule());
            builder.RegisterModule(new Kistl.API.Client.ClientApiModule());
            builder.RegisterModule(new Kistl.DalProvider.Memory.MemoryProvider());

            builder.RegisterType<TestProxy>()
                .As<IProxy>()
                .InstancePerDependency();

            builder
                .RegisterType<ClientApiAssemblyConfiguration>()
                .As<IAssemblyConfiguration>()
                .SingleInstance();

            builder.Register(c => new KistlContextImpl(
                    c.Resolve<KistlConfig>(),
                    c.Resolve<ITypeTransformations>(),
                    c.Resolve<IProxy>(),
                    typeof(Kistl.App.Test.TestObjClass__Implementation__).Assembly.FullName,
                    c.Resolve<Func<IReadOnlyKistlContext>>(Kistl.API.Helper.FrozenContextServiceName)))
                .As<IKistlContext>()
                .As<IReadOnlyKistlContext>()
                .Named<IReadOnlyKistlContext>(Kistl.API.Helper.FrozenContextServiceName)
                .InstancePerDependency();
        }

        protected override string GetConfigFile()
        {
            return "Kistl.API.Client.Tests.Config.xml";
        }

        protected override HostType GetHostType()
        {
            return HostType.Client;
        }
    }
}
