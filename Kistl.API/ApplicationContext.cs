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
    public abstract class ApplicationContext : IDisposable
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

        public KistlConfig Configuration { get; private set; }

        /// <summary>
        /// Loads an appropriate frozen custom actions manager for this IKistlContext.
        /// TODO: Remove that when AutoFac is implemented anywhere
        /// </summary>
        /// <param name="ctx"></param>
        public abstract void LoadFrozenActions(IReadOnlyKistlContext ctx);

        /// <summary>
        /// Create a context with the given HostType and load the configuration from 
        /// the given path. If no path is given, DefaultConfig.xml in the current 
        /// working directory is used.
        /// </summary>
        /// <param name="type">the type of the host application</param>
        /// <param name="config">use this configuration</param>
        protected ApplicationContext(HostType type, Configuration.KistlConfig config)
        {
            HostType = type;

            Configuration = config;

            // now the basic configuration is finished, therefore "publish" the appCtx
            ApplicationContext.Current = this;

            AssemblyLoader.Bootstrap(AppDomain.CurrentDomain, Configuration);

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

        #region IDisposable Members

        // as suggested on http://msdn.microsoft.com/en-us/system.idisposable.aspx
        // adapted for easier usage when inheriting, by naming the functions appropriately
        private bool disposed = false;
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // dispose managed resources
                    // must not be done when running from the finalizer
                    DisposeManagedResources();
                }
                // free native resources
                DisposeNativeResources();

                this.disposed = true;
            }
        }

        /// <summary>
        /// Override this to be called when Managed Resources should be disposed
        /// </summary>
        protected virtual void DisposeManagedResources()
        {
        }

        /// <summary>
        /// Override this to be called when Native Resources should be disposed
        /// </summary>
        protected virtual void DisposeNativeResources() { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ApplicationContext()
        {
            Dispose(false);
        }

        #endregion

    }
}
