using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Kistl.API.Configuration;
using System.Diagnostics;

namespace Kistl.API
{
    /// <summary>
    /// This is the Assembly Loader. Assemblies are obtained by you configurated Assembly sources.
    /// Assemblies are copied to your WorkingFolder\bin directory. 
    /// eg. C:\Users\Arthur\AppData\Local\dasz\Kistl\Arthur's Configuration\Kistl.Client.exe\bin.
    /// </summary>
    // This does not seem to be the best solution. But it works.
    // See http://blogs.msdn.com/suzcook/archive/2003/05/29/57143.aspx
    // In the long term, we want to either use a framework like MEF (http://www.codeplex.com/MEF);
    // or Mono.Addins; or push all Assemblies to the GAC to avoid this mess.
    public static class AssemblyLoader
    {

        /// <summary>
        /// Initialises the AssemblyLoader in the <see cref="AppDomain">target AppDomain</see> with a minimal search path
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="config"></param>
        public static void Bootstrap(AppDomain domain, KistlConfig config)
        {
            var init = (AssemblyLoaderInitializer)domain.CreateInstanceAndUnwrap(
                "Kistl.API",
                "Kistl.API.AssemblyLoaderInitializer");

            init.Init(config);
        }

        private static bool _isInitialised = false;
        public static void EnsureInitialisation(KistlConfig config)
        {
            lock (typeof(AssemblyLoader))
            {
                if (_isInitialised) return;
                _isInitialised = true;

                InitialiseTargetAssemblyFolder(config);
                InitialiseSearchPath(config);

                // Start resolving Assemblies
                AppDomain.CurrentDomain.AssemblyResolve += AssemblyLoader.AssemblyResolve;
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += AssemblyLoader.AssemblyResolve;
            }
        }

        private static void InitialiseSearchPath(KistlConfig config)
        {
            foreach (var path in config.SourceFileLocation)
            {
                AssemblyLoader.SearchPath.Add(path);
            }
        }

        private static void InitialiseTargetAssemblyFolder(KistlConfig config)
        {
            TargetAssemblyFolder = Path.Combine(config.WorkingFolder, @"bin\");
            Directory.CreateDirectory(TargetAssemblyFolder);

            // Delete stale Assemblies
            Directory.GetFiles(AssemblyLoader.TargetAssemblyFolder).ForEach<string>(f => System.IO.File.Delete(f));
            Trace.TraceInformation("Cleaned TargetAssemblyFolder {0}", AssemblyLoader.TargetAssemblyFolder);
        }

        /// <summary>
        /// Gets the Target Assembly Folder. Directory is created if it does not exist.
        /// </summary>
        public static string TargetAssemblyFolder { get; private set; }

        static AssemblyLoader()
        {
            SearchPath = new List<string>();
        }
        /// <summary>
        /// A list of paths to search for assemblies
        /// </summary>
        public static IList<string> SearchPath { get; private set; }

        /// <summary>
        /// Assembly Cache
        /// </summary>
        private static Dictionary<string, Assembly> _Assemblies = new Dictionary<string, Assembly>();

        /// <summary>
        /// Called by the Framework to resolve an Assembly. Initialized in the <see cref="ApplicationContext"/>.
        /// </summary>
        /// <param name="sender">See MSDN</param>
        /// <param name="args">See MSDN</param>
        /// <returns>Returns the requested Assembly or null if not found. 
        /// See http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=1109769&amp;SiteID=1
        /// </returns>
        internal static Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (AssemblyLoader.SearchPath.Count <= 0) return null;
            // Do not call Trace.WriteLine! A TraceListener might want to load XML Serializers.dll and
            // this would lead to a StackOverflow due to recursion.
            Trace.TraceInformation("Resolving Assembly {0}", args.Name);
            return Load(args.Name);
        }

