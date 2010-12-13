using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Kistl.API.Configuration;
using System.IO;

namespace Kistl.Server
{
    public enum FileType
    {
        File = 1,
        Executalable = 2,
        AppConfig = 3,
    }

    [DataContract]
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

    [ServiceContract]
    public interface IBootstrapperService
    {
        [OperationContract]
        FileInfo[] GetFileInfos();

        [OperationContract]
        byte[] GetFile(string path, string name);
    }

    /// <summary>
    /// Bootstrapper service
    /// </summary>
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
                    return FileType.Executalable;
                case "kistl.client.wpf.exe.config":
                    return FileType.AppConfig;
                default:
                    return FileType.File;
            }
        }

        public byte[] GetFile(string path, string name)
        {
            var dir = config.Server.ClientFilesLocations.Single(i => i.Name == path);
            var probe = Path.Combine(dir.Value, name);
            if (File.Exists(probe))
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(probe);
                var result = new byte[fi.Length];
                using (var fs = fi.OpenRead())
                {
                    fs.Read(result, 0, (int)fi.Length);
                    return result;
                }
            }
            throw new FileNotFoundException("File not found in Bootstrapper directories", Path.Combine(path, name));
        }
    }
}
