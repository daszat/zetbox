using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Kistl.Client.Bootstrapper
{
    public enum FileType
    {
        File = 1,
        Executalable = 2,
        AppConfig = 3,
    }

    [XmlRoot("ArrayOfFileInfo", Namespace = "http://dasz.at/ZBox/Bootstrapper/FileInfo")]
    public class FileInfoArray
    {
        [XmlElement("FileInfo")]
        public FileInfo[] Files { get; set; }
    }

    public class FileInfo
    {
        public string Name { get; set; }
        public string DestPath { get; set; }
        public DateTime Date { get; set; }
        public long Size { get; set; }
        public FileType Type { get; set; }
    }
}
