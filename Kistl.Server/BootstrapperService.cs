
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

    [DataContract(Namespace="http://dasz.at/ZBox/Bootstrapper")]
    public class FileInfo
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string DestPath { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public long Size { get; set; }
        [DataMember]
        public FileType Type { get; set; }
    }

    [ServiceContract(SessionMode = SessionMode.NotAllowed, Namespace="http://dasz.at/ZBox/Bootstrapper")]
    public interface IBootstrapperService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/GetFileInfos")]
        FileInfo[] GetFileInfos();

        [OperationContract]
        [WebGet(UriTemplate = "/GetFile/{path}/{name}")]
        Stream GetFile(string path, string name);
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
                foreach (var f in Directory.GetFiles(dir.Value, "*.*", SearchOption.AllDirectories))
                {
                    var fi = new System.IO.FileInfo(f);
                    result.Add(new FileInfo() { Name = fi.Name, DestPath = dir.Name, Date = fi.LastWriteTimeUtc, Size = fi.Length, Type = GetFileType(fi) });
                }
            }

            return result.ToArray();
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

        public Stream GetFile(string path, string name)
        {
            var dir = config.Server.ClientFilesLocations.Single(i => i.Name == path);
            var probe = Path.Combine(dir.Value, name);
            if (File.Exists(probe))
            {
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                System.IO.FileInfo fi = new System.IO.FileInfo(probe);
                return fi.OpenRead();
            }
            throw new FileNotFoundException("File not found in Bootstrapper directories", Path.Combine(path, name));
        }
    }
}
