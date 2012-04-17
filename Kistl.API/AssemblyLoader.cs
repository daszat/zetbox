using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API.Configuration;
using Kistl.API.Utils;

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
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.AssemblyLoader");
        private readonly static object _lock = new object();

        /// <summary>
        /// Initializes the AssemblyLoader in the <see cref="AppDomain">target AppDomain</see> with a minimal search path.
        /// </summary>
        public static void Bootstrap(AppDomain domain, KistlConfig config)
        {
            if (domain == null) { throw new ArgumentNullException("domain"); }
            if (config == null) { throw new ArgumentNullException("config"); }

            var init = (AssemblyLoaderInitializer)domain.CreateInstanceAndUnwrap(
                "Kistl.API",
                "Kistl.API.AssemblyLoaderInitializer");

            init.Init(config);
        }

        public static void Unload(AppDomain domain)
        {
            if (domain == null) { throw new ArgumentNullException("domain"); }

            var init = (AssemblyLoaderInitializer)domain.CreateInstanceAndUnwrap(
                "Kistl.API",
                "Kistl.API.AssemblyLoaderInitializer");

            init.Unload();
        }

        internal static void Unload()
        {
            lock (_lock)
            {
                Log.DebugFormat("Unloading from {0}", AppDomain.CurrentDomain.FriendlyName);
                AppDomain.CurrentDomain.AssemblyResolve -= AssemblyLoader.AssemblyResolve;
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= AssemblyLoader.ReflectionOnlyAssemblyResolve;
            }
        }

        private static bool _isInitialised = false;
        public static void EnsureInitialisation(KistlConfig config)
        {
            if (config == null) { throw new ArgumentNullException("config"); }

            lock (_lock)
            {
                if (_isInitialised) return;
                _isInitialised = true;

                Log.DebugFormat("Initializing {0}", AppDomain.CurrentDomain.FriendlyName);
                InitialiseTargetAssemblyFolder(config);
                InitialiseSearchPath(config.AssemblySearchPaths.Paths);

                // Start resolving Assemblies
                AppDomain.CurrentDomain.AssemblyResolve += AssemblyLoader.AssemblyResolve;
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += AssemblyLoader.ReflectionOnlyAssemblyResolve;
            }
        }

        private static void InitialiseSearchPath(string[] paths)
        {
            foreach (var path in paths ?? new string[] { })
            {
                var rootedPath = Path.IsPathRooted(path)
                    ? path
                    : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

                Log.DebugFormat("Added searchpath [{0}]", rootedPath);
                AssemblyLoader.SearchPath.Add(rootedPath);
            }
        }

        /// <param name="config">must not be null</param>
        private static void InitialiseTargetAssemblyFolder(KistlConfig config)
        {
            EnableShadowCopy = config.AssemblySearchPaths == null ? false : config.AssemblySearchPaths.EnableShadowCopy;
            if (EnableShadowCopy)
            {
                TargetAssemblyFolder = Path.Combine(config.TempFolder, "bin");
                Directory.CreateDirectory(TargetAssemblyFolder);

                // Delete stale Assemblies
                try
                {
                    Directory.GetFiles(AssemblyLoader.TargetAssemblyFolder).ForEach<string>(f => System.IO.File.Delete(f));
                    Log.InfoFormat("Cleaned TargetAssemblyFolder {0}", AssemblyLoader.TargetAssemblyFolder);
                }
                catch (Exception ex)
                {
                    Log.WarnFormat("Couldn't clean TargetAssemblyFolder {0}: {1}", AssemblyLoader.TargetAssemblyFolder, ex.ToString());
                }
            }
            else
            {
                Log.Info("Shadow copy has been disabled");
            }
        }

        /// <summary>
        /// Gets the Target Assembly Folder. Directory is created if it does not exist.
        /// </summary>
        public static string TargetAssemblyFolder { get; private set; }
        public static bool EnableShadowCopy { get; private set; }

        static AssemblyLoader()
        {
            SearchPath = new List<string>();
        }
        /// <summary>
        /// A list of paths to search for assemblies
        /// </summary>
        public static IList<string> SearchPath { get; private set; }

        /// <summary>
        /// Called by the Framework to resolve an Assembly.
        /// </summary>
        /// <param name="sender">See MSDN</param>
        /// <param name="args">See MSDN</param>
        /// <returns>Returns the requested Assembly or null if not found. 
        /// See http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=1109769&amp;SiteID=1
        /// </returns>
        internal static Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (AssemblyLoader.SearchPath.Count <= 0) return null;
            Log.DebugFormat("Resolving Assembly {0}", args.Name);
            return LoadAssemblyByName(args.Name, false);
        }

        internal static Assembly ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            Log.DebugFormat("Resolving Assembly {0} for reflection", args.Name);
            try
            {
                // http://blogs.msdn.com/b/jmstall/archive/2006/11/22/reflection-type-load-exception.aspx
                // try loading through ReflectionOnlyLoad first. This will resolve dependencies
                // Even to System!
                var a = System.Reflection.Assembly.ReflectionOnlyLoad(args.Name);
                if (a != null) return a;
            }
            catch
            {
                // Don't care, continue loading "by hand"
            }
            if (AssemblyLoader.SearchPath.Count <= 0) return null;
            return LoadAssemblyByName(args.Name, true);
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
            return LoadAssemblyByName(name, false);
        }

        /// <summary>
        /// Loads an Assembly for Reflection only. This method does not cache the requested Assemblies.
        /// </summary>
        /// <param name="name">Assemblyname, passed to a AssemblyName Consructor.</param>
        /// <returns>Returns the requested Assembly or null if not found. see http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=1109769&amp;SiteID=1</returns>
        public static Assembly ReflectionOnlyLoadFrom(string name)
        {
            return LoadAssemblyByName(name, true);
        }

        /// <summary>
        /// Returns the actual filename of a DLL or EXE matching a given base assembly name. Uses the SearchPath.
        /// </summary>
        /// <returns>null if the assembly cannot be located in the SearchPath. Otherwise, the full path.</returns>
        private static string LocateAssembly(string baseName)
        {
            foreach (string path in AssemblyLoader.SearchPath)
            {
                string basePath = Path.Combine(path, baseName);
                // Resolve .dll or .exe
                if (File.Exists(basePath + ".dll"))
                {
                    return basePath + ".dll";
                }
                else if (File.Exists(basePath + ".exe"))
                {
                    return basePath + ".exe";
                }
            }
            return null;
        }


        private static string PdbFromDll(string dllFile)
        {
            var parts = dllFile.Split('.');
            var extension = parts.Last();
            if (extension == "exe" || extension == "dll")
            {
                return String.Join(".", parts.Take(parts.Length - 1).Concat(new[] { "pdb" }).ToArray());
            }
            else
            {
                return dllFile;
            }
        }

        private static string _currentlyLoading = null;

        private static Assembly LoadAssemblyByName(string name, bool reflectOnly)
        {
            // Be nice & Thread Save
            lock (_lock)
            {
                try
                {
                    AssemblyName assemblyName = new AssemblyName(name);
                    string baseName = assemblyName.Name;

                    // Prevent loading twice, it doesn't help
                    if (baseName == _currentlyLoading) return null;
                    _currentlyLoading = baseName;

                    // search for file to load
                    string sourceDll = LocateAssembly(baseName);

                    // assembly could not be found?
                    if (String.IsNullOrEmpty(sourceDll))
                        return null;

                    // Copy files to destination folder, unless the target file exists
                    // the folder should have been cleared on initialisation and once
                    // an assembly is loaded, we cannot re-load the assembly anyways.
                    string dllToLoad;
                    if (EnableShadowCopy)
                    {
                        dllToLoad = Path.Combine(TargetAssemblyFolder, baseName + ".dll");
                        Log.DebugFormat("Loading {0} (from {1}){2}", sourceDll, dllToLoad, reflectOnly ? " for reflection" : String.Empty);
                        try
                        {
                            if (!File.Exists(dllToLoad))
                            {
                                File.Copy(sourceDll, dllToLoad, true);
                                // Also copy .PDB Files.
                                string sourcePDBFile = PdbFromDll(sourceDll);
                                string targetPDBFile = PdbFromDll(dllToLoad);
                                if (File.Exists(sourcePDBFile))
                                {
                                    File.Copy(sourcePDBFile, targetPDBFile, true);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Warn("Error loading assembly", ex);
                        }
                    }
                    else
                    {
                        Log.DebugFormat("Loading {0}{1}", sourceDll, reflectOnly ? " for reflection" : String.Empty);
                        dllToLoad = sourceDll;
                    }
                    Assembly result = null;

                    // Finally load the Assembly
                    if (reflectOnly)
                    {
                        result = Assembly.ReflectionOnlyLoadFrom(dllToLoad);
                    }
                    else
                    {
                        assemblyName.CodeBase = dllToLoad;
                        result = Assembly.Load(assemblyName);

                        // If the assembly could not be loaded, do nothing! Return null. 
                        // See http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=1109769&SiteID=1
                    }
                    if (result == null)
                        Log.WarnFormat("Cannot load {0}", baseName);
                    return result;
                }
                finally
                {
                    _currentlyLoading = null;
                }
            }
        }


    }

    public class AssemblyLoaderInitializer : MarshalByRefObject
    {
        public void Init(KistlConfig config)
        {
            AssemblyLoader.EnsureInitialisation(config);
        }

        public void Unload()
        {
            AssemblyLoader.Unload();
        }
    }
}
