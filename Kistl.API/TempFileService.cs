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
        string CreateTempFolder();
        string CreateTempFolder(string filename);
        string CreateTempFile();
        /// <summary>
        /// Extension should have a "." in it, otherwise it's a suffix
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        string CreateTempFile(string ext);    
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

        public string CreateTempFolder()
        {
            return GetTempName(true, null);            
        }

        public string CreateTempFolder(string filename)
        {
            var tmp = GetTempName(true, null);
            return Path.Combine(tmp, filename);
        }

        public string CreateTempFile()
        {
            return CreateTempFile(".tmp");
        }

        public string CreateTempFile(string ext)
        {
            if (string.IsNullOrEmpty(ext)) throw new ArgumentNullException("ext");
            return GetTempName(false, ext);
        }
    }
}
