using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Configuration;

namespace Kistl.API
{

    /// <summary>
    /// Which kind of host to be
    /// </summary>
    public enum HostType
    {
        Client,
        Server,
        /// <summary>
        /// no predefined personality. This is used only in very rare cases.
        /// </summary>
        None
    }

    /// <summary>
    /// Contains all application-wide information
    /// </summary>
    public abstract class ApplicationContext
    {
        public static ApplicationContext Current { get; private set; }

        public HostType HostType { get; private set; }

        /// <summary>
        /// The assembly containing the object implementations
        /// </summary>
        public string ImplementationAssembly { get; protected set; }
        /// <summary>
        /// The assembly containing the object interfaces
        /// </summary>
        public string InterfaceAssembly { get; protected set; }

        public Type BasePersistenceObjectType { get; protected set; }
        public Type BaseDataObjectType { get; protected set; }
        public Type BaseCompoundObjectType { get; protected set; }
        public Type BaseCollectionEntryType { get; protected set; }

        /// <summary>
        /// Create a context with the given HostType and load the configuration from 
        /// the given path. If no path is given, DefaultConfig.xml in the current 
        /// working directory is used.
        /// </summary>
        /// <param name="type">the type of the host application</param>
        protected ApplicationContext(HostType type)
        {
            HostType = type;

            // now the basic configuration is finished, therefore "publish" the appCtx
            ApplicationContext.Current = this;

            // Hardcode Interface and Implementation assemblies
            InterfaceAssembly = Kistl.API.Helper.InterfaceAssembly;
            switch(HostType)
            {
                case HostType.Client:
                    ImplementationAssembly = Kistl.API.Helper.ClientAssembly;
                    break;
                case HostType.Server:
                    ImplementationAssembly = Kistl.API.Helper.ServerAssembly;
                    break;
                case HostType.None:
                    ImplementationAssembly = "";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }
    }
}
