using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Kistl.API.Configuration;
using Kistl.API.Utils;

namespace Kistl.API
{
    public interface ITempFileService
    {
        /// <summary>
        /// Creates an empty tempfile with the extension .tmp
        /// </summary>
        /// <returns></returns>
        string Create();
        /// <summary>
        /// Creates a tempfile with the specified filename.
        /// </summary>
        /// <remarks>
        /// Creates a temp folder and returns the path to it, combined with the given filename.
        /// </remarks>
        /// <param name="filename"></param>
        /// <returns></returns>
        string Create(string filename);
        /// <summary>
        /// Creates an empty tempfile with the given extension.
        /// Extension should have a "." in it, otherwise it's a suffix
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        string CreateWithExtension(string ext);
        /// <summary>
        /// Creates an empty temp folder.
        /// </summary>
        /// <returns></returns>
        string CreateFolder();
    }

    public class TempFileService : ITempFileService
    {
        private static readonly object _lock = new object();
        private readonly Random _rand = new Random();
        private readonly string _currentTempFolder;
        private readonly string _rootTempFolder;

        public TempFileService()
        {
            _rootTempFolder = Path.Combine(Path.GetTempPath(), Path.Combine("zetbox", "tmp"));
            _currentTempFolder = Path.Combine(_rootTempFolder, DateTime.Today.ToString("yyMMdd"));
            
            if (!Directory.Exists(_currentTempFolder))
                Directory.CreateDirectory(_currentTempFolder);

            System.Threading.ThreadPool.QueueUserWorkItem(DeleteOldFiles);
        }

        private void DeleteOldFiles(object state)
        {
            try
            {
                var deleteTime = DateTime.Now.AddDays(-1);
                foreach (var dir in Directory.GetDirectories(_rootTempFolder))
                {
                    var info = new DirectoryInfo(dir);
                    if (info.Name.Length == 6 // yyMMdd
                        && info.CreationTime < deleteTime)
                    {
                        try
                        {
                            Directory.Delete(dir, true);
                        }
                        catch(Exception ex)
                        {
                            Logging.Log.WarnFormat("Unable to delete old temp directory: {0}", ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Error("Error deleting old tempfiles", ex);
            }
        }

        private string GetTempName(bool asFolder, string ext)
        {
            lock (_lock)
            {
                for (int attempt = 0; attempt < 10; attempt++)
                {
                    var sb = new StringBuilder();
                    for (int i = 0; i < 8; i++)
                    {
                        sb.Append((char)_rand.Next('a', 'z'));
                    }
                    var probe = sb.ToString();
                    if (Directory.GetFiles(_currentTempFolder, probe + "*").Length == 0 && !Directory.Exists(Path.Combine(_currentTempFolder, probe)))
                    {
                        string result;
                        if (asFolder)
                        {
                            result = Path.Combine(_currentTempFolder, probe);
                            Directory.CreateDirectory(result);
                        }
                        else
                        {
                            result = Path.Combine(_currentTempFolder, probe + (ext ?? string.Empty));
                            try
                            {
                                using (var fs = File.Open(result, FileMode.CreateNew))
                                {
                                    fs.Flush();
                                    fs.Close();
                                }
                            }
                            catch (IOException)
                            {
                                // File already exists - continue
                                continue;
                            }
                        }
                        return result;
                    }
                }
                throw new InvalidOperationException("Unable to create a unique tempfile");
            }
        }

        public string Create()
        {
            return CreateWithExtension(".tmp");
        }

        public string Create(string filename)
        {
            if (string.IsNullOrEmpty(filename)) throw new ArgumentNullException("filename");
            var tmp = CreateFolder();
            return Path.Combine(tmp, filename);
        }

        public string CreateWithExtension(string ext)
        {
            if (string.IsNullOrEmpty(ext)) throw new ArgumentNullException("ext");
            return GetTempName(false, ext);
        }

        public string CreateFolder()
        {
            return GetTempName(true, null);
        }
    }
}
