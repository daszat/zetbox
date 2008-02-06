using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Kistl.API
{
    public class AssemblyLoader
    {
        private static string _TargetAssemblyFolder;
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

        private static Dictionary<string, Assembly> _Assemblies = new Dictionary<string, Assembly>();

        /// <summary>
        /// Auch wenn'S nicht sooo toll ist. Siehe http://blogs.msdn.com/suzcook/archive/2003/05/29/57143.aspx
        /// Es scheint zu funktionieren.
        /// Aber ja nicht Trace.WriteLine aufrufen!!!! Meine TraceLib versucht eine XML Serializer.dll zu laden! 
        /// Die gibt es aber (noch) nicht, sie wird erst später autogeneriert.
        /// Wenn das Assembly nicht gefunden wird -> null zurück geben.
        /// http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=1109769&SiteID=1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        internal static Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (!Configuration.KistlConfig.IsInitialized) return null;
            Console.WriteLine("Resolving Assembly {0}", args.Name);
            return Load(args.Name);
        }

        public static Assembly Load(string name)
        {
            // Be nice & Thread Save
            lock (typeof(AssemblyLoader))
            {
                foreach (string path in Configuration.KistlConfig.Current.SourceFileLocation)
                {
                    AssemblyName n = new AssemblyName(name);
                    n.CodeBase = TargetAssemblyFolder;
                    string fullName = "";

                    if (System.IO.File.Exists(path + @"\" + n.Name + ".dll"))
                    {
                        fullName = n.Name + ".dll";

                    }
                    else if (System.IO.File.Exists(path + @"\" + n.Name + ".exe"))
                    {
                        fullName = n.Name + ".exe";
                    }

                    if (!string.IsNullOrEmpty(fullName))
                    {
                        if (_Assemblies.ContainsKey(fullName)) return _Assemblies[fullName];

                        n.CodeBase += fullName;
                        try
                        {
                            System.IO.File.Copy(path + @"\" + fullName, n.CodeBase, true);
                            System.IO.File.Copy(path + @"\" + n.Name + ".pdb", TargetAssemblyFolder + n.Name + ".pdb", true);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        Assembly result = Assembly.Load(n);

                        _Assemblies[fullName] = result;

                        return result;
                    }
                }

                return null;
            }
        }

        public static Assembly ReflectionOnlyLoadFrom(string name)
        {
            // Be nice & Thread Save
            lock (typeof(AssemblyLoader))
            {
                foreach (string path in Configuration.KistlConfig.Current.SourceFileLocation)
                {
                    string fullName = "";

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
                        if (!_Assemblies.ContainsKey(fullName))
                        {
                            try
                            {
                                System.IO.File.Copy(path + @"\" + fullName, TargetAssemblyFolder + fullName, true);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }

                        // Trotzdem laden...
                        // und nicht merken!!
                        return Assembly.ReflectionOnlyLoadFrom(TargetAssemblyFolder + fullName);
                    }
                }

                return null;
            }
        }
    }
}
