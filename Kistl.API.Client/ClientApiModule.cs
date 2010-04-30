using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using System.Reflection;
using Kistl.API.Configuration;

namespace Kistl.API.Client
{
    internal class ClientAssemblyConfiguration : IAssemblyConfiguration
    {
        #region IAssemblyConfiguration Members

        public string InterfaceAssemblyName
        {
            get { return Kistl.API.Helper.InterfaceAssembly; }
        }

        public IEnumerable<string> AllImplementationAssemblyNames
        {
            get { return new[] { Kistl.API.Helper.ClientAssembly, Kistl.API.Helper.FrozenAssembly, Kistl.API.Helper.MemoryAssembly }; }
        }
        #endregion
    }

    public sealed class ClientApiModule
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.Register(c => new KistlContextImpl(c.Resolve<KistlConfig>(), c.Resolve<ITypeTransformations>(), c.Resolve<IProxy>(), Kistl.API.Helper.ClientAssembly))
                .As<IKistlContext>()
                .As<IReadOnlyKistlContext>()
                .InstancePerDependency();

            moduleBuilder
                .RegisterType<ClientAssemblyConfiguration>()
                .As<IAssemblyConfiguration>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<ProxyImplementation>()
                .As<IProxy>();
        }
    }
}
