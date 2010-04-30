using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using System.Reflection;

namespace Kistl.API.Client
{
    internal class ClientAssemblyConfiguration : IAssemblyConfiguration
    {
        #region IAssemblyConfiguration Members

        public string InterfaceAssemblyName
        {
            get { return Kistl.API.Helper.InterfaceAssembly; }
        }

        public string ImplementationAssemblyName
        {
            get { return Kistl.API.Helper.ClientAssembly; }
        }

        public IEnumerable<string> AllImplementationAssemblyNames
        {
            get { return new[] { Kistl.API.Helper.ClientAssembly }; }
        }

        public Type BasePersistenceObjectType
        {
            get { return typeof(BaseClientPersistenceObject); }
        }

        public Type BaseDataObjectType
        {
            get { return typeof(BaseClientDataObject); }
        }

        public Type BaseCompoundObjectType
        {
            get { return typeof(BaseClientCompoundObject); }
        }

        public Type BaseCollectionEntryType
        {
            get { return typeof(BaseClientCollectionEntry); }
        }

        #endregion
    }

    public sealed class ClientApiModule
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterType<KistlContextImpl>()
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
