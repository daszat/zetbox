
namespace Kistl.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using System.Text;
    using Kistl.API.Configuration;

    public enum FileType
    {
        File = 1,
        Exec = 2,
        AppConfig = 3,
    }

    [DataContract(Namespace = "http://dasz.at/ZBox/Bootstrapper")]
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

    [ServiceContract(SessionMode = SessionMode.NotAllowed, Namespace = "http://dasz.at/ZBox/Bootstrapper")]
    public interface IBootstrapperService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/GetFileInfos")]
        FileInfo[] GetFileInfos();

        [OperationContract]
        [WebGet(UriTemplate = "/GetFile/{path}")]
        Stream GetFile(string path);
    }

    /// <summary>
    /// Bootstrapper service
    /// </summary>
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://dasz.at/ZBox/Bootstrapper")]
    public class BootstrapperService : IBootstrapperService
    {
        private KistlConfig config;

        public BootstrapperService(KistlConfig config)
        {
            this.config = config;
        }

        public FileInfo[] GetFileInfos()
        {
            var result = new List<FileInfo>();

            foreach (var dir in config.Server.ClientFilesLocations)
            {
                var root = Path.GetFullPath(dir.Value);

                foreach (var f in Directory.GetFiles(dir.Value, "*.*", SearchOption.AllDirectories))
                {
                    var fi = new System.IO.FileInfo(f);
                    if (!fi.FullName.StartsWith(root)) throw new InvalidOperationException(String.Format("Aborting because [{0}] is outside the current root [{1}].", fi.FullName, root));

                    var relPath = Path.Combine(dir.Name, RelativePath(root, Path.GetDirectoryName(fi.FullName)));

                    result.Add(new FileInfo() { Name = fi.Name, DestPath = relPath, Date = fi.LastWriteTimeUtc, Size = fi.Length, Type = GetFileType(fi) });
                }
            }

            return result.ToArray();
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

        private FileType GetFileType(System.IO.FileInfo fi)
        {
            switch (fi.Name.ToLower())
            {
                case "kistl.client.wpf.exe":
                    return FileType.Exec;
                case "kistl.client.wpf.exe.config":
                    return FileType.AppConfig;
                default:
                    return FileType.File;
            }
        }

        public string GetFilePath(string path)
        {
            var parts = path.Split('/');

            var dir = config.Server.ClientFilesLocations.Single(i => i.Name == parts[0]);
            var file = Path.GetFullPath(Path.Combine(dir.Value, String.Join(Path.DirectorySeparatorChar.ToString(), parts.Skip(1).ToArray())));
            if (file.StartsWith(Path.GetFullPath(dir.Value)))
            {
                return file;
            }
            else
            {
                throw new ArgumentException("path", String.Format("file [{0}] is not within configured directory [{1}]", file, dir.Value));
            }
        }

        public Stream GetFile(string path)
        {
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
