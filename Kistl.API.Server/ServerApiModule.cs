using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using System.Reflection;

namespace Kistl.API.Server
{
    internal class ServerAssemblyConfiguration : IAssemblyConfiguration
    {
        #region IAssemblyConfiguration Members

        public string InterfaceAssemblyName
        {
            get { return Kistl.API.Helper.InterfaceAssembly; }
        }

        public string ImplementationAssemblyName
        {
            get { return Kistl.API.Helper.ServerAssembly; }
        }

        public IEnumerable<string> AllImplementationAssemblyNames
        {
            get { return new[] { Kistl.API.Helper.ServerAssembly }; }
        }

        public Type BasePersistenceObjectType
        {
            get { return typeof(BaseServerPersistenceObject); }
        }

        public Type BaseDataObjectType
        {
            get { return typeof(BaseServerDataObject); }
        }

        public Type BaseCompoundObjectType
        {
            get { return typeof(BaseServerCompoundObject); }
        }

        public Type BaseCollectionEntryType
        {
            get { return typeof(BaseServerCollectionEntry); }
        }

        #endregion
    }

    public sealed class ServerApiModule
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .RegisterType<ServerAssemblyConfiguration>()
                .As<IAssemblyConfiguration>()
                .SingleInstance();
        }
    }
}
