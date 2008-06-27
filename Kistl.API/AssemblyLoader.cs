using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Kistl.API
{
    /// <summary>
    /// This is the Assembly Loader. Assemblies are obtained by you configurated Assembly sources.
    /// Assemblies are copied to your WorkingFolder\bin directory. 
    /// eg. C:\Users\Arthur\AppData\Local\dasz\Kistl\Arthur's Configuration\Kistl.Client.exe\bin.
    /// This does not seams to be the best solution. See http://blogs.msdn.com/suzcook/archive/2003/05/29/57143.aspx.
    /// But it works.
    /// </summary>
    public class AssemblyLoader
    {
        /// <summary>
        /// Private Field for TargetAssemblyFolder
        /// </summary>
        private static string _TargetAssemblyFolder;

        /// <summary>
        /// Gets the Target Assembly Folder. Directory is created if it does not exist.
        /// </summary>
        public static string TargetAssemblyFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_TargetAssemblyFolder))
                {
                    _TargetAssemblyFolder = Helper.WorkingFolder + @"bin\";
                    System.IO.Directory.CreateDirectory(_TargetAssemblyFolder);
                }

                return _TargetAssemblyFolder;
            }
        }

        /// <summary>
        /// Assembly Cache
        /// </summary>
        private static Dictionary<string, Assembly> _Assemblies = new Dictionary<string, Assembly>();

        /// <summary>
        /// Called by the Framework to resolve an Assembly. Initialized by <see cref="APIInit"/>.
        /// Do not call Trace.WriteLine!!!! A Tracelistener might want to load XML Serializers.dll! 
        /// This would lead to a StackOverflow due to a recursion.
        /// </summary>
        /// <param name="sender">See MSDN</param>
        /// <param name="args">See MSDN</param>
        /// <returns>Returns the requested Assembly or null if not found. see http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=1109769&amp;SiteID=1</returns>
        internal static Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (!Configuration.KistlConfig.IsInitialized) return null;
            Console.WriteLine("Resolving Assembly {0}", args.Name);
            return Load(args.Name);
        }

        /// <summary>
        /// Loads an Assembly.
        /// </summary>
        /// <param name="name">Assemblyname, passed to a AssemblyName Consructor.</param>
        /// <returns>Returns the requested Assembly or null if not found. see http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=1109769&amp;SiteID=1</returns>
        public static Assembly Load(string name)
        {
            // Be nice & Thread Save
            lock (typeof(AssemblyLoader))
            {
                foreach (string path in Configuration.KistlConfig.Current.SourceFileLocation)
                {
                    // Create a AssemblyName Object & set the CodeBase.
                    AssemblyName n = new AssemblyName(name);
                    n.CodeBase = TargetAssemblyFolder;
                    string fullName = "";

                    // Resolve .dll or .exe
                    if (System.IO.File.Exists(path + @"\" + n.Name + ".dll"))
                    {
                        fullName = n.Name + ".dll";

                    }
                    else if (System.IO.File.Exists(path + @"\" + n.Name + ".exe"))
                    {
                        fullName = n.Name + ".exe";
                    }

                    // If found, continue...
                    if (!string.IsNullOrEmpty(fullName))
                    {
                        // IF found in cache, then return the cached Assembly
                        if (_Assemblies.ContainsKey(fullName)) return _Assemblies[fullName];

                        n.CodeBase += fullName;
                        // Copy files to destination folder, override existing files
                        try
                        {
                            System.IO.File.Copy(path + @"\" + fullName, n.CodeBase, true);
                            // Also copy .PDB Files.
                            string sourcePDBFile = path + @"\" + n.Name + ".pdb";
                            if (System.IO.File.Exists(sourcePDBFile))
                            {
                                System.IO.File.Copy(sourcePDBFile, TargetAssemblyFolder + n.Name + ".pdb", true);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        // Finally load the Assembly
                        Assembly result = Assembly.Load(n);

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
                foreach (string path in Configuration.KistlConfig.Current.SourceFileLocation)
                {
                    string fullName = "";

                    // Resolve .dll or .exe
                    if (System.IO.File.Exists(path + @"\" + name + ".dll"))
                    {
                        fullName = name + ".dll";

                    }
                    else if (System.IO.File.Exists(path + @"\" + name + ".exe"))
                    {
                        fullName = name + ".exe";
                    }

                    if (!string.IsNullOrEmpty(fullName))
                    {
                        // If the Assembly is already loaded -> do not try to copy! It's locked.
                        if (!_Assemblies.ContainsKey(fullName))
                        {
                            // Copy files to destination folder, override existing files
                            try
                            {
                                System.IO.File.Copy(path + @"\" + fullName, TargetAssemblyFolder + fullName, true);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }

                        // Load, but do not cache
                        return Assembly.ReflectionOnlyLoadFrom(TargetAssemblyFolder + fullName);
                    }
                }

                return null;
            }
        }
    }
}
