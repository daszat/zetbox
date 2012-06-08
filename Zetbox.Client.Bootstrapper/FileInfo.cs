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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Zetbox.Client.Bootstrapper
{
    public enum FileType
    {
        File = 1,
        Exec = 2,
        AppConfig = 3,
    }

    [XmlRoot("ArrayOfFileInfo", Namespace = "http://dasz.at/Zetbox/Bootstrapper")]
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

        public string GetFullFileName(string targetDir)
        {
            switch (DestPath)
            {
                case "Exe":
                    return Path.GetFullPath(Path.Combine(targetDir, Path.Combine(String.Empty, Name)));
                case "Config":
                case "Configs":
                    return Path.GetFullPath(Path.Combine(targetDir, Path.Combine(DestPath, "DefaultConfig.xml")));
                default:
                    return Path.GetFullPath(Path.Combine(targetDir, Path.Combine(DestPath, Name)));
            }
        }

        public string GetDisplayFileName()
        {
            return Path.Combine(DestPath, Name);
        }
    }
}
