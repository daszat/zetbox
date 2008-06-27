using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Permissions;

namespace Kistl.API
{
    public enum HostType
    {
        Client,
        Server,
    }

    /// <summary>
    /// Loads configuration, Resolves Assemblies and cleans up invalid Assemblies
    /// </summary>
    [Serializable]
    public class APIInit : MarshalByRefObject
    {
        public static HostType HostType { get; private set; }

        /// <summary>
        /// Uses the default configuration DefaultConfig.xml in the current directory
        /// </summary>
        [SecurityPermission(SecurityAction.Demand, ControlAppDomain = true)]
        public void Init(HostType type)
        {
            Init(type, "");
        }

        /// <summary>
        /// Uses the given configuration file
        /// </summary>
        /// <param name="configFile">configuration file w/ or w/o path</param>
        [SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain=true)]
        public void Init(HostType type, string configFile)
        {
            HostType = type;

            // Load Configuration
            Configuration.KistlConfig.Init(configFile);

            // Delete Assemblies
            System.IO.Directory.GetFiles(AssemblyLoader.TargetAssemblyFolder).ForEach<string>(f => System.IO.File.Delete(f));

            // Start resolving Assemblies
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(Kistl.API.AssemblyLoader.AssemblyResolve);
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(Kistl.API.AssemblyLoader.AssemblyResolve);
        }
    }
}
