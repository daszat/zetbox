// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using System.Text;
    using Zetbox.API.Configuration;
    using System.Text.RegularExpressions;

    public enum FileType
    {
        File = 1,
        Exec = 2,
    }

    [DataContract(Namespace = "http://dasz.at/Zetbox/Bootstrapper")]
    public class FileInfo
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "DestPath")]
        public string DestPath { get; set; }
        [DataMember(Name = "Date")]
        public DateTime Date { get; set; }
        [DataMember(Name = "Size")]
        public long Size { get; set; }
        [DataMember(Name = "Type")]
        public FileType Type { get; set; }
    }

    [ServiceContract(SessionMode = SessionMode.NotAllowed, Namespace = "http://dasz.at/Zetbox/Bootstrapper")]
    public interface IBootstrapperService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/GetFileInfos")]
        FileInfo[] GetFileInfos();

        [OperationContract]
        [WebGet(UriTemplate = "/GetFile/{*path}")]
        Stream GetFile(string path);
    }

    /// <summary>
    /// Bootstrapper service
    /// </summary>
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://dasz.at/Zetbox/Bootstrapper")]
    public class BootstrapperService
        : IBootstrapperService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Server.BootstrapperService");

        private readonly ZetboxConfig config;

        public BootstrapperService(ZetboxConfig config)
        {
            if (config == null) throw new ArgumentNullException("config");

            Log.InfoFormat("Starting BootstrapperService from {0}", config.ConfigFilePath);
            this.config = config;
        }

        public FileInfo[] GetFileInfos()
        {
            var result = new List<FileInfo>();

            foreach (var location in config.Server.ClientFilesLocations)
            {
                Log.InfoFormat("Processing client file location [{0}] ({0})", location.Name, location.Value);

                var value = ResolveConfigPath(location.Value);

                switch (location.Name)
                {
                    case "Exe":
                        if (File.Exists(value))
                        {
                            // whole file is specified, get directory path
                            var directory = Path.GetFullPath(Path.GetDirectoryName(value));

                            result.Add(InspectFile("Exe", directory, Path.GetFileName(value)));
                            if (location.Start)
                            {
                                result.Last().Type = FileType.Exec;
                            }

                            // need to collect .config too
                            var dotConfig = value + ".config";
                            if (File.Exists(dotConfig))
                            {
                                result.Add(InspectFile("Exe", directory, dotConfig));
                            }
                        }
                        break;
                    case "Configs":
                        if (File.Exists(value))
                        {
                            var fi = InspectFile("Configs", Path.GetFullPath(Path.GetDirectoryName(value)), value);
                            result.Add(fi);
                        }
                        break;
                    default:
                        {
                            var root = Path.GetFullPath(value);
                            var regex = !string.IsNullOrEmpty(location.Exclude) ? new Regex(location.Exclude) : null;

                            foreach (var f in Directory.GetFiles(value, "*.*", SearchOption.AllDirectories))
                            {
                                if (regex != null && regex.IsMatch(f)) continue;
                                var fi = InspectFile(location.Name, root, f);
                                result.Add(fi);
                            }
                        }
                        break;
                }
            }

            return result.ToArray();
        }

        private static string ResolveConfigPath(string dir)
        {
            var value = Path.IsPathRooted(dir)
                ? dir
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dir);
            return value;
        }

        private FileInfo InspectFile(string baseDir, string root, string f)
        {
            var fi = Path.IsPathRooted(f)
                ? new System.IO.FileInfo(f)
                : new System.IO.FileInfo(Path.Combine(root, f));

            if (!fi.FullName.StartsWith(root)) throw new InvalidOperationException(String.Format("Aborting because [{0}] is outside the current root [{1}].", fi.FullName, root));

            var relPath = Path.Combine(baseDir, RelativePath(root, Path.GetDirectoryName(fi.FullName)));
            var fileInfo = new FileInfo() { Name = fi.Name, DestPath = relPath, Date = fi.LastWriteTimeUtc, Size = fi.Length, Type = FileType.File };
            return fileInfo;
        }

        /// <summary>
        /// Make a relative path from root to target, provided that target is a subdirectory of root.
        /// </summary>
        private static string RelativePath(string root, string target)
        {
            if (root == target)
                return String.Empty;
            else
                return target.Substring(root.Length + 1);
        }

        public string GetFilePath(string path)
        {
            if (String.IsNullOrEmpty(path)) throw new ArgumentNullException("path");
            var parts = path.Split('/');
            var dir = config.Server.ClientFilesLocations.Single(i => i.Name == parts[0]);
            var value = ResolveConfigPath(dir.Value);
            var file = dir.Name == "Exe" || dir.Name == "Configs"
                ? Path.GetFullPath(Path.Combine(Path.GetDirectoryName(value), API.Helper.PathCombine(parts.Skip(1).ToArray()))) // Exe and Configs reference file directly
                : Path.GetFullPath(Path.Combine(value, API.Helper.PathCombine(parts.Skip(1).ToArray())));
            if (file.StartsWith(Path.GetFullPath(value)))
            {
                return file;
            }
            else
            {
                throw new ArgumentException(String.Format("file [{0}] is not within configured directory [{1}]", file, dir.Value), "path");
            }
        }

        public Stream GetFile(string path)
        {
            if (String.IsNullOrEmpty(path)) throw new ArgumentNullException("path");
            var probe = GetFilePath(path);
            if (File.Exists(probe))
            {
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                System.IO.FileInfo fi = new System.IO.FileInfo(probe);
                return fi.OpenRead();
            }
            throw new FileNotFoundException(String.Format("File [{0}] not found in Bootstrapper directories", path), path);
        }
    }
}