        /// <summary>
        /// Loads an Assembly.
        /// </summary>
        /// <param name="name">Assemblyname, passed to a AssemblyName Constructor.</param>
        /// <returns>Returns the requested Assembly or null if not found. 
        /// See http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=1109769&amp;SiteID=1
        /// </returns>
        public static Assembly Load(string name)
        {
            // Be nice & Thread Save
            lock (typeof(AssemblyLoader))
            {
                foreach (string path in AssemblyLoader.SearchPath)
                {
                    string targetPath = TargetAssemblyFolder;

                    // Create a AssemblyName Object & set the CodeBase.
                    AssemblyName n = new AssemblyName(name);
                    n.CodeBase = targetPath;
                    string fullName = "";

                    // Resolve .dll or .exe
                    if (File.Exists(Path.Combine(path, n.Name) + ".dll"))
                    {
                        fullName = n.Name + ".dll";

                    }
                    else if (File.Exists(Path.Combine(path, n.Name) + ".exe"))
                    {
                        fullName = n.Name + ".exe";
                    }

                    // If found, continue...
                    if (!string.IsNullOrEmpty(fullName))
                    {
                        // If found in cache, then return the cached Assembly
                        if (_Assemblies.ContainsKey(fullName)) return _Assemblies[fullName];

                        n.CodeBase += fullName;
                        // Copy files to destination folder, override existing files
                        string sourceDll = Path.Combine(path, fullName);
                        Trace.TraceInformation("Loading from {0}", sourceDll);
                        try
                        {
                            File.Copy(sourceDll, n.CodeBase, true);
                            // Also copy .PDB Files.
                            string sourcePDBFile = sourceDll + ".pdb";
                            if (File.Exists(sourcePDBFile))
                            {
                                File.Copy(sourcePDBFile, Path.Combine(targetPath, n.Name) + ".pdb", true);
                            }
                        }
                        catch (Exception ex)
                        {
                            Trace.TraceError(ex.ToString());
                        }

                        // Finally load the Assembly
                        Assembly result = Assembly.LoadFrom(n.CodeBase);

                        // If the assembly could not be loaded, do nothing! Return null. 
                        // See http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=1109769&SiteID=1
                        if (result != null)
                        {
                            // Add to cache.
                            _Assemblies[fullName] = result;
                        }

                        return result;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Loads an Assembly for Reflection only. This method does not cache the requested Assemblies.
        /// </summary>
        /// <param name="name">Assemblyname, passed to a AssemblyName Consructor.</param>
        /// <returns>Returns the requested Assembly or null if not found. see http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=1109769&amp;SiteID=1</returns>
        public static Assembly ReflectionOnlyLoadFrom(string name)
        {
            // Be nice & Thread Save
            lock (typeof(AssemblyLoader))
            {
                foreach (string path in AssemblyLoader.SearchPath)
                {
                    string targetPath = TargetAssemblyFolder;

                    string fullName = "";

                    // Resolve .dll or .exe
                    if (System.IO.File.Exists(Path.Combine(path, name) + ".dll"))
                    {
                        fullName = name + ".dll";

                    }
                    else if (System.IO.File.Exists(Path.Combine(path, name) + ".exe"))
                    {
                        fullName = name + ".exe";
                    }

                    if (!string.IsNullOrEmpty(fullName))
                    {
                        string sourceDll = Path.Combine(path, fullName);
                        Trace.TraceInformation("Loading for reflection from {0}", sourceDll);

                        // If the Assembly is already loaded -> do not try to copy! It's locked.
                        if (!_Assemblies.ContainsKey(fullName))
                        {
                            // Copy files to destination folder, override existing files
                            try
                            {
                                File.Copy(sourceDll, Path.Combine(targetPath, fullName), true);
                            }
                            catch (Exception ex)
                            {
                                Trace.TraceError(ex.ToString());
                            }
                        }
                        // Load, but do not cache
                        return Assembly.ReflectionOnlyLoadFrom(Path.Combine(targetPath, fullName));
                    }
                }

                return null;
            }
        }
    }

    public class AssemblyLoaderInitializer : MarshalByRefObject
    {
        public void Init(KistlConfig config)
        {
            AssemblyLoader.EnsureInitialisation(config);
        }
    }
}
