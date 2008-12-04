using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace Arebis.Utils
{
	/// <summary>
	/// Utility class containing several static utility related to the files or the file system.
	/// </summary>
	public static class FileUtils
	{
		/// <summary>
		/// Returns the full filepath of an existing file matching the given filename
		/// in one of the given path directories. Returns null if no file found.
		/// </summary>
		public static string FindInPath(string filename, string[] path)
		{
			// No filename results in null path:
			if ((filename == null) || (filename.Length == 0)) return null;

			// Search each path directory for the file:
			foreach (string dir in path)
			{ 
				string file = Path.Combine(dir, filename);
				if (File.Exists(file)) return file;
			}

			// Return null if not found:
			return null;
		}

        public static bool IsResourceFile(this string filename)
        {
            return filename.ToLower().StartsWith("res://");
        }
    
        public static Assembly GetResourceAssembly(this string filename)
        {
            string res = filename.Substring("res://".Length);
            int idx = res.IndexOf('/');
            string assembly = res.Substring(0, idx);
            res = res.Substring(idx + 1);

            var a = Assembly.Load(assembly);
            if (a == null) throw new ArgumentException(string.Format("Assembly {0} in Path {1} not found", assembly, filename));
            return a;
        }

        public static Stream GetResourceStream(this string filename)
        {
            string res = filename.Substring("res://".Length);
            int idx = res.IndexOf('/');
            string assembly = res.Substring(0, idx);
            res = res.Substring(idx + 1);

            Assembly a = GetResourceAssembly(filename);
            Stream s = a.GetManifestResourceStream(res);
            if (s == null)
            {
                throw new ResourceNotFoundException(res, a);
            }
            return s;
        }
    }

    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string res, Assembly a)
            :base(string.Format("ResourceStream {0} in Assembly {1} not found!\n\n------------- Resources -------------\n{2}\n------------- Resources -------------", 
                    res, a.FullName,
                    string.Join("\n", a.GetManifestResourceNames()) ))
        {
        }
    }
}
