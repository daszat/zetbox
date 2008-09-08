using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Permissions;

namespace Kistl.API
{
    /// <summary>
    /// Host Type
    /// </summary>
    public enum HostType
    {
        /// <summary>
        /// Client Host
        /// </summary>
        Client,
        /// <summary>
        /// Server Host
        /// </summary>
        Server,
    }

    /// <summary>
    /// Loads configuration, Resolves Assemblies and cleans up invalid Assemblies
    /// </summary>
    [Serializable]
    public class APIInit : MarshalByRefObject
    {
        /// <summary>
        /// Current Host Type
        /// </summary>
        public static HostType HostType { get; private set; }

        public static string ImplementationAssembly { get; set; }

        public static Type BaseDataObjectType { get; private set; }
        public static Type BasePersistenceObjectType { get; private set; }
        public static Type BaseStructObjectType { get; private set; }
        public static Type BaseCollectionEntryType { get; private set; }

        /// <summary>
        /// Uses the default configuration DefaultConfig.xml in the current directory
        /// </summary>
        /// <param name="type">Host Type</param>
        [SecurityPermission(SecurityAction.Demand, ControlAppDomain = true)]
        public void Init(HostType type)
        {
            Init(type, "");
        }

        /// <summary>
        /// Uses the given configuration file
        /// </summary>
        /// <param name="configFile">configuration file w/ or w/o path</param>
        /// <param name="type">Host Type</param>
        [SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
        public void Init(HostType type, string configFile)
        {
            HostType = type;
            ImplementationAssembly = "Kistl.Objects." + HostType;

            // Load Configuration
            Configuration.KistlConfig.Init(configFile);

            // Delete Assemblies
            System.IO.Directory.GetFiles(AssemblyLoader.TargetAssemblyFolder).ForEach<string>(f => System.IO.File.Delete(f));

            // Start resolving Assemblies
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(Kistl.API.AssemblyLoader.AssemblyResolve);
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(Kistl.API.AssemblyLoader.AssemblyResolve);

            // Set Base Types
            BaseDataObjectType = Type.GetType(string.Format("Kistl.API.{0}.Base{0}DataObject, Kistl.API.{0}", HostType), true);
            BasePersistenceObjectType = Type.GetType(string.Format("Kistl.API.{0}.Base{0}PersistenceObject, Kistl.API.{0}", HostType), true);
            BaseCollectionEntryType = Type.GetType(string.Format("Kistl.API.{0}.Base{0}CollectionEntry, Kistl.API.{0}", HostType), true);
            BaseStructObjectType = Type.GetType(string.Format("Kistl.API.{0}.Base{0}StructObject, Kistl.API.{0}", HostType), true);
        }
    }
}
